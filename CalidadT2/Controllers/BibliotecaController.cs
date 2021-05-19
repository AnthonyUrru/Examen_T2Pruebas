using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Constantes;
using CalidadT2.ModelMocks.Services;
using CalidadT2.Models;
using CalidadT2.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private readonly AppBibliotecaContext app;
        private readonly ICookie_Service _cook;
        private readonly IRepositorio_Context _repos;

        public BibliotecaController(IRepositorio_Context repos, ICookie_Service cook)
        {
            this._cook = cook;
            this._repos = repos;
        }
        [HttpGet]
        public IActionResult Index()
        {
            Usuario user = LoggedUser();

            var model = _repos.Mostrar_Biblio(user);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int libro)
        {
            Usuario user = LoggedUser();
            _repos.TempData(TempData);
            _repos.Add_newBook_Biblio(libro,user);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            Usuario user = LoggedUser();
            _repos.TempData(TempData);
            var book = _repos.GetBiblioteca(libroId, user);
            _repos.Guardar_Leyendo(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            //Usuario user = LoggedUser();
            Usuario user = LoggedUser();
            _repos.TempData(TempData);
            var book = _repos.GetBiblioteca(libroId, user);
            _repos.Guardar_Terminado(book);
//
            return RedirectToAction("Index");
        }

        private Usuario LoggedUser()
        {
            _cook.SetHttpContext(HttpContext);
            var claim = _cook.Obtener_ClaimPrincipal();
            var user = _repos.Get_UsserLogged(claim);
            return user;
           
        }
    }
}
