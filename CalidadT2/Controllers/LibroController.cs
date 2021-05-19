using System;
using System.Linq;
using CalidadT2.ModelMocks.Services;
using CalidadT2.Models;
using CalidadT2.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class LibroController : Controller
    {
        private readonly AppBibliotecaContext app;
        private readonly IRepositorio_Context _repos;
        private readonly ICookie_Service _cook;
        public LibroController(IRepositorio_Context repos,ICookie_Service cook)
        {
            this._repos = repos;
            this._cook = cook;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var detalle_Book = _repos.Detalle_Book(id);
            /*var model = app.Libros
                .Include("Autor")
                .Include("Comentarios.Usuario")
                .Where(o => o.Id == id)
                .FirstOrDefault();
            */

            return View(detalle_Book);
        }

        [HttpPost]
        public IActionResult AddComentario(Comentario comentario)
        {
            Usuario user = LoggedUser();
            _repos.add_comentario(user,comentario);
            /*comentario.UsuarioId = user.Id;
            comentario.Fecha = DateTime.Now;
            app.Comentarios.Add(comentario);

            var libro = app.Libros.Where(o => o.Id == comentario.LibroId).FirstOrDefault();
            libro.Puntaje = (libro.Puntaje + comentario.Puntaje) / 2;

            app.SaveChanges();
            */
            return RedirectToAction("Details", new { id = comentario.LibroId });
        }

        private Usuario LoggedUser()
        {
            /*var claim = HttpContext.User.Claims.FirstOrDefault();
            var user = app.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            */

            _cook.SetHttpContext(HttpContext);
            var claimss = _cook.Obtener_ClaimPrincipal();
            var user = _repos.Get_UsserLogged(claimss);
            return user;
        }
    }
}
