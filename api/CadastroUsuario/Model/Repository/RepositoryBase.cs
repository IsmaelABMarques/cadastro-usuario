using CadastroUsuario.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuario.Model.Repository
{
    public class RepositoryBase : DbContext
    {
        public RepositoryBase (DbContextOptions<RepositoryBase> options)
            : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasOne(c => c.Status).WithMany(m => m.Usuarios).HasForeignKey(c => c.StatusId);
        }
    }
}
