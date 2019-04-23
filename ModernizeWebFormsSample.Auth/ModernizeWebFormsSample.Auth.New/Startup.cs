using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Steeltoe.Security.Authentication.CloudFoundry.Owin;

[assembly: OwinStartup(typeof(ModernizeWebFormsSample.Auth.New.Startup))]

namespace ModernizeWebFormsSample.Auth.New
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationConfig.RegisterConfig("development");

            app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ExternalCookie",
                AuthenticationMode = AuthenticationMode.Active,
                CookieName = ".AspNet.ExternalCookie",
                LoginPath = new PathString("/Account/AuthorizeSSO"),
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });

            app.UseCloudFoundryOpenIdConnect(ApplicationConfig.Configuration, "CloudFoundry", ApplicationConfig.LoggerFactory);

//            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
