using AppLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLogin.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) {  }



        public DbSet<Usuario>Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(col => col.IdUsuario);
                tb.Property(col=>col.IdUsuario)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
                tb.Property(col => col.NombreCompleto).HasMaxLength(50);
                tb.Property(col => col.Correo).HasMaxLength(50);
                tb.Property(col => col.Clave).HasMaxLength(20);
            });
            modelBuilder.Entity<Usuario>().ToTable("tb_Usuarios");
            modelBuilder.Entity<Rol>(tb =>
            {
                tb.HasKey(col => col.IdRol);
                tb.Property(col => col.IdRol)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).HasMaxLength(50);
              
               
            });
            modelBuilder.Entity<Rol>().ToTable("tb_Roles");

            // setup properties UsuarioRol
            modelBuilder.Entity<UsuarioRol>(tb =>
            {
                tb.HasKey(ur => new { ur.IdUsuario, ur.IdRol });

                tb.HasOne(ur => ur.Usuario)
                  .WithMany(u => u.UsuarioRoles)
                  .HasForeignKey(ur => ur.IdUsuario);

                tb.HasOne(ur => ur.Rol)
                  .WithMany(r => r.UsuarioRoles)
                  .HasForeignKey(ur => ur.IdRol);

                tb.ToTable("UsuarioRol");
            });
        }
    }
   
}
