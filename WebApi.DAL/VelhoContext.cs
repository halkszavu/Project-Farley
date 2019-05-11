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
        public DbSet<Person> Populii { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Note> Notes { get; set; }

        public VelhoContext(DbContextOptions<VelhoContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Meeting>()
                .Property(m => m.Date)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.Notes)
                .IsRequired();

            modelBuilder.Entity<Note>()
                .Property(n => n.Time)
                .HasDefaultValue(DateTime.Now);

            #region Seeding
            modelBuilder.Entity<Person>().HasData(
                new Person
                    { ID = 1, Name = "Bálint", MartialState = MartialState.Single, SiblingState = SiblingState.Middle },
                new Person
                    { ID = 2, Name = "Zsófia", MartialState = MartialState.Single, SiblingState = SiblingState.Eldest }
                );

            modelBuilder.Entity<Meeting>().HasData(
                new Meeting
                    { ID = 1, Date = new DateTime(2019, 5, 10), Notes = "Zenekar" },
                new Meeting
                    { ID = 2, Date = new DateTime(2019, 5, 9), Notes = "Házi átvétel", Place = "BME L épület" }
                );

            modelBuilder.Entity<Note>().HasData(
                new Note { ID = 1, PersonID = 1, Notes = "barbarbarbar" }
                );

            modelBuilder.Entity<PersonMeeting>().HasData(
                new PersonMeeting { ID = 1, PersonID = 1, MeetingID = 1 },
                new PersonMeeting { ID = 2, PersonID = 2, MeetingID = 1 }
                );
            #endregion

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
