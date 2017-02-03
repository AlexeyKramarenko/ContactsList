using ApplicationCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainServices
{
    public interface ICompanyRepository : IDisposable
    {
        List<Company> GetCompanies(int maximumRows, int startRowIndex, string sortByExpression);
        List<Company> GetCompanies(int maximumRows, int startRowIndex, out int totalRowCount, string sortByExpression);
        void UpdateCompanyName(string name, int companyId);
        void RemoveCompanyByID(int companyId);
        Company GetCompanyByID(int companyId);
        void SaveCompany(Company company);
        int GetLastInsertedIdentity();
        List<Company> GetSortedCompanyList(int rowCount, string sortByExpression);
 
    }
}
