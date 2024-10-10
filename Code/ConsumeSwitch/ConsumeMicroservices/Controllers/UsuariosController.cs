using ConsumeMicroservices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeMicroservices.Controllers
{
    public class UsuariosController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        public async Task<ActionResult> Index()
        {
            string bearerToken = string.Empty;
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error","Home");
            }

            List<Usuarios> EmpInfo = new List<Usuarios>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Usuarios/GetUsuarios");
                if (Res.IsSuccessStatusCode) 
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Usuarios>>(EmpResponse);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Usuarios usuarios)
        {
            try
            {
                string bearerToken = string.Empty;
                if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
                {
                    bearerToken = Session["BearerToken"] as string;
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                    string json = JsonConvert.SerializeObject(usuarios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Usuarios/PostUsuario", content);

                    if (Res.IsSuccessStatusCode)
                    {

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}