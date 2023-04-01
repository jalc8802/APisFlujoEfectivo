using backend_tareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace backend_tareas.Context
{
  public class AplicationDbContext : DbContext
  {
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
    {

    }
    public DbSet<Cuenta> cuentas { get; set; }
    public DbSet<Transaccion> transacciones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>().ToTable("cuentas", "dbo");
            modelBuilder.Entity<Cuenta>(entity => {
                entity.HasKey(e => new { e.id }); entity.Property(e => e.id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Transaccion>().ToTable("transacciones", "dbo");
            modelBuilder.Entity<Cuenta>(entity => {
                entity.HasKey(e => new { e.id }); entity.Property(e => e.id).ValueGeneratedOnAdd();
            });
        }
    }
}
