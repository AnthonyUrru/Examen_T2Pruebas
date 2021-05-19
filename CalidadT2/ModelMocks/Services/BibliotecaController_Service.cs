using CalidadT2.Constantes;
using CalidadT2.ModelMocks.Interfaces;
using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.ModelMocks.Services
{
    public class BibliotecaController_Service : IBibliotecaController_Service
    {
        private AppBibliotecaContext _context;
        public BibliotecaController_Service(AppBibliotecaContext context)
        {
            this._context = context;
        }
        public void Agregar_(int libroId, Usuario usuario)
        {
            var b = _context.Bibliotecas.Where(o => o.LibroId==libroId && o.UsuarioId == usuario.Id).FirstOrDefault();
            b.Estado = ESTADO.LEYENDO;
            _context.SaveChanges();
        }

        public void Agregar_newBook(int libro, Usuario usuario)
        {
            var newB = new Biblioteca { LibroId = libro, UsuarioId = usuario.Id, Estado = ESTADO.POR_LEER };
            _context.Bibliotecas.Add(newB);
            _context.SaveChanges();
        }

        public List<Biblioteca> Biblioteca(Usuario usuario)
        {
            var add = _context.Bibliotecas.Include(o => o.Libro.Autor).Include(o => o.Usuario).Where(
                o => o.UsuarioId == usuario.Id).ToList();
            return add;
        }

        public void Marcar_Terminado(int libroId, Usuario usuario)
        {
            var bokSelected = _context.Bibliotecas.Where(o => o.LibroId == libroId && o.UsuarioId == usuario.Id).FirstOrDefault();
            bokSelected.Estado = ESTADO.TERMINADO;
            _context.SaveChanges();

        }
    }
}
