﻿// <auto-generated />
using System;
using Measurments_Service.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Measurments_Service.Migrations
{
    [DbContext(typeof(MeasurmentContext))]
    partial class MeasurmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Measurments_Service.Be.MeasurmentBe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Diastolic")
                        .HasColumnType("integer");

                    b.Property<string>("PatientSSN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Seen")
                        .HasColumnType("boolean");

                    b.Property<int>("Systolic")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Measurments");
                });
#pragma warning restore 612, 618
        }
    }
}
