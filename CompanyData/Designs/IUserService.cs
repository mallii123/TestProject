using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyData.Models;

namespace CompanyData.Designs
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

}
