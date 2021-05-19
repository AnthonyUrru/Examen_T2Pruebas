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
    public class UserData
    {
        public Usuario userData1() {
            var user = new Usuario();
            user.Nombres = "thony";
            user.Password = "012";
            return user;
        }
        public Usuario userData2()
        {
            var user = new Usuario();
            user.Nombres = "raul";
            user.Password = "raulF";
            return user;
        }

    }
    [TestFixture]
    public class AuthController_Tests
    {
        [Test]
        public void Iniciar_SesionCOrrecta()
        {
            
            var RepoMock = new Mock<IRepositorio_Context>();
            var n = new UserData();
            RepoMock.Setup(o => o.Usser_lookfor(n.userData1().Nombres, n.userData1().Password)).Returns(n.userData1());
            var CookieMock = new Mock<ICookie_Service>();
            var authCont = new AuthController(RepoMock.Object, CookieMock.Object);
            var assert = authCont.Login("thony","012");
            Assert.IsInstanceOf<RedirectToActionResult>(assert);

        }
        [Test]
        public void Iniciar_SessionIncorrecta()
        {

            var n = new UserData();
            var userMock = new Mock<IRepositorio_Context>();
            userMock.Setup(o => o.Usser_lookfor(n.userData2().Nombres, n.userData2().Password)).Returns(n.userData2());

            var cookMock = new Mock<ICookie_Service>();
            var authCont = new AuthController(userMock.Object, cookMock.Object);
            var log = authCont.Login("raul", "raulito");

            Assert.IsInstanceOf<ViewResult>(log);
        }

    }
}
