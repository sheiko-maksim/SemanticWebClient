﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EF
{
    public class AppDbContextFactory
    {
        private const string LocalDb = "LocalDB";

        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = GetConnectionString(LocalDb);
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer();

            return new AppDbContext(optionsBuilder.Options);
        }

        private static string GetConnectionString(string connStrName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("db.config.json")
                .Build();

            return configuration.GetConnectionString(connStrName);
        }
    }
}
