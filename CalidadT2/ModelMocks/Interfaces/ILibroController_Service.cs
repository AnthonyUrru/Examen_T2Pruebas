using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.ModelMocks.Interfaces
{
    public interface ILibroController_Service
    {
        void Comentario(Comentario comentario,Usuario usuario);
        Libro Obtener_Book(int id);
        List<Libro> LibrosList();
    }
}
