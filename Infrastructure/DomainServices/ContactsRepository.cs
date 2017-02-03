using ApplicationCore.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.DomainModel;
using System.Collections;
using NI.Data;

namespace Infrastructure.DomainServices
{
    public class ContactsRepository : BaseRepository, IContactsRepository
    {
        public void AddCountry(string name)
        {
            var fields = new Hashtable() {
               { "Name", name }
            };

            db.Insert(tableName: "Countries",
                      data: fields);
        }
        public void AddTown(string name, int countryId)
        {
            var fields = new Hashtable() {
                { "Name", name },
                { "CountryID", countryId }
            };

            db.Insert(tableName: "Towns",
                      data: fields);
        }
        public List<Country> GetCountries()
        {
            var query = new Query(tableName: "Countries")
            {
                Fields = new[] {
                    (QField)"ID",
                    (QField)"Name"
                }
            };

            IDictionary[] dict = db.LoadAllRecords(query);

            List<Country> countries = dict.Select(d => new Country
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["Name"].ToString()
            }).ToList();

            return countries;
        }
        public void RemoveCountryByID(int countryId)
        {
            var query = new Query(tableName: "Countries",
                                  condition: (QField)"ID" == new QConst(countryId));

            int result = db.Delete(query);
        }
        public void UpdateCountryName(string name, int countryId)
        {
            var query = new Query(tableName: "Countries",
                                  condition: new QueryConditionNode((QField)"ID", Conditions.In, new QConst(countryId)));

            var fields = new Hashtable() {
                { "Name", name }
            };

            int result = db.Update(query, fields);
        }
        public List<Town> GetTowns(int countryID)
        {
            var query = new Query(tableName: "Towns",
                                  condition: (QField)"CountryID" == new QConst(countryID));

            query.Fields = new[] {
                (QField)"ID",
                (QField)"Name"
            };

            IDictionary[] dict = db.LoadAllRecords(query);

            List<Town> towns = dict.Select(d => new Town
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["Name"].ToString()
            }).ToList();

            return towns;
        }
        public void RemoveTownByID(int townId)
        {
            var query = new Query(tableName: "Company_Towns",
                                condition: (QField)"TownID" == new QConst(townId));

            int result = db.Delete(query);

            query = new Query(tableName: "Towns",
                                  condition: (QField)"ID" == new QConst(townId));

            result = db.Delete(query);
        }
        public void UpdateTownName(string name, int townId)
        {
            var query = new Query(tableName: "Towns",
                                 condition: new QueryConditionNode((QField)"ID", Conditions.In, new QConst(townId)));

            var fields = new Hashtable() {
                { "Name", name }
            };

            int result = db.Update(query, fields);
        }
        public void AddCompanyDayLink(int companyId, int dayId)
        {
            var fields = new Hashtable() {
                { "CompanyID", companyId },
                { "DayID", dayId }
            };

            db.Insert(tableName: "Company_Days",
                      data: fields);
        }
        public void AddCompanyTownLink(int companyId, int townId)
        {
            var fields = new Hashtable() {
                { "CompanyID", companyId },
                { "TownID", townId }
            };

            db.Insert(tableName: "Company_Towns",
                      data: fields);
        }
    }
}
