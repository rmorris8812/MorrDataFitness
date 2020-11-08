using Fitness.Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Fitness.Database.Api.Data 
{
    class FitnessContext : DbContext
    {
        public virtual DbSet<FitnessUser> FitnessUser { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<UserFood> UserFood { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        /// <summary>
        /// Configure options.
        /// </summary>
        /// <param name="options">The options to use.</param>
        public FitnessContext(DbContextOptions options) : base(options)
        {
        }
        /// <summary>
        /// Default.
        /// </summary>
        public FitnessContext() : base()
        {
        }
        /// <summary>
        /// Set up the connection.
        /// </summary>
        /// <param name="options">The options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(GetConnectionString());
        }
        /// <summary>
        /// Get the connection string from the app settings.
        /// </summary>
        /// <returns>The defined connection string.</returns>
        public static string GetConnectionString()
        {
            var configurationProvider = new JsonConfigurationProvider();
            var connectionString = configurationProvider.GetValue("ConnectionString");
            if (connectionString == null)
                throw new ArgumentNullException("Connection string not found");
            return connectionString;
        }
    }
}
