using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zapatillas1.zapatillas1.Data;
using zapatillas1.zapatillas1.Models;

namespace zapatillas1.Models
{
    public class UsuariosController : Controller
    {
        private readonly EshopDbContext _context;

        public UsuariosController(EshopDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string pass)
        {
            //GUARDAMOS LA URL A LA QUE DEBEMOS REDIGIRIR AL USUARIO
            // var urlIngreso = TempData["UrlIngreso"] as string;
            var urlIngreso = TempData["Index"] as string;

            //Verifivamos que ambos estén informados
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(pass))
            {
                //Verificamos que existe el usuario
                var user = _context.Usuarios.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    //Verificamos que coincida la contraseña

                    if (pass.Equals(user.Password))
                    {
                        //Creamos los claims (credencial de acceso con info del usuario
                        ClaimsIdentity identidad = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        //Agregamos la credencial el nombre de usuario
                        identidad.AddClaim(new Claim(ClaimTypes.Name, user.Email));

                        //Agregamos la credencial el nombre del admin
                        identidad.AddClaim(new Claim(ClaimTypes.GivenName, user.Email));

                        //Agragamos a la credencial el rol
                        identidad.AddClaim(new Claim(ClaimTypes.Role, user.Id_rol.ToString()));

                        ClaimsPrincipal principal = new ClaimsPrincipal(identidad);

                        //Ejecutamos el login
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrEmpty(urlIngreso))
                        {
                            TempData["is_admin"] = true;
                            return Redirect(urlIngreso);
                        }
                        else
                        {
                            //Redirigimos a la pagina principal
                            return RedirectToAction("Index", "Home");

                        }

                    }

                }

            }

            ViewBag.ErrorEnLogin = "Verifique el usuario y la contraseña";
            TempData["UrlIngreso"] = urlIngreso;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        // public IActionResult Registrarse()
        // {

        //     if (TempData["is_admin"].Equals("is_admin"))
        //     {
        //         return View();
        //     }

        // }
        [Authorize]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Registrarse(Usuario usuario, string pass)
        {
            if (ModelState.IsValid)
            {

                usuario.Password = pass;
                usuario.Id_rol = 2;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));

            }
            return View(usuario);
        }

        // private int biggestId

        // GET: Usuarios
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_usuario,Id_rol,Email,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id_usuario,Id_rol,Email,Password")] Usuario usuario)
        {
            if (id != usuario.Id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id_usuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id_usuario == id);
        }
    }
}
