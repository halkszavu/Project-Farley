﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.DAL;

namespace WebApi.DAL.Migrations
{
    [DbContext(typeof(VelhoContext))]
    partial class VelhoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Entities.Meeting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Notes");

                    b.Property<string>("Place");

                    b.HasKey("ID");

                    b.ToTable("Meetings");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Date = new DateTime(2019, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Notes = "Zenekar"
                        },
                        new
                        {
                            ID = 2,
                            Date = new DateTime(2019, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Notes = "Házi átvétel",
                            Place = "BME L épület"
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Notes")
                        .IsRequired();

                    b.Property<int>("PersonID");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 6, 13, 14, 29, 23, 576, DateTimeKind.Local).AddTicks(4581));

                    b.HasKey("ID");

                    b.HasIndex("PersonID");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Notes = "barbarbarbar",
                            PersonID = 1,
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("MartialState")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SiblingState")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Populii");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MartialState = "Single",
                            Name = "Bálint",
                            SiblingState = "Middle"
                        },
                        new
                        {
                            ID = 2,
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MartialState = "Single",
                            Name = "Zsófia",
                            SiblingState = "Eldest"
                        });
                });

            modelBuilder.Entity("WebApi.Entities.PersonMeeting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetingID");

                    b.Property<int>("PersonID");

                    b.HasKey("ID");

                    b.HasIndex("MeetingID");

                    b.HasIndex("PersonID");

                    b.ToTable("PersonMeeting");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            MeetingID = 1,
                            PersonID = 1
                        },
                        new
                        {
                            ID = 2,
                            MeetingID = 1,
                            PersonID = 2
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Note", b =>
                {
                    b.HasOne("WebApi.Entities.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Entities.PersonMeeting", b =>
                {
                    b.HasOne("WebApi.Entities.Meeting", "Meeting")
                        .WithMany("PersonMeetings")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApi.Entities.Person", "Person")
                        .WithMany("PersonMeetings")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
