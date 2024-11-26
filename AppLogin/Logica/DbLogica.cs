using AppLogin.Data;
using AppLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace AppLogin.Logica
{
    public class DbLogica
    {
        private readonly AppDbContext _context;
        public DbLogica(AppDbContext context) {
            _context=context;
        }

       

            public async Task<List<Usuario>> getAll()
            {
                var users = await _context.Usuarios
                    .Include(u => u.UsuarioRoles)        // Incluir la relación UsuarioRoles
                    .ThenInclude(ur => ur.Rol)           // Incluir la información del Rol (nombre, id)
                    .ToListAsync();
                return users;
            }

           
        public async Task<Usuario> getUser(Usuario usuario) {
           var user =  _context.Usuarios.Include(e=> e.UsuarioRoles).ThenInclude(ur=>ur.Rol).Where(u => u.Clave == usuario.Clave &&u.Correo==usuario.Correo).FirstOrDefault();

            return user;

        }


    }
}
