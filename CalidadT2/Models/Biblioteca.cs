using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Models
{
    public class Biblioteca
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
        public int Estado { get; set; }
        public Usuario Usuario { get; set; }
        public Libro Libro { get; set; }
    }
}
