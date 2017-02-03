using ApplicationCore.DomainModel;
using ApplicationCore.DomainServices;
using AutoMapper;
using ContactsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactsList.Controllers
{
    public class DefaultController : Controller
    {
        ICompanyRepository companyRepository;
        IMapper mapper;

        public DefaultController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            using(var db = companyRepository)
            {
                List<Company> companies = db.GetCompanies(maximumRows: 10, startRowIndex: 1, sortByExpression: "ID");
                List<CompanyItemViewModel> companiesVM = mapper.Map<List<Company>, List<CompanyItemViewModel>>(companies);
                return View("Index", companiesVM);
            } 
        }

        [HttpGet]
        public JsonResult GetNextCompanyList(int maximumRows, int startRowIndex, string sortByExpression)
        {
            int totalRowCount;
            using (var db = companyRepository)
            {
                List<Company> companies = db.GetCompanies(maximumRows, startRowIndex, out totalRowCount, sortByExpression);
                List<CompanyItemViewModel> companiesVM = mapper.Map<List<Company>, List<CompanyItemViewModel>>(companies);
                bool isLimit = (totalRowCount - companiesVM.Count * startRowIndex <= 0) ? true : false;
                return Json(new { companiesVM, isLimit }, JsonRequestBehavior.AllowGet);
            }
        }
          
        [HttpGet]
        public JsonResult GetSortedCompanyList(int rowCount, string sortByExpression)
        {
            using (var db = companyRepository)
            {
                List<Company> companies = db.GetSortedCompanyList(rowCount, sortByExpression);
                List<CompanyItemViewModel> companiesVM = mapper.Map<List<Company>, List<CompanyItemViewModel>>(companies);
                return Json(new { companiesVM }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}