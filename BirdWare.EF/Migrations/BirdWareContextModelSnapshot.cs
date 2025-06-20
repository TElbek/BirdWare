﻿// <auto-generated />
using System;
using BirdWare.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BirdWare.EF.Migrations
{
    [DbContext(typeof(BirdWareContext))]
    partial class BirdWareContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BirdWare.Domain.Entities.Art", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long>("GruppeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Navn")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("SU")
                        .HasColumnType("bit");

                    b.Property<bool>("SetIDK")
                        .HasColumnType("bit");

                    b.Property<bool>("Speciel")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GruppeId");

                    b.ToTable("Art");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Bruger", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bruger");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Familie", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Navn")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Familie");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Fugletur", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Dato")
                        .HasColumnType("datetime2");

                    b.Property<long>("LokalitetId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LokalitetId");

                    b.ToTable("Fugletur");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Gruppe", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long>("FamilieId")
                        .HasColumnType("bigint");

                    b.Property<string>("Navn")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("FamilieId");

                    b.ToTable("Gruppe");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Lokalitet", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Navn")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Lokalitet");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Observation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ArtId")
                        .HasColumnType("bigint");

                    b.Property<string>("Beskrivelse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FugleturId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ArtId");

                    b.HasIndex("FugleturId");

                    b.ToTable("Observation");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Region", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<bool>("Indland")
                        .HasColumnType("bit");

                    b.Property<string>("Navn")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("BirdWare.Domain.Models.SpTripAnalysisResult", b =>
                {
                    b.Property<int>("AnalyseType")
                        .HasColumnType("int");

                    b.Property<int>("ArtId")
                        .HasColumnType("int");

                    b.Property<string>("ArtNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SU")
                        .HasColumnType("bit");

                    b.ToTable("SpTripAnalysisResult");
                });

            modelBuilder.Entity("BirdWare.Domain.Models.spHvorKanJegFindeResult", b =>
                {
                    b.Property<long>("ArtId")
                        .HasColumnType("bigint");

                    b.Property<string>("ArtNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("LokalitetNavn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.ToTable("SpHvorKanJegFindeResult");
                });

            modelBuilder.Entity("BirdWare.Domain.Models.spLokaliteterByLatLongResult", b =>
                {
                    b.Property<int>("AntalTure")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Navn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SpLokaliteterByLatLongResult");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Art", b =>
                {
                    b.HasOne("BirdWare.Domain.Entities.Gruppe", "Gruppe")
                        .WithMany("Arter")
                        .HasForeignKey("GruppeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gruppe");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Fugletur", b =>
                {
                    b.HasOne("BirdWare.Domain.Entities.Lokalitet", "Lokalitet")
                        .WithMany("Fugleture")
                        .HasForeignKey("LokalitetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lokalitet");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Gruppe", b =>
                {
                    b.HasOne("BirdWare.Domain.Entities.Familie", "Familie")
                        .WithMany("Grupper")
                        .HasForeignKey("FamilieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Familie");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Lokalitet", b =>
                {
                    b.HasOne("BirdWare.Domain.Entities.Region", "Region")
                        .WithMany("Lokaliteter")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Observation", b =>
                {
                    b.HasOne("BirdWare.Domain.Entities.Art", "Art")
                        .WithMany("Observationer")
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BirdWare.Domain.Entities.Fugletur", "Fugletur")
                        .WithMany("Observationer")
                        .HasForeignKey("FugleturId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Art");

                    b.Navigation("Fugletur");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Art", b =>
                {
                    b.Navigation("Observationer");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Familie", b =>
                {
                    b.Navigation("Grupper");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Fugletur", b =>
                {
                    b.Navigation("Observationer");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Gruppe", b =>
                {
                    b.Navigation("Arter");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Lokalitet", b =>
                {
                    b.Navigation("Fugleture");
                });

            modelBuilder.Entity("BirdWare.Domain.Entities.Region", b =>
                {
                    b.Navigation("Lokaliteter");
                });
#pragma warning restore 612, 618
        }
    }
}
