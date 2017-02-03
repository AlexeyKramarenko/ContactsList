using ApplicationCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainServices
{
    public interface IContactsRepository : IDisposable
    {
        void AddCountry(string name);
        void AddTown(string name, int countryId);
        void RemoveCountryByID(int countryId);
        void RemoveTownByID(int townId);
        List<Country> GetCountries();
        List<Town> GetTowns(int countryID);
        void UpdateCountryName(string name, int countryId);
        void UpdateTownName(string name, int townId);
        void AddCompanyDayLink(int companyId, int dayId);
        void AddCompanyTownLink(int companyId, int townId);
  

    }
}
