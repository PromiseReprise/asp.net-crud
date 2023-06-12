using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Data
{
    public class DuomenuKontekstas : DbContext
    {
        public DuomenuKontekstas(DbContextOptions<DuomenuKontekstas> options) : base(options) 
        { 
        }

        public DbSet<Darbuotojas> Darbuotojai { get; set; } = null!;
        public DbSet<Pareiga> Pareigos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Darbuotojas>()
                .HasMany(e => e.Pareigos)
                .WithMany(e => e.Darbuotojai)
                .UsingEntity(t => t.ToTable("DarbuotojuPareigos"));
        }
    }
}
