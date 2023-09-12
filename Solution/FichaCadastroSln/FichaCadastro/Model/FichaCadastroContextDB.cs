using Microsoft.EntityFrameworkCore;


namespace FichaCadastro.Model
{
    public class FichaCadastroContextDB : DbContext
    {
        public FichaCadastroContextDB(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
