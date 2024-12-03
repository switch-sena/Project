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
    public class PublicacionesDTOController : Controller
    {
        string apiUrl = ConfigurationManager.AppSettings["Api"].ToString();
        string bearerToken = string.Empty;

        //Get
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

            List<PublicacionesDTO> EmpInfo = new List<PublicacionesDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Publicaciones/GetPublicacionesInfoComp");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<PublicacionesDTO>>(EmpResponse);
                }
            }

            return View(EmpInfo);
        }

        //GET 
        public async Task<ActionResult> Create()
        {
            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            List<Habilidades> habilidadesList = new List<Habilidades>();
            List<Modalidades> modalidadesList = new List<Modalidades>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Habilidades/GetHabilidades");
                HttpResponseMessage Res2 = await client.GetAsync("api/Modalidades/GetModalidades");
                if (Res.IsSuccessStatusCode && Res2.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var EmpResponse2 = Res2.Content.ReadAsStringAsync().Result;
                    habilidadesList = JsonConvert.DeserializeObject<List<Habilidades>>(EmpResponse);
                    modalidadesList = JsonConvert.DeserializeObject<List<Modalidades>>(EmpResponse2);
                }
            }

            ViewBag.NombreHabi = new SelectList(habilidadesList, "IdHabi", "NombreHabi");
            ViewBag.NombreModa = new SelectList(modalidadesList, "IdModa", "NombreModa");
            return View();
        }

        //GET
        public async Task<ActionResult> Completa(int id)
        {

            if (!string.IsNullOrEmpty(Session["BearerToken"].ToString()))
            {
                bearerToken = Session["bearerToken"] as string;
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

            PublicacionesDTO EmpInfo = new PublicacionesDTO();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage Res = await client.GetAsync("api/Publicaciones/GetPublicacionesInfoCompByPubl/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<PublicacionesDTO>(EmpResponse);
                }
            }
            return View(EmpInfo);
        }
        [HttpPost]
        public async Task<ActionResult> Create(PublicacionesDTO publicaciones)
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
                    string json = JsonConvert.SerializeObject(publicaciones);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage Res = await client.PostAsync("api/Publicaciones/PostPublicacionesDTO", content);

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