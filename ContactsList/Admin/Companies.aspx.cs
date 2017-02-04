using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using AutoMapper;
using ContactsList.Admin.Models;
using ContactsList.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace ContactsList.Admin
{
    public partial class Companies : System.Web.UI.Page
    {
        [Inject]
        public ICompanyRepository CompanyRepository { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Enable twitter bootstrap for DataPager control
            WebControl.DisabledCssClass = "customDisabledClassName";
        }

        public List<CompanyItemViewModel> GetCompanies(int maximumRows, int startRowIndex, out int totalRowCount, [Control] string sortByExpression)
        {

            List<Company> companies = CompanyRepository.GetCompanies(maximumRows, ++startRowIndex, out totalRowCount, sortByExpression);
            List<CompanyItemViewModel> companiesVM = Mapper.Map<List<Company>, List<CompanyItemViewModel>>(companies);
            return companiesVM;

        }
        public void UpdateCompanyName(CompanyItemViewModel model)
        {
            CompanyRepository.UpdateCompanyName(model.Name, model.ID);

        }
        public void RemoveCompany(int ID)
        {
            CompanyRepository.RemoveCompanyByID(ID);
        }

        protected void Page_Unload(object sender, EventArgs e)
        { 
            CompanyRepository.Dispose();
        }
    }
}