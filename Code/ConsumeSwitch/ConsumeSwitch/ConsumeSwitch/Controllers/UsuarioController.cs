using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ConsumeSwitch.Models;
using System.Threading.Tasks;
using System.Text;

namespace ConsumeSwitch.Controllers
{
    public class UsuarioController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();

        // GET: Usuario
        public async Task<ActionResult> Index()
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

            return View(EmpInfo);
        }
        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public async Task<ActionResult> Create(Usuarios usuario)
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
                    string json = JsonConvert.SerializeObject(usuario);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Usuario/PostUsuario", content);

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