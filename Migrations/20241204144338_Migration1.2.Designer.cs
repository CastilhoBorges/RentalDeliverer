﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RentalDeliverer.src.Data;

#nullable disable

namespace RentalDeliverer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241204144338_Migration1.2")]
    partial class Migration12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RentalDeliverer.src.Models.Deliverer", b =>
                {
                    b.Property<string>("DelivererId")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CNH")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("CNHImgPath")
                        .HasColumnType("text");

                    b.Property<string>("CNHType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DelivererId");

                    b.HasIndex("CNH")
                        .IsUnique();

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.ToTable("Deliverer", (string)null);
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.Motorcycle", b =>
                {
                    b.Property<string>("MotorcycleId")
                        .HasColumnType("text");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("MotorcycleId");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.ToTable("Motorcycles", (string)null);
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.Rental", b =>
                {
                    b.Property<Guid>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DelivererId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MotorcycleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RentalTypeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RentalId");

                    b.HasIndex("DelivererId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("RentalTypeId");

                    b.ToTable("Rentals", (string)null);
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.RentalType", b =>
                {
                    b.Property<string>("RentalTypeId")
                        .HasColumnType("text");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<int>("Days")
                        .HasColumnType("integer");

                    b.HasKey("RentalTypeId");

                    b.ToTable("RentalTypes", (string)null);
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.Rental", b =>
                {
                    b.HasOne("RentalDeliverer.src.Models.Deliverer", "Deliverer")
                        .WithMany("Rentals")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalDeliverer.src.Models.Motorcycle", "Motorcycle")
                        .WithMany("Rentals")
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalDeliverer.src.Models.RentalType", "RentalType")
                        .WithMany("Rentals")
                        .HasForeignKey("RentalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverer");

                    b.Navigation("Motorcycle");

                    b.Navigation("RentalType");
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.Deliverer", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.Motorcycle", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("RentalDeliverer.src.Models.RentalType", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
