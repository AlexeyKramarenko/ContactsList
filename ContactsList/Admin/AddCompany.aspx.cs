using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using AutoMapper;
using ContactsList.Admin.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactsList.Admin
{
    public partial class SaveCompany : System.Web.UI.Page
    {
        #region Repository properties
        [Inject]
        public IContactsRepository ContactsRepository { get; set; }
        [Inject]
        public IActivitiesRepository ActivitiesRepository { get; set; }
        [Inject]
        public ICompanyRepository CompanyRepository { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        #endregion

        #region Event handlers
        protected void listView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTown")
            {
                int townId = int.Parse(e.CommandArgument.ToString());
                this.RemoveTownFromSelectedTowns(townId);
            }
        }
        protected void AddTown_Click(object sender, EventArgs e)
        {
            ListItem item = ddlTown.SelectedItem;
            this.AddTownToSelectedTowns(new TownViewModel { ID = int.Parse(item.Value), Name = item.Text });
            this.ClearErrors();
        }
        public void Page_LoadComplete(object s, EventArgs e)
        {
            if (IsPostBack)
            {
                listView.DataSource = this.GetSelectedTowns();
                listView.DataBind();
            }
        }
        #endregion
        #region Wrappers
        public string ViewStateCompanyName
        {
            get
            {
                if (ViewState["CompanyName"] != null)
                    return ViewState["CompanyName"].ToString();
                return null;
            }
            set
            {
                ViewState["CompanyName"] = value;
            }
        }
        public List<TownViewModel> ViewStateTowns
        {
            get
            {
                if (ViewState["Towns"] == null)
                {
                    var towns = new List<TownViewModel>();
                    ViewState["Towns"] = towns;
                    return towns;
                }
                return (List<TownViewModel>)ViewState["Towns"];
            }
            set
            {
                ViewState["Towns"] = value;
            }
        }
        public void JavaScriptAlert(string message)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), null, "alert('" + message + "');", true);
        }
        private void ClearErrors()
        {
            lblError.Text = "";
        }
        private string ShowErrorMessage(Dictionary<int, string> ErrorList)
        {
            StringBuilder msg = null;
            if (ErrorList.Count > 0)
            {
                msg = new StringBuilder();
                foreach (var error in ErrorList)
                {
                    msg.Append(error.Value);
                }

                lblError.Text = msg.ToString();
            }
            return lblError.Text;
        }
        private void ClearViewState()
        {
            ViewState.Clear();
        }
        public int FromHourDropDownListSelectedIndex
        {
            get
            {
                //Nested controls are disabled on SelectMethod of page life cycle.
                int index = int.Parse(Request.Form["ctl00$ContentPlaceHolder1$formView$ddlFrom"].ToString());
                return index;
            }
        }

        #endregion
        #region ModelBinding methods 
        public List<TownViewModel> GetSelectedTowns()
        {
            return this.ViewStateTowns;
        }
        public List<CountryViewModel> GetCountries()
        {
            using (var db = ContactsRepository)
            {
                List<Country> countries = ContactsRepository.GetCountries();
                List<CountryViewModel> countriesVM = Mapper.Map<List<Country>, List<CountryViewModel>>(countries);
                return countriesVM;
            }
        }
        public List<TownViewModel> GetTowns([Control("ddlCountry")] int? countryId)
        {
            if (countryId.HasValue)
            {
                using (var db = ContactsRepository)
                {
                    List<Town> towns = ContactsRepository.GetTowns(countryId.Value);
                    List<TownViewModel> townsVM = Mapper.Map<List<Town>, List<TownViewModel>>(towns);
                    return townsVM;
                }
            }
            return new List<TownViewModel>();
        }
        public void AddTown(TownViewModel model)
        {
            this.AddTownToSelectedTowns(model);
        }
        public AddCompanyViewModel InitFormView([QueryString]int? companyId)
        {
            List<ActivityViewModel> activitiesVM = null;
            using (var db = ActivitiesRepository)
            {
                List<Activity> activities = db.GetActivities();
                activitiesVM = Mapper.Map<List<Activity>, List<ActivityViewModel>>(activities);
            }


            AddCompanyViewModel model = null;

            if (companyId.HasValue)
            {
                using (var db = CompanyRepository)
                {
                    Company company = CompanyRepository.GetCompanyByID(companyId.Value);
                    model = Mapper.Map<Company, AddCompanyViewModel>(company);
                }
            }
            else
            {
                model = new AddCompanyViewModel
                {
                    Name = this.ViewStateCompanyName
                };
            }
            model.Activities = activitiesVM;

            return model;
        }
        public void Save(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (NoErrors(model))
                {
                    #region Saving data to database

                    var company = new Company
                    {
                        ID = model.ID,
                        Name = model.Name,
                        ActivityID = ++model.ActivityID //SQL index starts from 1, asp dropdownlist index starts from 0
                    };

                    int companyId;
                    using (var db = CompanyRepository)
                    {
                        db.SaveCompany(company);
                        companyId = db.GetLastInsertedIdentity();
                    }

                    using (var db = ContactsRepository)
                    {
                        foreach (var town in this.ViewStateTowns)
                            db.AddCompanyTownLink(companyId, town.ID);
                    }

                    #endregion

                    this.ClearErrors();
                    this.ClearViewState();
                    this.JavaScriptAlert("Компания сохранена успешно");
                }
            }
        }
        #endregion

        public bool NoErrors(AddCompanyViewModel model)
        {
            var errors = new Dictionary<int, string>();

            if (this.ViewStateTowns.Count == 0)
                errors.Add(0, "Выберите город(а), где находится эта компания.<br/>");

            if (errors.Count > 0)
            {
                //To restore CompanyName in input field after postback
                this.ViewStateCompanyName = model.Name;
                this.ShowErrorMessage(errors);
                return false;
            }
            else
                return true;
        }
        private void AddTownToSelectedTowns(TownViewModel model)
        {
            var towns = this.ViewStateTowns;
            TownViewModel town = towns.FirstOrDefault(a => a.ID == model.ID);

            if (town == null)
            {
                towns.Add(model);
                this.ViewStateTowns = towns;
            }
            else
            {
                string msg = "Город \"" + model.Name + "\" уже есть в Вашем списке.";
                this.JavaScriptAlert(msg);
            }
        }
        private void RemoveTownFromSelectedTowns(int townId)
        {
            List<TownViewModel> towns = this.ViewStateTowns;
            var town = towns.Find(a => a.ID == townId);
            if (town != null)
            {
                towns.Remove(town);
                this.ViewStateTowns = towns;
            }
        }
    }
}
