using CompanyData.Models;
using System.Collections.Generic;

namespace CompanyData.Design
{
    interface IRequiredHttpActions<T>
    {
        long Create(T company);
        List<T> Search(CompanySearchParameters CompanySearchParameters);
        bool Update(T company);
        bool Delete(long id);
    }
}
