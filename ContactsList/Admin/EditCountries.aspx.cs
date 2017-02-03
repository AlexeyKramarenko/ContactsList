﻿using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using AutoMapper;
using ContactsList.Admin.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContactsList.Admin
{
    public partial class EditCountries : System.Web.UI.Page
    {
        [Inject]
        public IContactsRepository ContactsRepository { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
         
        public void AddCountry(AddCountryViewModel model)
        {
            using (var db = ContactsRepository)
            {
                db.AddCountry(model.Name);
            }
        }
        public List<CountryViewModel> GetCountries()
        {
            using (var db = ContactsRepository)
            {
                List<Country> countries = db.GetCountries();
                List<CountryViewModel> countriesVM = Mapper.Map<List<Country>, List<CountryViewModel>>(countries);
                return countriesVM;
            }
        }
        public void UpdateCountryName(CountryViewModel model)
        {
            using (var db = ContactsRepository)
            {
                db.UpdateCountryName(model.Name, model.ID);
            }
        }
        public void RemoveCountry(int ID)
        {
            using (var db = ContactsRepository)
            {
                db.RemoveCountryByID(ID);
            }
        } 
        protected void CountriesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
                            string selectedCountry = e.Row.Cells[1].Text;
                            button.OnClientClick = "if (!confirm('Вы действительно хотите удалить страну \"" + selectedCountry + "\"?')) return false;";
                        }
                    }
                }
            }
        } 
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CountriesGridView.DataBind();
        }
    }
}