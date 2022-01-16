using DemoBlazorApp.Shared.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorApp.Server.Models.Services.Infrastructure
{
    public partial class BlazorAppDbContext : DbContext
    {
        public BlazorAppDbContext(DbContextOptions<BlazorAppDbContext> options)
            : base(options)
        { 
        }

        public virtual DbSet<Persona> Persone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persone");
                entity.HasKey(persona => persona.PersonaId);
            });
        }
    }
}
