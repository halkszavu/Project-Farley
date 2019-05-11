using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Entities;

namespace WebApi.DAL
{
    public class VelhoContext : DbContext
    {
        const string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FarleyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<Person> Populii { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Note> Notes { get; set; }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.AddConsole()
                                                           .AddFilter(ll => ll == LogLevel.Debug));
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
