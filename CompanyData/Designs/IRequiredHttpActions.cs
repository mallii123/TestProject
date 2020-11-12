using CompanyData.Models;
using System.Collections.Generic;

namespace CompanyData.Designs
{
    public interface ICompanyRequiredHttpActions
    {
        long Create(Company company);
        List<Company> Search(CompanySearchParameters CompanySearchParameters);
        bool Update(Company company);
        bool Delete(long id);
    }
}
