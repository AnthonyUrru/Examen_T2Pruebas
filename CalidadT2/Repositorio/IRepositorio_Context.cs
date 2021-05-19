using CalidadT2.Constantes;
using CalidadT2.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CalidadT2.Repositorio
{
    public interface IRepositorio_Context
    {
        public Usuario Get_UsserLogged(Claim claim);
        public Usuario Usser_lookfor(string user, string password);
        public void add_comentario(Usuario user, Comentario coment);
        public Libro Detalle_Book(int id);
        public List<Biblioteca> Mostrar_Biblio(Usuario us);
        public void Add_newBook_Biblio(int id,Usuario us);
        public Biblioteca GetBiblioteca(int libroId, Usuario us);
        public void Guardar_Terminado(Biblioteca bi);
        public void Guardar_Leyendo(Biblioteca bi);
        public void TempData(ITempDataDictionary TempData);

    }

    public class Repositorio_Context : IRepositorio_Context
    {
        private readonly AppBibliotecaContext _context;
        private ITempDataDictionary _tData;
        public Repositorio_Context(AppBibliotecaContext context)
        {
            this._context = context;
        }

        public void add_comentario(Usuario user, Comentario coment)
        {
            coment.UsuarioId = user.Id;
            coment.Fecha = DateTime.Now;
            _context.Comentarios.Add(coment);

            var libro = _context.Libros.Where(o => o.Id == coment.LibroId).FirstOrDefault();
            libro.Puntaje = (libro.Puntaje + coment.Puntaje) / 2;
            _context.SaveChanges();
        }
        public void TempData(ITempDataDictionary tData)
        {
            this._tData = tData;
        }

        public void Add_newBook_Biblio(int id, Usuario us)
        {
            var biblioteca = new Biblioteca
            {
                LibroId = id,
                UsuarioId = us.Id,
                Estado = ESTADO.POR_LEER
            };

            _context.Bibliotecas.Add(biblioteca);
            _context.SaveChanges();
            _tData["SuccessMessage"] = "Se añadio el libro a su biblioteca";
        }

        public Libro Detalle_Book(int id)
        {
            var libroDetalle = _context.Libros.Include("Autor").Include("Comentarios.Usuario").Where(o => o.Id == id)
               .FirstOrDefault();
            return libroDetalle;
        }

        public Biblioteca GetBiblioteca(int libroId, Usuario us)
        {
            return _context.Bibliotecas
                .Where(o => o.LibroId == libroId && o.UsuarioId == us.Id)
                .FirstOrDefault();
        }

        public Usuario Get_UsserLogged(Claim claim)
        {
            var user = _context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }

        public void Guardar_Leyendo(Biblioteca bi)
        {
            bi.Estado = ESTADO.LEYENDO;
            _context.SaveChanges();
            _tData["SuccessMessage"] = "Se marco como leyendo el libro";
        }

        public void Guardar_Terminado(Biblioteca bi)
        {
            bi.Estado = ESTADO.TERMINADO;
            _context.SaveChanges();
            _tData["SuccessMessage"] = "Se marco como libro terminado";
        }

        public List<Biblioteca> Mostrar_Biblio(Usuario us)
        {
            return _context.Bibliotecas
                .Include(o => o.Libro.Autor)
                .Include(o => o.Usuario)
                .Where(o => o.UsuarioId == us.Id)
                .ToList();
        }

        public Usuario Usser_lookfor(string user, string password)
        {
            var usuario = _context.Usuarios.Where(o => o.Username == user && o.Password == password).FirstOrDefault();
            return usuario;
        }
    }
    
}
