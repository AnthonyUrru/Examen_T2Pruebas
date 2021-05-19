using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Models.Maps
{
    public class LibroMap : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.ToTable("Libro");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Autor)
                .WithMany()
                .HasForeignKey(o => o.AutorId);

            builder.HasMany(o => o.Comentarios)
                .WithOne()
                .HasForeignKey(o => o.LibroId);
        }
    }
}
