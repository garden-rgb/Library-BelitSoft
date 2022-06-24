using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BookLibrary.DAL.DatabaseSecret
{
    public class SampleContextFactory: IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(@"D:\.NET BelitSoft\BookStore\BookStore");
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new StoreContext(optionsBuilder.Options);
        }
    }
}
