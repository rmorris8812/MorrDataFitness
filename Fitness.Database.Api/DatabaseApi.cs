using Fitness.Database.Api.Data;
using Fitness.Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public DatabaseApi(string connectionString)
        {
            _context = new FitnessContext(connectionString);
        }

        #region User
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
        /// Get a user by email
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The user</returns>
        public Task<FitnessUser> GetUserByIdAsync(long id, CancellationToken token)
        {
            return _context.FitnessUser.FirstOrDefaultAsync(u => u.Id == id, token);
        }
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>The user</returns>
        public Task<FitnessUser> GetUserByEmailAsync(string email, string tenantId, CancellationToken token)
        {
            return _context.FitnessUser.FirstOrDefaultAsync(u => u.Email == email && u.TenantId == tenantId, token);
        }
        public async Task<long> InsertUserAsync(FitnessUser user, CancellationToken token)
        {
            _context.FitnessUser.Add(user);
            await _context.SaveChangesAsync(token);
            var role = new UserRole() { Role = "user", UserId = user.Id };
            return user.Id;
        }
        /// <summary>
        /// Update the user record.
        /// </summary>
        /// <param name="user">The user to update</param>
        public async Task UpdateUserAsync(FitnessUser user, CancellationToken token)
        {
            var existingUser = await _context.FitnessUser.FirstOrDefaultAsync(u => u.Id == user.Id && u.TenantId == user.TenantId, token);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Password = user.Password;
                existingUser.Token = user.Token;

                _context.SaveChanges();
            }
        }
        public async Task DeleteUserAsync(long id, CancellationToken token)
        {
            var user = await GetUserByIdAsync(id, token);
            if (user != null)
            {
                var userRole = await _context.UserRole.FirstOrDefaultAsync<UserRole>(u => u.UserId == user.Id, token);
                _context.UserRole.Remove(userRole);
                _context.FitnessUser.Remove(user);
                await _context.SaveChangesAsync(token);
            }
        }
        #endregion
        #region Food
        /// <summary>
        /// Search for food item.
        /// </summary>
        /// <param name="search">The search criteria</param>
        /// <returns>A list of matching foods.</returns>
        public List<Food> SearchFood(string search)
        {
            var query = string.Format(CultureInfo.InvariantCulture, "Name LIKE '%{0}%'", search);
            var foods = _context.Food.FromSqlRaw(query).ToList<Food>();
            return foods;
        }
        /// <summary>
        /// Insert a Food item.
        /// </summary>
        /// <param name="food">The food item to insert</param>
        /// <returns>The id of the food item</returns>
        public async Task<long> InsertFoodAsync(Food food, CancellationToken token)
        {
            _context.Food.Add(food);
            await _context.SaveChangesAsync();
            return food.FoodId;
        }
        /// <summary>
        /// Update a food item
        /// </summary>
        /// <param name="food">The food item to update</param>
        /// <returns>The if of the food item</returns>
        public async Task<long> UpdateFoodAsync(Food food, CancellationToken token)
        {
            _context.Food.Update(food);
            await _context.SaveChangesAsync(token);
            return food.FoodId;
        }
        /// <summary>
        /// Delete a food item.
        /// </summary>
        /// <param name="foodId">The id of the food item to delete</param>
        /// <returns></returns>
        public async Task<bool> DeleteFoodAsync(long foodId, CancellationToken token)
        {
            var food = await _context.Food.FirstOrDefaultAsync<Food>(f => f.FoodId == foodId, token);
            if (food != null)
            {
                _context.Food.Remove(food);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
        /// <summary>
        /// Get the user's role.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The user's role.</returns>
        public async Task<UserRole> GetUserRoleByUserIdAsync(long id, CancellationToken token)
        {
            return await _context.UserRole.FirstOrDefaultAsync<UserRole>(u => u.UserId == id, token);
        }
        /// <summary>
        /// Insert the user role for a given user.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<long> InsertUserRoleAsync(UserRole role, CancellationToken token)
        {
            if (role.UserId == 0)
                throw new ArgumentException("The user id must be set");
            var user = GetUserRoleByUserIdAsync(role.UserId, token);
            if (user == null)
                throw new ArgumentNullException("The user was not found.");

            _context.UserRole.Add(role);
            await _context.SaveChangesAsync(token);
            return role.Id;
        }
    }
}
