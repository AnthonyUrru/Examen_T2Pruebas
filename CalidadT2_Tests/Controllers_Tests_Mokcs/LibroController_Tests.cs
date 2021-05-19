using CalidadT2.Controllers;
using CalidadT2.ModelMocks.Services;
using CalidadT2.Models;
using CalidadT2.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalidadT2_Tests.Controllers_Tests_Mokcs
{
    public class BookData
    {
        public Libro DataLibro()
        {
            var book = new Libro();
            book.Id = 1;
            book.Nombre = "La comunidad del anillo";
            book.AutorId = 2;
            return book;
        }
        public Comentario ComenData()
        {
            var c = new Comentario();
            c.Texto = "Excelente libro";
            c.Id = 1;
            return c;
        }
    }
    [TestFixture]
    public class LibroController_Tests
    {
        [Test]
        public void Funcion_Add_Comentario()
        {
            var user = new UserData();
            var comment = new BookData();
            var CookMock = new Mock<ICookie_Service>();
            var ReposMock = new Mock<IRepositorio_Context>();
            ReposMock.Setup(o => o.add_comentario(user.userData1(), comment.ComenData()));
            var Controller = new LibroController(ReposMock.Object, CookMock.Object);
            var c2 = new Comentario();
            c2.Texto = "Excelente libro";
            c2.Id = 1;
            var assert=Controller.AddComentario(c2);
            Assert.IsInstanceOf<RedirectToActionResult>(assert);

        }



        [Test]
        public void Detalle_Libro_ID()
        {
            var b = new BookData();
            var RepoMock = new Mock<IRepositorio_Context>();
            var MockCoock = new Mock<ICookie_Service>();
            RepoMock.Setup(o => o.Detalle_Book(b.DataLibro().Id));
            var Controller = new LibroController(RepoMock.Object, MockCoock.Object);
            var assert = Controller.Details(1);
            Assert.IsInstanceOf<ViewResult>(assert);
        }
       
       
    }
}
