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
        public async Task<ActionResult> Completa()
        {
            return View();
        }
    }
}