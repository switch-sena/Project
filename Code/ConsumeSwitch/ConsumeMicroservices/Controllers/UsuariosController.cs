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

        //GET 
        public async Task<ActionResult> Create()
        {
            List<Barrios> barriosList = new List<Barrios>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                
                HttpResponseMessage Res = await client.GetAsync("api/Barrios/GetBarrios");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    barriosList = JsonConvert.DeserializeObject<List<Barrios>>(EmpResponse);
                }
            }

            ViewBag.NombreBarr = new SelectList(barriosList, "IdBarr", "NombreBarr");
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
            Usuarios EmpInfo = new Usuarios();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Usuarios/GetUsuariosById/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<Usuarios>(EmpResponse);
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
                HttpResponseMessage Res = await client.DeleteAsync("api/Usuarios/DeleteUsuarios/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error", "Home");
        }

        //GET
        public async Task<ActionResult> Perfil()
        {
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            UsuariosDTO EmpInfo = new UsuariosDTO();
            List<Barrios> EmpInfo2 = new List<Barrios>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Usuarios/Me");
                HttpResponseMessage Res2 = await client.GetAsync("api/Barrios/GetBarrios");

                if (Res.IsSuccessStatusCode && Res2.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var EmpResponse2 = Res2.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<UsuariosDTO>(EmpResponse);
                    EmpInfo2 = JsonConvert.DeserializeObject<List<Barrios>>(EmpResponse2);
                }
            }
            ViewBag.NombreBarr = new SelectList(EmpInfo2, "IdBarr", "NombreBarr");
            return View(EmpInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuariosDTO usuarios)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Clear();

                    string json = JsonConvert.SerializeObject(usuarios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Usuarios/PostUsuariosDTO", content);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(Usuarios usuarios)
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
                    string json = JsonConvert.SerializeObject(usuarios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PutAsync($"api/Usuarios/UpdateUsuarios/{usuarios.IdUsua}", content);

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

        [HttpPost]
        public async Task<ActionResult> Perfil(UsuariosDTO usuarios)
        {
            try
            {
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
                    string json = JsonConvert.SerializeObject(usuarios);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PutAsync($"api/Usuarios/UpdateUsuariosDTO/{usuarios.IdUsua}", content);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("Error", "Home");
            }
        }
    }
}