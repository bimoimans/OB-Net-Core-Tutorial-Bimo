using Microsoft.EntityFrameworkCore;
using RumahMakanPadang.dal.Models;
using System;

namespace RumahMakanPadang.dal
{
    public class RumahMakanPadangDbContext : DbContext 
    {
        public RumahMakanPadangDbContext(DbContextOptions<RumahMakanPadangDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Masakan>().HasIndex(t => t.Nama).IsUnique();
            builder.Entity<Masakan>()
            .HasOne(p => p.Chef)
            .WithMany(b => b.Masakans)
            .HasForeignKey(p => p.ChefKTP)
            .HasPrincipalKey(b => b.KTP);

            builder.Entity<Chef>().HasIndex(c => c.KTP).IsUnique();
            
        }

        public DbSet<Masakan> Masakans { get; set; }
        public DbSet<Chef> Chefs { get; set; }
    }
}
