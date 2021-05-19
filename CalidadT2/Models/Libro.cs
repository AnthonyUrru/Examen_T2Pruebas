using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Imagen { get; set; }
        public int AutorId { get; set; }
        public double Puntaje { get; set; }
        public Autor Autor { get; set; }

        public List<Comentario> Comentarios { get; set; }
    }
}
