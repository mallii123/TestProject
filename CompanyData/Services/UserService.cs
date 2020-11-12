﻿using CompanyData.Models;
using CompanyData.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyData.Designs; 

namespace CompanyData.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "admin", Password = "admin" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return null;

            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }

}
