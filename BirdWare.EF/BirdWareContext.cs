using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF
{
    public class BirdWareContext(DbContextOptions<BirdWareContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gruppe>(entity =>
                entity.HasOne(f => f.Familie)
                      .WithMany(g => g.Grupper)
                      .HasForeignKey(g => g.FamilieId));

            modelBuilder.Entity<Art>(entity =>
                entity.HasOne(g => g.Gruppe)
                      .WithMany(g => g.Arter)
                      .HasForeignKey(g => g.GruppeId));

            modelBuilder.Entity<Observation>(entity =>
                entity.HasOne(s => s.Art)
                      .WithMany(o => o.Observationer)
                      .HasForeignKey(s => s.ArtId));

            modelBuilder.Entity<Observation>(entity =>
                entity.HasOne(s => s.Fugletur)
                      .WithMany(o => o.Observationer)
                      .HasForeignKey(s => s.FugleturId));

            modelBuilder.Entity<Fugletur>(entity => 
                entity.HasOne(l => l.Lokalitet)
                      .WithMany(t => t.Fugleture)
                      .HasForeignKey(l => l.LokalitetId));

            modelBuilder.Entity<Lokalitet>(entity =>
                entity.HasOne(r => r.Region)
                .WithMany(t => t.Lokaliteter)
                .HasForeignKey(r => r.RegionId));

            modelBuilder.Entity<SpTripAnalysisResult>().HasNoKey();
            modelBuilder.Entity<spLokaliteterByLatLongResult>().HasNoKey();
            modelBuilder.Entity<spHvorKanJegFindeResult>().HasNoKey();

        }

        public DbSet<Familie> Familie { get; set; }
        public DbSet<Gruppe> Gruppe {get; set;}
        public DbSet<Art> Art {get; set;}
        public DbSet<Observation> Observation {get; set;}
        public DbSet<Fugletur> Fugletur {get; set;}
        public DbSet<Lokalitet> Lokalitet {get; set;}
        public DbSet<Region> Region {get; set;}
        public DbSet<SpTripAnalysisResult> SpTripAnalysisResult {get; set;}
        public DbSet<spLokaliteterByLatLongResult> SpLokaliteterByLatLongResult { get; set;}
        public DbSet<spHvorKanJegFindeResult> SpHvorKanJegFindeResult { get; set;}
    }
}