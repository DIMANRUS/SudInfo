﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SudInfo.EfDataAccessLibrary.Contexts;

#nullable disable

namespace SudInfo.EfDataAccessLibrary.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Computer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("GPU")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("int");

                    b.Property<int>("OS")
                        .HasColumnType("int");

                    b.Property<byte>("RAM")
                        .HasColumnType("tinyint");

                    b.Property<int>("ROM")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("YearRelease")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Computers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Monitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComputerId")
                        .HasColumnType("int");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ScreenResolutionHeight")
                        .HasColumnType("int");

                    b.Property<int>("ScreenResolutionWidth")
                        .HasColumnType("int");

                    b.Property<int>("ScreenSize")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("YearRelease")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Monitors");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Periphery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComputerId")
                        .HasColumnType("int");

                    b.Property<string>("InventarNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Peripheries");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Printer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ComputerId")
                        .HasColumnType("int");

                    b.Property<string>("InventarNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ip")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("IsDecommissioned")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("YearRelease")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComputerId");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Rutoken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDateCertificate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rutokens");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("NumberCabinet")
                        .HasColumnType("int");

                    b.Property<string>("PersonalPhone")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhoneLocal")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("WorkPhone")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Users");
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

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.Computer", b =>
                {
                    b.Navigation("Monitors");

                    b.Navigation("Peripheries");

                    b.Navigation("Printers");
                });

            modelBuilder.Entity("SudInfo.EfDataAccessLibrary.Models.User", b =>
                {
                    b.Navigation("Computers");
                });
#pragma warning restore 612, 618
        }
    }
}
