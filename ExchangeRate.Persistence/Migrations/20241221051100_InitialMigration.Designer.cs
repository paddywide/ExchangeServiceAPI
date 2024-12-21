﻿// <auto-generated />
using System;
using ExchangeRate.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExchangeRate.Persistence.Migrations
{
    [DbContext(typeof(ExchangeDatabaseContext))]
    [Migration("20241221051100_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExchangeRate.Domain.Models.CurrencyCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nchar(3)")
                        .IsFixedLength();

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CurrencyCode");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "AUD",
                            DateCreated = new DateTime(2024, 12, 21, 16, 11, 0, 442, DateTimeKind.Local).AddTicks(704),
                            DateModified = new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4809)
                        },
                        new
                        {
                            Id = 2,
                            Code = "USD",
                            DateCreated = new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4975),
                            DateModified = new DateTime(2024, 12, 21, 16, 11, 0, 443, DateTimeKind.Local).AddTicks(4979)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
