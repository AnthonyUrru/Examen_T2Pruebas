using CalidadT2.ModelMocks.Interfaces;
using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.ModelMocks.Services
{
    public class LibroController_Service : ILibroController_Service
    {
        private AppBibliotecaContext _context;
        public LibroController_Service(AppBibliotecaContext context)
        {
            this._context = context;
        }
        public void Comentario(Comentario comment,Usuario usuario)
        {
            comment.Fecha = DateTime.Now;
            comment.UsuarioId = usuario.Id;
           
            _context.Comentarios.Add(comment);

            var libron = _context.Libros.Where(o => o.Id == comment.LibroId).FirstOrDefault();
            libron.Puntaje = (libron.Puntaje + comment.Puntaje) / 2;

            _context.SaveChanges();
        }

        public List<Libro> LibrosList()
        {
            var listLBook=_context.Libros.Include(a => a.Autor).ToList();
            return listLBook;
        }

        public Libro Obtener_Book(int id)
        {
            var Book = _context.Libros.Include(o => o.Autor).Include(o => o.Comentarios.Select(x => x.Usuario)).Where(o => o.Id == id)
                  .FirstOrDefault();
            return Book;
        }
    }
}
