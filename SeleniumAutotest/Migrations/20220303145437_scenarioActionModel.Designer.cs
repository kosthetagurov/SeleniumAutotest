﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeleniumAutotest.Data;

#nullable disable

namespace SeleniumAutotest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220303145437_scenarioActionModel")]
    partial class scenarioActionModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.Scenario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.ScenarioAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("ContinueOnError")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ScenarioId");

                    b.ToTable("ScenarioActions");
                });

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.ScenarioJournal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ScenarioId");

                    b.ToTable("ScenarioJournals");
                });

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.ScenarioAction", b =>
                {
                    b.HasOne("SeleniumAutotest.Core.Scenarios.Scenario", "Scenario")
                        .WithMany("ScenarioActions")
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scenario");
                });

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.ScenarioJournal", b =>
                {
                    b.HasOne("SeleniumAutotest.Core.Scenarios.Scenario", "Scenario")
                        .WithMany()
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scenario");
                });

            modelBuilder.Entity("SeleniumAutotest.Core.Scenarios.Scenario", b =>
                {
                    b.Navigation("ScenarioActions");
                });
#pragma warning restore 612, 618
        }
    }
}