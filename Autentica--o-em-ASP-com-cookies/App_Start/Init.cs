using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AppAutenticacao.App_Start.Init))]

namespace AppAutenticacao.App_Start
{
    public class Init
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "AppAplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}