using ConsumeMicroservices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumeMicroservices.Controllers
{
    public class BarriosController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();
        string bearerToken = string.Empty;

        //GET 
        public async Task<ActionResult> Index()
        {
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            List<Barrios> EmpInfo = new List<Barrios>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Barrios/GetBarrios");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Barrios>>(EmpResponse);
                }
            }

            return View(EmpInfo);
        }

        //GET 
        public ActionResult Create()
        {
            return View();
        }

        //GET 
        public async Task<ActionResult> Update(int id)
        {
            //validacion de que existe el token de inicio
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            //logica para traer la informacion de el barrio por id
            Barrios EmpInfo = new Barrios();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Barrios/GetBarriosById/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<Barrios>(EmpResponse);
                }
            }
            return View(EmpInfo);
        }

        //GET 
        public async Task<ActionResult> Delete(int id)
        {
            //validacion de que existe el token de inicio
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
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
                HttpResponseMessage Res = await client.DeleteAsync("api/Barrios/DeleteBarrios/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Create(Barrios barrios)
        {
            try
            {
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
                    string json = JsonConvert.SerializeObject(barrios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Barrios/PostBarrios", content);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Index", "Home");
            }
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Update(Barrios barrios)
        {
            try
            {
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
                    string json = JsonConvert.SerializeObject(barrios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PutAsync($"api/Barrios/UpdateBarrios/{barrios.IdBarr}", content);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Index", "Home");
            }
        }
    }
}