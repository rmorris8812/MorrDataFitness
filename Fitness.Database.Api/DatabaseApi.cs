// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Database.Api.Data;
using Fitness.Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Database.Api
{
    public class DatabaseApi : IFitnessService
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private static readonly FitnessContext _context = new FitnessContext();

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
        public static void ValidateUser(FitnessUser user)
        {
            if (user == null)
            {
                var message = "User object is required";
                _logger.Error(message);
                throw new ArgumentNullException(message);
            }
            if (string.IsNullOrEmpty(user.TenantId))
            {
                var message = "User tenant id is required";
                _logger.Error(message);
                throw new ArgumentNullException(message);
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                var message = "User email is required";
                _logger.Error(message);
                throw new ArgumentNullException(message);
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                var message = "User password is required";
                _logger.Error(message);
                throw new ArgumentNullException(message);
            }
            if (string.IsNullOrEmpty(user.FirstName))
            {
                var message = "User first name is required";
                _logger.Error(message);
                throw new ArgumentNullException(message);
            }
        }
        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">The user to insert</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The id of the user</returns>
        public async Task<long> InsertUserAsync(FitnessUser user, CancellationToken token)
        {
            ValidateUser(user);

            user.UserRole = "user";
            _context.FitnessUser.Add(user);
            await _context.SaveChangesAsync(token);
            return user.Id;
        }
        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">The user to insert</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The id of the user</returns>
        public async Task<long> InsertUserAdminAsync(FitnessUser user, CancellationToken token)
        {
            ValidateUser(user);

            user.UserRole = "adminuser";
            _context.FitnessUser.Add(user);
            await _context.SaveChangesAsync(token);
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
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>nada</returns>
        public async Task DeleteUserAsync(long id, CancellationToken token)
        {
            var user = await GetUserByIdAsync(id, token);
            if (user != null)
            {
                _context.FitnessUser.Remove(user);
                await _context.SaveChangesAsync(token);
            }
            else
            {
                _logger.Error(string.Format(CultureInfo.InvariantCulture, "User {0} not found", id));
            }
        }
        #endregion
        #region Food
        /// <summary>
        /// Get a food by id
        /// </summary>
        /// <param name="id">The food id</param>
        /// <returns>The user</returns>
        public Task<Food> GetFoodByIdAsync(long id, CancellationToken token)
        {
            return _context.Food.FirstOrDefaultAsync(u => u.FoodId == id, token);
        }
        /// <summary>
        /// Get a page of food items.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        public List<Food> GetFood(int startIndex, int maxResults)
        {
            return _context.Food.Skip(startIndex).Take(maxResults).ToList<Food>();
        }

        /// <summary>
        /// Search for food item.
        /// </summary>
        /// <param name="search">The search criteria</param>
        /// <returns>A list of matching foods.</returns>
        public List<Food> SearchFood(string search)
        {
            var foods = _context.Food.Where(f => f.Name.Contains(search)).ToList<Food>();
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
        #region UserFood
        /// <summary>
        /// Get a page of food items.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        public List<UserFood> GetUserFood(long userId, int startIndex, int maxResults)
        {
            return _context.UserFood.Where(f => f.UserId == userId).Skip(startIndex).Take(maxResults).ToList<UserFood>();
        }
        public List<UserFood> GetUserFood(long userId, int startIndex, int maxResults, DateTime dateTime)
        {
            return _context.UserFood.Where(f => f.DateConsumed.Date == dateTime.Date && f.UserId == userId).Skip(startIndex).Take(maxResults).ToList<UserFood>();
        }
        public List<UserFood> GetUserFood(long userId, int startIndex, int maxResults, DateTime dateTime, int meal)
        {
            return _context.UserFood.Where(f => f.DateConsumed.Date == dateTime.Date && f.Meal == meal && f.UserId == userId).Skip(startIndex).Take(maxResults).ToList<UserFood>();
        }
        public async Task<long> InsertUserFoodAsync(UserFood food, CancellationToken token)
        {
            _context.UserFood.Add(food);
            await _context.SaveChangesAsync();
            return food.UserFoodId;
        }
        #endregion
        #region Goal
        /// <summary>
        /// Get a page of goals.
        /// </summary>
        /// <param name="userId">The id of user</param>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        public Goal GetUserGoal(long userId)
        {
            return _context.Goal.Where(g => g.UserId == userId).FirstOrDefault();
        }
        /// <summary>
        /// Create a user goal
        /// </summary>
        /// <param name="goal">The goal to create</param>
        /// <returns>The goal's id</returns>
        public long CreateUserGoal(Goal goal)
        {
            _context.Goal.Add(goal);
            _context.SaveChanges();
            return goal.GoalId;
        }
        /// <summary>
        /// Update the user's goal.
        /// </summary>
        /// <param name="goal">The goal to update</param>
        /// <returns>True if the goal exists, false if the user does not have a goal.</returns>
        public bool UpdateUserGoal(Goal goal)
        {
            if (GetUserGoal(goal.UserId) == null)
            {
                return false;
            }
            _context.Goal.Update(goal);
            _context.SaveChanges();
            return true;
        }
        #endregion
        #region DailyGoal
        /// <summary>
        /// Get the user's daily goals
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user's daily goal</returns>
        public DailyGoal GetDailyGoal(long userId)
        {
            return _context.DailyGoal.Where(g => g.UserId == userId).FirstOrDefault();
        }
        /// <summary>
        /// Create a user goal
        /// </summary>
        /// <param name="goal">The goal to create</param>
        /// <returns>The goal's id</returns>
        public long CreateDailyGoal(DailyGoal goal)
        {
            _context.DailyGoal.Add(goal);
            _context.SaveChanges();
            return goal.GoalId;
        }
        /// <summary>
        /// Update the user's goal.
        /// </summary>
        /// <param name="goal">The goal to update</param>
        /// <returns>True if the goal exists, false if the user does not have a goal.</returns>
        public bool UpdateDailyGoal(DailyGoal goal)
        {
            if (GetDailyGoal(goal.UserId) == null)
            {
                return false;
            }
            _context.DailyGoal.Update(goal);
            _context.SaveChanges();
            return true;
        }
        #endregion
        #region Measurement
        public Measurement GetMeasurement(long userId)
        {
            return _context.Measurement.Where(m => m.UserId == userId).FirstOrDefault();
        }

        public List<Measurement> GetMeasurements(long userId, int startIndex, int maxResults)
        {
            return _context.Measurement.Where(m => m.UserId == userId).Skip(startIndex).Take(maxResults).ToList();
        }

        public long CreateMeasurement(Measurement measurement)
        {
            _context.Measurement.Add(measurement);
            _context.SaveChanges();
            return measurement.MeasurementId;
        }

        public bool UpdateMeasurement(Measurement measurement)
        {
            if (GetMeasurement(measurement.UserId) == null)
            {
                return false;
            }
            _context.Measurement.Update(measurement);
            _context.SaveChanges();
            return true;
        }
        #endregion
        public void Dispose()
        {
            // _context?.Dispose();
        }
    }
}
