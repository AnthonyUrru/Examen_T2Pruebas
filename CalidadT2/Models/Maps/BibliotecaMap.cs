using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Models.Maps
{
    public class BibliotecaMap : IEntityTypeConfiguration<Biblioteca>
    {
        public void Configure(EntityTypeBuilder<Biblioteca> builder)
        {
            builder.ToTable("Biblioteca");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Libro)
                .WithMany()
                .HasForeignKey(o => o.LibroId);
        }
    }
}
