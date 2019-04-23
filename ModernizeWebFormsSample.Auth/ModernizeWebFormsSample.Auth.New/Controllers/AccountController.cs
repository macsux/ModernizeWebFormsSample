using System.Web;
using System.Web.Mvc;
using ModernizeWebFormsSample.Auth.New.Models;
using Steeltoe.Security.Authentication.CloudFoundry;

namespace ModernizeWebFormsSample.Auth.New.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult AuthorizeSSO(string returnUrl)
        {
            // the value for provider must match the value used for AuthenticationType when configuring authentication (in Startup.Auth.cs)
            return new ChallengeResult(CloudFoundryDefaults.AuthenticationScheme, returnUrl ?? "~/User/Profile.aspx");
        }

        public ActionResult AccessDenied()
        {
            ViewData["Message"] = "Insufficient permissions.";
            return AccessDenied();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("~");
        }
    }
}