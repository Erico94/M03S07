using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FichaCadastro.Model
{
    public class FichaCadastroContextDB : DbContext
    {
        public DbSet<DetalheModel> DetalheModels { get; set; }
        public DbSet<FichaModel> FichaModels { get; set; }

        public FichaCadastroContextDB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetalheModel>()
                         .HasOne(h => h.Ficha)
                         .WithMany(w => w.DetalheModels);

            modelBuilder.Entity<DetalheModel>().
                Property(p => p.DataCadastro).
                HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<FichaModel>().
               Property(p => p.DataCadastro).
               HasDefaultValueSql("GETDATE()");


            base.OnModelCreating(modelBuilder);
        }
    }
}
