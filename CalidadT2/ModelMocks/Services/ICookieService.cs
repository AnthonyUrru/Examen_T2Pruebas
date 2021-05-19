using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CalidadT2.ModelMocks.Services
{
    public interface ICookie_Service
    {
        void SetHttpContext(HttpContext httpContext);
        public void Login_User(ClaimsPrincipal claim);
        public Claim Obtener_ClaimPrincipal();
    }
    public class Cookie_Service : ICookie_Service
    {
        private HttpContext httpContext;

        public Claim Obtener_ClaimPrincipal()
        {
            var claim = httpContext.User.Claims.FirstOrDefault();
            return claim;
        }
        public void SetHttpContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public void Login_User(ClaimsPrincipal claim)
        {
            httpContext.SignInAsync(claim);

        }

       
    }
}
