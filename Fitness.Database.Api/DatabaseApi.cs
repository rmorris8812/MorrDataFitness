using Fitness.Database.Api.Data;
using Fitness.Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        public Task<FitnessUser> GetUserByEmailAsync(string email, CancellationToken token)
        {
            return _context.FitnessUser.FirstOrDefaultAsync(u => u.Email == email, token);
        }
        /// <summary>
        /// Update the user record.
        /// </summary>
        /// <param name="user">The user to update</param>
        public async Task UpdateUserAsync(FitnessUser user, CancellationToken token)
        {
            var existingUser = await _context.FitnessUser.FirstOrDefaultAsync(u => u.Id == user.Id, token);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Password = user.Password;
                existingUser.Token = user.Token;

                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task<UserRole> GetUserRoleByUserIdAsync(long id, CancellationToken none)
        {
            throw new NotImplementedException();
        }
    }
}
