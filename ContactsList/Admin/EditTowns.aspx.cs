using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using AutoMapper;
using ContactsList.Admin.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactsList.Admin
{
    public partial class EditTowns : System.Web.UI.Page
    {
        [Inject]
        public IContactsRepository ContactsRepository { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        public List<CountryViewModel> GetCountries()
        {
            List<Country> countries = ContactsRepository.GetCountries();
            List<CountryViewModel> countriesVM = Mapper.Map<List<Country>, List<CountryViewModel>>(countries);
            return countriesVM;
        }

        public void AddTown(AddTownViewModel model, [Control("ddlCountry")]int CountryID)
        {
            ContactsRepository.AddTown(model.Name, CountryID);
        }
        public List<TownViewModel> GetTowns([Control("ddlCountry")]int? CountryID)
        {
            List<TownViewModel> townsVM = null;
            int countryId = 1;
            if (CountryID.HasValue)
                countryId = CountryID.Value;
            List<Town> towns = ContactsRepository.GetTowns(countryId);
            townsVM = Mapper.Map<List<Town>, List<TownViewModel>>(towns);
            return townsVM;

        }
        public void RemoveTown(int ID)
        {
            ContactsRepository.RemoveTownByID(ID);
        }
        public void UpdateTownName(TownViewModel town)
        {
            ContactsRepository.UpdateTownName(town.Name, town.ID);
        }
        protected void TownsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlFieldCell cell in e.Row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        var button = control as LinkButton;
                        if (button != null && button.CommandName == "Delete")
                        {
                            string selectedTown = e.Row.Cells[1].Text;
                            button.OnClientClick = "if (!confirm('Вы действительно хотите удалить запись \"" + selectedTown + "\"?')) return false;";
                        }
                    }
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            TownsGridView.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            ContactsRepository.Dispose();
        }
    }
}