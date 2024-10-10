using ConsumeMicroservices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeMicroservices.Controllers
{
    public class HomeController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
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

        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }

        [HttpPost]
        public async Task<ActionResult> Login(Usuarios model)
        {
            string returnUrl = Url.Action("Index", "Home");
            Token token = new Token();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync("api/Auth/Login", content);

                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<Token>(res);
                    Session["BearerToken"] = token.token;
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}