using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF
{
    public class BirdWareContext : DbContext
    {
        //Used by Unit Testing
        public BirdWareContext()
        {
            
        }

        public BirdWareContext(DbContextOptions<BirdWareContext> options) : base(options)
        {
            
        }

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

        public virtual DbSet<Familie> Familie { get; set; }
        public virtual DbSet<Gruppe> Gruppe {get; set;}
        public virtual DbSet<Art> Art {get; set;}
        public virtual DbSet<Observation> Observation {get; set;}
        public virtual DbSet<Fugletur> Fugletur {get; set;}
        public virtual DbSet<Lokalitet> Lokalitet {get; set;}
        public virtual DbSet<Region> Region {get; set;}
        public virtual DbSet<SpTripAnalysisResult> SpTripAnalysisResult {get; set;}
        public virtual DbSet<spLokaliteterByLatLongResult> SpLokaliteterByLatLongResult { get; set;}
        public virtual DbSet<spHvorKanJegFindeResult> SpHvorKanJegFindeResult { get; set;}
    }
}