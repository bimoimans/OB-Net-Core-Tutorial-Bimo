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
        }

        public DbSet<Masakan> Masakans { get; set; }
        //public DbSet<Author> Authors { get; set; }
    }
}
