using Fitness.Database.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Database.Api
{
    public interface IFitnessService : IDisposable
    {

        #region User
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>The user</returns>
        Task<FitnessUser> GetUserByEmailAsync(string email, CancellationToken token);
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The user</returns>
        Task<FitnessUser> GetUserByIdAsync(long id, CancellationToken token);
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">The email</param>
        /// <returns>The user</returns>
        Task<FitnessUser> GetUserByEmailAsync(string email, string tenantId, CancellationToken token);
        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">The user to insert</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The id of the user</returns>
        Task<long> InsertUserAsync(FitnessUser user, CancellationToken token);
        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">The user to insert</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The id of the user</returns>
        Task<long> InsertUserAdminAsync(FitnessUser user, CancellationToken token);
        /// <summary>
        /// Update the user record.
        /// </summary>
        /// <param name="user">The user to update</param>
        Task UpdateUserAsync(FitnessUser user, CancellationToken token);
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>nada</returns>
        Task DeleteUserAsync(long id, CancellationToken token);
        #endregion
        #region Food
        /// <summary>
        /// Get a food by id
        /// </summary>
        /// <param name="id">The food id</param>
        /// <returns>The user</returns>
        Task<Food> GetFoodByIdAsync(long id, CancellationToken token);
        /// <summary>
        /// Get a page of food items.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        List<Food> GetFood(int startIndex, int maxResults);

        /// <summary>
        /// Search for food item.
        /// </summary>
        /// <param name="search">The search criteria</param>
        /// <returns>A list of matching foods.</returns>
        List<Food> SearchFood(string search);
        /// <summary>
        /// Insert a Food item.
        /// </summary>
        /// <param name="food">The food item to insert</param>
        /// <returns>The id of the food item</returns>
        Task<long> InsertFoodAsync(Food food, CancellationToken token);
        /// <summary>
        /// Update a food item
        /// </summary>
        /// <param name="food">The food item to update</param>
        /// <returns>The if of the food item</returns>
        Task<long> UpdateFoodAsync(Food food, CancellationToken token);
        /// <summary>
        /// Delete a food item.
        /// </summary>
        /// <param name="foodId">The id of the food item to delete</param>
        /// <returns></returns>
        Task<bool> DeleteFoodAsync(long foodId, CancellationToken token);
        #endregion
        #region UserFood
        /// <summary>
        /// Get a page of food items.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        List<UserFood> GetUserFood(int startIndex, int maxResults);
        /// <summary>
        /// Get a page of food items for a given date.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <param name="dateTime">The date the food was consumed</param>
        /// <returns>A list of food items.</returns>
        List<UserFood> GetUserFood(int startIndex, int maxResults, DateTime dateTime);
        /// <summary>
        /// Get a page of food items for a given date.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <param name="dateTime">The date the food was consumed</param>
        /// <param name="meal">The meal the food was consumed (0=breakfast,1=lunch,2=dinner,3=snacks</param>
        /// <returns>A list of food items.</returns>
        List<UserFood> GetUserFood(int startIndex, int maxResults, DateTime dateTime, int meal);
        /// <summary>
        /// Insert a UserFood item.
        /// </summary>
        /// <param name="food">The user food to insert</param>
        /// <returns>The id of the user food</returns>
        Task<long> InsertUserFoodAsync(UserFood food, CancellationToken token);

        #endregion
        #region Goal
        /// <summary>
        /// Get a page of food items.
        /// </summary>
        /// <param name="startIndex">The start index (skip)</param>
        /// <param name="maxResults">The max number of items to return.</param>
        /// <returns>A list of food items.</returns>
        List<Goal> GetUserGoal(int startIndex, int maxResults);
        #endregion
    }
}
