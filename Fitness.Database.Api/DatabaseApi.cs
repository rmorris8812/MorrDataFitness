using Fitness.Database.Api.Data;
using Fitness.Database.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fitness.Database.Api
{
    public class DatabaseApi : IDisposable
    {
        private readonly FitnessContext _context;

        public DatabaseApi()
        {
            _context = new FitnessContext();
        }
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>The user</returns>
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
