﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SudInfo.EfDataAccessLibrary.Contexts;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    [DbContext(typeof(SudInfoDbContext))]
    [Migration("20230720143948_v2.2.0.8")]
    partial class v2208
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("AppEntityComputer", b =>
                {
                    b.Property<int>("AppsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComputersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppsId", "ComputersId");

                    b.HasIndex("ComputersId");

                    b.ToTable("AppEntityComputer");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.AppEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.AppSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Theme = "Light"
                        });
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Cartridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Remains")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cartridges");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Computer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BreakdownDescription")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("GPU")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBroken")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStock")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OS")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RAM")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ROM")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearRelease")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Computers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Monitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BreakdownDescription")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ComputerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBroken")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("ScreenResolutionHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScreenResolutionWidth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScreenSize")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("YearRelease")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Monitors");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.PasswordEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Periphery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ComputerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Peripheries");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Printer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BreakdownDescription")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ComputerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ip")
                        .HasMaxLength(12)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBroken")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearRelease")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Rutoken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDateCertificate")
                        .HasColumnType("TEXT");

                    b.Property<string>("NumberCertificate")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumberRutoken")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rutokens");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventarNumber")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("OperatingSystem")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PosiitionInServerRack")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ServerRackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ServerRackId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.ServerRack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("InventarNumber")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Position")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Position")
                        .IsUnique();

                    b.ToTable("ServerRacks");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReminderTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PersonalPhone")
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneLocal")
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkPhone")
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppEntityComputer", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.AppEntity", null)
                        .WithMany()
                        .HasForeignKey("AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.Computer", null)
                        .WithMany()
                        .HasForeignKey("ComputersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Computer", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.User", "User")
                        .WithMany("Computers")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Monitor", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.Computer", "Computer")
                        .WithMany("Monitors")
                        .HasForeignKey("ComputerId");

                    b.Navigation("Computer");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Periphery", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.Computer", "Computer")
                        .WithMany("Peripheries")
                        .HasForeignKey("ComputerId");

                    b.Navigation("Computer");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Printer", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.Computer", "Computer")
                        .WithMany("Printers")
                        .HasForeignKey("ComputerId");

                    b.Navigation("Computer");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Rutoken", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Server", b =>
                {
                    b.HasOne("SudInfo.EfDataAccessLibrary.Models.ServerRack", "ServerRack")
                        .WithMany("Servers")
                        .HasForeignKey("ServerRackId");

                    b.Navigation("ServerRack");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Computer", b =>
                {
                    b.Navigation("Monitors");

                    b.Navigation("Peripheries");

                    b.Navigation("Printers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.ServerRack", b =>
                {
                    b.Navigation("Servers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.User", b =>
                {
                    b.Navigation("Computers");
                });
#pragma warning restore 612, 618
        }
    }
}
