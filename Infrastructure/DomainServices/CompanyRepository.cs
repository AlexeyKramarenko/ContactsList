using ApplicationCore.DomainServices;
using System;
using System.Collections.Generic;
using ApplicationCore.DomainModel;
using System.Collections;
using System.Linq;
using System.Data.SqlClient;
using NI.Data;

namespace Infrastructure.DomainServices
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public Company GetCompanyByID(int companyId)
        {
            var query = new Query(tableName: "Companies",
                                  condition: (QField)"ID" == new QConst(companyId));

            query.Fields = new[] {
                (QField)"ID",
                (QField)"Name"
            };

            IDictionary dict = db.LoadAllRecords(query)[0];

            var company = new Company
            {
                ID = Convert.ToInt32(dict["Id"]),
                Name = dict["Name"].ToString()
            };
            return company;
        }
        public void RemoveCompanyByID(int companyId)
        {
            var query = new Query(tableName: "Company_Towns",
                                  condition: (QField)"CompanyID" == new QConst(companyId));

            var result = db.Delete(query);

            query = new Query(tableName: "Companies",
                              condition: (QField)"ID" == new QConst(companyId));

            result = db.Delete(query);
        }
        public void SaveCompany(Company company)
        {
            if (company.ID > 0)
            {
                var query = new Query(tableName: "Companies",
                                      condition: new QueryConditionNode((QField)"ID", Conditions.In, new QConst(company.ID)));

                var fields = new Hashtable() {
                    { "ID", company.ID },
                    { "Name", company.Name },
                    { "ActivityID", company.ActivityID }
                   };

                int result = db.Update(query, fields);
            }
            else
            {
                var fields = new Hashtable() {
                    { "Name", company.Name },
                    { "ActivityID", company.ActivityID }
                };

                db.Insert(tableName: "Companies", data: fields);
            }
        }
        public void UpdateCompanyName(string name, int companyId)
        {
            var query = new Query(tableName: "Companies",
                                  condition: new QueryConditionNode((QField)"ID", Conditions.In, new QConst(companyId)));

            var fields = new Hashtable() {
                { "Name", name }
            };

            int result = db.Update(query, fields);
        }
        public int GetLastInsertedIdentity()
        {
            string query = "SELECT IDENT_CURRENT('Companies');";
            int Id;
            using (var conn = new SqlConnection(this.connectionString))
            {
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                Id = int.Parse(result.ToString());
            }
            return Id;
        }
        public List<Company> GetCompanies(int maximumRows, int startRowIndex, string sortByExpression)
        {
            int currentPageNumber = 1 + startRowIndex / maximumRows;

            string query = @"SELECT c.ID, c.Name, t.Name as TownName, a.ActivityName 
                                    FROM Companies c  
                                         INNER JOIN Company_Towns ct ON c.ID = ct.CompanyID
                                         INNER JOIN Towns t ON t.ID = ct.TownID
                                         INNER JOIN Activities a ON a.ID = c.ActivityID ORDER BY " + sortByExpression +
                                      " OFFSET ((" + currentPageNumber + "- 1) * " + maximumRows + ") ROWS FETCH NEXT " + maximumRows + " ROWS ONLY";

            var list = new List<IDictionary>();

            db.ExecuteReader(query, (reader) =>
            {
                while (reader.Read())
                {
                    var data = new Hashtable();
                    for (int i = 0; i < reader.FieldCount; i++)
                        data[reader.GetName(i)] = reader.GetValue(i);
                    list.Add(data);
                }
            });

            List<Company> companies = list.Select(d => new Company
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["Name"].ToString(),
                TownName = d["TownName"].ToString(),
                ActivityName = d["ActivityName"].ToString()
            }).ToList();

            return companies;
        }
        public List<Company> GetCompanies(int maximumRows, int startRowIndex, out int totalRowCount, string sortByExpression)
        {
            //var q = new Query(tableName: "Companies");
            //q.Sort = new QSort[] { new QSort(sortByExpression) };
            //q.StartRecord = startRowIndex;
            //q.RecordCount = maximumRows;
            int currentPageNumber =1 + startRowIndex / maximumRows;
            string query = @"SELECT c.ID, c.Name, t.Name as TownName, a.ActivityName,
                                    total_count = COUNT(*) OVER()
                                    FROM Companies c  
                                         INNER JOIN Company_Towns ct ON c.ID = ct.CompanyID
                                         INNER JOIN Towns t ON t.ID = ct.TownID
                                         INNER JOIN Activities a ON a.ID = c.ActivityID ORDER BY " + sortByExpression +
                                         " OFFSET ((" + currentPageNumber + "- 1) * " + maximumRows + ") ROWS FETCH NEXT " + maximumRows + " ROWS ONLY";

            var list = new List<IDictionary>();

            db.ExecuteReader(query, (reader) =>
            {
                while (reader.Read())
                {
                    var data = new Hashtable();
                    for (int i = 0; i < reader.FieldCount; i++)
                        data[reader.GetName(i)] = reader.GetValue(i);
                    list.Add(data);
                }
            });

            IDictionary dict = list.FirstOrDefault();
            if (dict != null && dict.Count > 0)
                totalRowCount = Convert.ToInt32(dict["total_count"]);
            else
                totalRowCount = 0;

            List<Company> companies = list.Select(d => new Company
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["Name"].ToString(),
                TownName = d["TownName"].ToString(),
                ActivityName = d["ActivityName"].ToString()
            }).ToList();

            return companies;
        }

        public List<Company> GetSortedCompanyList(int rowCount, string sortByExpression)
        {
            var query = string.Format(@"SELECT TOP({0}) c.ID, c.Name, t.Name as TownName, a.ActivityName 
                                                FROM Companies c  
                                                     INNER JOIN Company_Towns ct ON c.ID = ct.CompanyID
                                                     INNER JOIN Towns t ON t.ID = ct.TownID
                                                     INNER JOIN Activities a ON a.ID = c.ActivityID ORDER BY {1}", rowCount, sortByExpression);

            var list = new List<IDictionary>();

            db.ExecuteReader(query, (reader) =>
            {
                while (reader.Read())
                {
                    var data = new Hashtable();
                    for (int i = 0; i < reader.FieldCount; i++)
                        data[reader.GetName(i)] = reader.GetValue(i);
                    list.Add(data);
                }
            });

            List<Company> companies = list.Select(d => new Company
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["Name"].ToString(),
                TownName = d["TownName"].ToString(),
                ActivityName = d["ActivityName"].ToString()
            }).ToList();

            return companies;
        }
    }
}
