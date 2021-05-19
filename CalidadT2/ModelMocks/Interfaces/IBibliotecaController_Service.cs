using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.ModelMocks.Interfaces
{
    public interface IBibliotecaController_Service
    {
        List<Biblioteca> Biblioteca(Usuario usuario);
        void Agregar_newBook(int libro, Usuario usuario);
        void Agregar_(int libroId, Usuario usuario);
        void Marcar_Terminado(int libroId, Usuario usuario);
    }
}
