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
    public class BarriosController : Controller
    {
        private SwitchContext db = new SwitchContext();

        // GET: Barrios
        public ActionResult Index()
        {
            return View(db.Barrios.ToList());
        }

        // GET: Barrios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            return View(barrio);
        }

        // GET: Barrios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Barrios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBarr,NombreBarr")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                db.Barrios.Add(barrio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(barrio);
        }

        // GET: Barrios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBarr,NombreBarr")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barrio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barrio barrio = db.Barrios.Find(id);
            if (barrio == null)
            {
                return HttpNotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Barrio barrio = db.Barrios.Find(id);
            db.Barrios.Remove(barrio);
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
