using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppLogin.Data;
using AppLogin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using AppLogin.Logica;
using Microsoft.AspNetCore.Authorization;

namespace AppLogin.Controllers
{
    [Authorize]
    public class AccesoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DbLogica db;

        public AccesoController(AppDbContext context)
        {
            _context = context;
            db = new DbLogica(_context);
        }


        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();


        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Correo,Clave")] UsuarioViewModel usuario)
        {


            var u = new Usuario
            {
                Correo = usuario.Correo,
                Clave = usuario.Clave
            };
            var user = new Usuario();
            user = await db.getUser(u);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.NombreCompleto),
                       new Claim("Correo",user.Correo)
                     };

                foreach (var rol in user.UsuarioRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.Rol.Nombre));




                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");

            }
            ViewBag.Mensaje = "Usuario no encontrado";

            return View("Login");


        }


        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("Login", "Acceso");
        }
    }
}

       

