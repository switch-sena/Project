using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Switch.Context;
using Switch.Models;

namespace Switch.Controllers
{
    public class UsuariosController : Controller
    {
        private SwitchContext db = new SwitchContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(/*db.Usuarios.ToList()*/);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            var barrios = db.Barrios.ToList();
            List<Genero> generos = new List<Genero>();
            generos.Add(new Genero() { Id = 1, Nombre = "Masculino" });
            generos.Add(new Genero() { Id = 2, Nombre = "Femenino" });

            ViewBag.Barrios = new SelectList(barrios, "IdBarr", "NombreBarr");
            ViewBag.Generos = new SelectList(generos, "Nombre", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsua,NombreUsua,ApellidoUsua,GeneroUsua,FechaNacimientoUsua,CelularUsua,CorreoUsua,ClaveUsua,CorreoElectronicoUsua,LinksRsUsua,CopiaIdBarr")] Usuario usuario)
        {
            try
            {
                usuario.CreacionFechaUsua = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Create", "Usuarios");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create", "Usuarios");
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var barrios = db.Barrios.ToList();
            List<Genero> generos = new List<Genero>();
            generos.Add(new Genero() { Id = 1, Nombre = "Masculino" });
            generos.Add(new Genero() { Id = 2, Nombre = "Femenino" });

            ViewBag.Barrios = new SelectList(barrios, "IdBarr", "NombreBarr");
            ViewBag.Generos = new SelectList(generos, "Nombre", "Nombre");
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsua,NombreUsua,ApellidoUsua,GeneroUsua,FechaNacimientoUsua,CelularUsua,CorreoUsua,ClaveUsua,CorreoElectronicoUsua,LinksRsUsua,CopiaIdBarr,CreacionFechaUsua,ModificacionFechaUsua")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
