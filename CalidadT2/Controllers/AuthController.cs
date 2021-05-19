using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CalidadT2.ModelMocks.Services;
using CalidadT2.Models;
using CalidadT2.Repositorio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CalidadT2.Controllers
{
    public class AuthController : Controller
    {

        private readonly IRepositorio_Context _repos;
        private readonly ICookie_Service _cook;
        private readonly AppBibliotecaContext app;
       

        public AuthController(IRepositorio_Context repos,ICookie_Service cook)
        {
            this._repos = repos;
            this._cook = cook;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            /*var usuario = app.Usuarios.Where(o => o.Username == username && o.Password == password).FirstOrDefault();*/
            var usuario = _repos.Usser_lookfor(username,password);
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                _cook.SetHttpContext(HttpContext);
                _cook.Login_User(claimsPrincipal);/*
                HttpContext.SignInAsync(claimsPrincipal);
                */
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
