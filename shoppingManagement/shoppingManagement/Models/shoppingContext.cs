using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace shoppingManagement.Models
{
    public partial class shoppingContext : DbContext
    {
        public shoppingContext()
        {
        }

        public shoppingContext(DbContextOptions<shoppingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<SepetUrun> SepetUrun { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=DESKTOP-NBT16JQ\\SQLEXPRESS;Database=shopping;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.Property(e => e.Ad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sehir)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soyad)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sepet>(entity =>
            {
                entity.HasOne(d => d.Musteri)
                    .WithMany(p => p.Sepet)
                    .HasForeignKey(d => d.MusteriId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Sepet_Musteri");
            });

            modelBuilder.Entity<SepetUrun>(entity =>
            {
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tutar).HasColumnType("numeric(6, 2)");

                entity.HasOne(d => d.Sepet)
                    .WithMany(p => p.SepetUrun)
                    .HasForeignKey(d => d.SepetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SepetUrun_Sepet");
            });
        }
    }
}
