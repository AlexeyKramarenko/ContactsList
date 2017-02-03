using ApplicationCore.DomainModel;
using ContactsList.Admin.Models;
using ContactsList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsList.App_Start
{
    internal class MappingProfile : AutoMapper.Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<Town, TownViewModel>();
            CreateMap<Company, CompanyItemViewModel>();
            CreateMap<Activity, ActivityViewModel>(); 
            CreateMap<AddCompanyViewModel, Company>().ReverseMap();
        }
    }
}
