using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba_Finaktiva.Models;

namespace Prueba_Finaktiva.Controllers
{
    public class municipioController : Controller
    {
        private prueba_finaktiva_celsoEntities db = new prueba_finaktiva_celsoEntities();

        // GET: municipio
        public ActionResult Index()
        {
            return View(db.municipio.ToList());
        }

        // GET: municipio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // GET: municipio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: municipio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,nombre,estado")] municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.municipio.Add(municipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(municipio);
        }

        // GET: municipio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // POST: municipio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,estado")] municipio municipio)
        {
            List<municipio_region> list_municipio_region = new List<municipio_region>();
            if (ModelState.IsValid)
            {
                db.Entry(municipio).State = EntityState.Modified;
                if (!municipio.estado)
                {
                    list_municipio_region = db.municipio_region.Where(z => z.codigo_municipio == municipio.codigo).ToList();
                    db.municipio_region.RemoveRange(list_municipio_region);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(municipio);
        }

        // GET: municipio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        // POST: municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            municipio municipio = db.municipio.Find(id);
            List<municipio_region> list_municipio_region = new List<municipio_region>();
            list_municipio_region = db.municipio_region.Where(z => z.codigo_municipio == municipio.codigo).ToList();
            db.municipio_region.RemoveRange(list_municipio_region);
            db.municipio.Remove(municipio);
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
