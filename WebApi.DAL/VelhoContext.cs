using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        public VelhoContext(DbContextOptions<VelhoContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var martialConverter = new EnumToStringConverter<MartialState>();
            modelBuilder
                .Entity<Person>()
                .Property(e => e.MartialState)
                .HasConversion(martialConverter);
            var siblingConverter = new EnumToStringConverter<SiblingState>();
            modelBuilder
                .Entity<Person>()
                .Property(e => e.SiblingState)
                .HasConversion(siblingConverter);
        }
    }
}
