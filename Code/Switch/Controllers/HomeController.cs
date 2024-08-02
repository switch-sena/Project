using Newtonsoft.Json;
using Switch.Context;
using Switch.Models;
using Switch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Switch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authService = new AuthorizationService();

        public HomeController()
        {
        }

        public ActionResult Index(LoginViewModel vm)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            string returnUrl = Url.Action("Index", "Home");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            Usuario usuario = new Usuario();
            var result = _authService.Auth(model.Correo, model.Clave, out usuario);
            switch (result)
            {
                case AuthResults.Success:
                    CookieUpdate(usuario);
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                case AuthResults.PasswordNotMatch:
                    return RedirectToAction("Index", "Home");
                case AuthResults.NotExists:
                    return RedirectToAction("Index", "Home");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void CookieUpdate(Usuario usuario)
        {
            var ticket = new FormsAuthenticationTicket(2,
                usuario.NombreUsua,
                DateTime.Now,
                DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                false,
                JsonConvert.SerializeObject(usuario)
            );
            Session["Username"] = usuario.NombreUsua;
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)) { };
            Response.AppendCookie(cookie);
        }
    }
}