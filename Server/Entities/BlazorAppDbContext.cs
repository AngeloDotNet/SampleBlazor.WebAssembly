using Microsoft.EntityFrameworkCore;

namespace DemoBlazorApp.Server.Entities
{
    public partial class BlazorAppDbContext : DbContext
    {
        public BlazorAppDbContext(DbContextOptions<BlazorAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonaEntity> Persone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaEntity>(entity =>
            {
                entity.ToTable("Persone");
                entity.HasKey(persona => persona.PersonaId);
            });
        }
    }
}