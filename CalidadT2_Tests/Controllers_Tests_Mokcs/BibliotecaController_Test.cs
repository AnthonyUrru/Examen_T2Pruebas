using CalidadT2.Controllers;
using CalidadT2.ModelMocks.Services;
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
    [TestFixture]
    public class BibliotecaController_Test
    {
        [Test]
        public void Add_Books()
        {
            var MockCook = new Mock<ICookie_Service>();
            var ReposMock = new Mock<IRepositorio_Context>();
            var controller = new BibliotecaController(ReposMock.Object, MockCook.Object);
            var assert = controller.Add(It.IsAny<int>());
            Assert.IsInstanceOf<RedirectToActionResult>(assert);
        }
        [Test]
        public void MostrarTodosLosLibrosConAutor()
        {
            var ReposMock = new Mock<IRepositorio_Context>();
            var MockCook = new Mock<ICookie_Service>();
            var Controller = new BibliotecaController(ReposMock.Object, MockCook.Object);
            var assert = Controller.Index();
            Assert.IsInstanceOf<ViewResult>(assert);
        }
       
        [Test]
        public void Guardar_Leyendo()
        {
            var ReposMock = new Mock<IRepositorio_Context>();
            var MockCook = new Mock<ICookie_Service>();
            var Controller = new BibliotecaController(ReposMock.Object, MockCook.Object);
            var assert = Controller.MarcarComoLeyendo(It.IsAny<int>());
            Assert.IsInstanceOf<RedirectToActionResult>(assert); ;
        }
        [Test]
        public void Guardar_Terminado()
        {
            var ReposMock = new Mock<IRepositorio_Context>();
            var MockCook = new Mock<ICookie_Service>();
            var Controller = new BibliotecaController(ReposMock.Object, MockCook.Object);
            var assert = Controller.MarcarComoTerminado(It.IsAny<int>());
            Assert.IsInstanceOf<RedirectToActionResult>(assert);
        }
    }
}
