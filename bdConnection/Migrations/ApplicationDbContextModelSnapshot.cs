﻿// <auto-generated />
using System;
using ClientManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConnectionBd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("ClientManagerDTO.Entity.Client", b =>
                {
                    b.Property<Guid>("clientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("age")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("dateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("married")
                        .HasColumnType("INTEGER");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("registerClient")
                        .HasColumnType("TEXT");

                    b.Property<string>("rut")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("updateClient")
                        .HasColumnType("TEXT");

                    b.HasKey("clientId");

                    b.HasIndex("email")
                        .IsUnique();

                    b.HasIndex("rut")
                        .IsUnique();

                    b.ToTable("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
