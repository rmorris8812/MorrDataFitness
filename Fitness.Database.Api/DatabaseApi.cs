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
        public FitnessUser GetUserByEmail(string email)
        {
            return _context.fitnessuser.FirstOrDefault(u => u.Email == email);
        }
        /// <summary>
        /// Update the user record.
        /// </summary>
        /// <param name="user">The user to update</param>
        public void UpdateUser(FitnessUser user)
        {
            var existingUser = _context.fitnessuser.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.FirstName = user.LastName;
                existingUser.Password = user.Password;
                existingUser.Token = user.Token;

                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
