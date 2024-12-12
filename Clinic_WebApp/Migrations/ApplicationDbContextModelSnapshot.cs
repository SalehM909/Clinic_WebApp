﻿// <auto-generated />
using System;
using Clinic_WebApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinic_WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clinic_WebApp.Models.Booking", b =>
                {
                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.Property<int>("ClinicID")
                        .HasColumnType("int");

                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("SlotNumber")
                        .HasColumnType("int");

                    b.HasKey("PatientID", "ClinicID");

                    b.HasIndex("ClinicID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Clinic_WebApp.Models.Clinic", b =>
                {
                    b.Property<int>("CID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CID"));

                    b.Property<int>("NumberOfSlots")
                        .HasColumnType("int");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CID");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("Clinic_WebApp.Models.Patient", b =>
                {
                    b.Property<int>("PID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PID"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PID");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Clinic_WebApp.Models.Booking", b =>
                {
                    b.HasOne("Clinic_WebApp.Models.Clinic", "Clinic")
                        .WithMany("Bookings")
                        .HasForeignKey("ClinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clinic_WebApp.Models.Patient", "Patient")
                        .WithMany("Bookings")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Clinic_WebApp.Models.Clinic", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Clinic_WebApp.Models.Patient", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
