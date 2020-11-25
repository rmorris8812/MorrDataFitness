// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Configuration;
using Fitness.Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fitness.Database.Api.Data 
{
    class FitnessContext : DbContext
    {
        private static readonly int IdentityStart = 1000;
        private static string _connectionString = null;
        public virtual DbSet<FitnessUser> FitnessUser { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<UserFood> UserFood { get; set; }
        public virtual DbSet<DailyGoal> DailyGoal { get; set; }
        public virtual DbSet<Goal> Goal { get; set; }
        public virtual DbSet<Measurement> Measurement { get; set; }
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
        /// Default.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public FitnessContext(string connectionString) : base()
        {
            _connectionString = connectionString;
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
            if (!string.IsNullOrEmpty(_connectionString))
            {
                return _connectionString;
            }
            var configurationProvider = ConfigurationFactory.GetConfiguration();
            var connectionString = configurationProvider.GetValue("ConnectionString");
            if (connectionString == null)
                throw new ArgumentNullException("Connection string not found");
            return connectionString;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
        }
    }
}
