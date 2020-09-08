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
    public class regionController : Controller
    {
        private prueba_finaktiva_celsoEntities db = new prueba_finaktiva_celsoEntities();

        // GET: region
        public ActionResult Index()
        {
            return View(db.region.ToList());
        }

        // GET: region/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            region region = db.region.Find(id);

            List<municipio_region> lstmunicipiosRegion = new List<municipio_region>();
            lstmunicipiosRegion = db.municipio_region.Where(z => z.codigo_region == id).ToList(); 
            List<municipio> lstMunicipios = new List<municipio>();
            List<municipio> lstMunicipios_2 = new List<municipio>();
            lstMunicipios = db.municipio.Where(x => x.estado == true).ToList(); //solo activos
            foreach (municipio item in lstMunicipios)
            {
                if (lstmunicipiosRegion.FindAll(x => x.codigo_municipio == item.codigo).Count == 1)
                {
                    lstMunicipios_2.Add(item); 
                }
            }

            ViewBag.lstMunicipios = lstMunicipios_2;


            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: region/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: region/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,nombre")] region region)
        {
            if (ModelState.IsValid)
            {
                db.region.Add(region);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(region);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult municipio_region(FormCollection fm)
        {
            try
            {
                if (fm["codigo_region"] == null || fm["nuevo_municipio"] == null)
                {
                    return RedirectToAction("Index");
                }
                int codigo_region = Convert.ToInt32(fm["codigo_region"].ToString());
                int codigo_municipio = Convert.ToInt32(fm["nuevo_municipio"].ToString());
                municipio_region aux = new municipio_region();
                aux.codigo_municipio = codigo_municipio;
                aux.codigo_region = codigo_region;
                db.municipio_region.Add(aux);
                db.SaveChanges();

                return RedirectToAction("Edit", new { id = codigo_region });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            //return View();
        }

        // GET: region/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            region region = db.region.Find(id);
            List<municipio_region> lstmunicipiosRegion = new List<municipio_region>();
            lstmunicipiosRegion = db.municipio_region.Where(z => z.codigo_region == id).ToList(); //Lista de municipios asocaciados a la region
            List<municipio> lstMunicipios = new List<municipio>();
            List<municipio> lstMunicipios_2 = new List<municipio>();
            lstMunicipios = db.municipio.Where(x => x.estado == true).ToList(); //solo activos
            foreach (municipio item in lstMunicipios)
            {
                if (lstmunicipiosRegion.FindAll(x => x.codigo_municipio == item.codigo).Count == 0)
                {
                    lstMunicipios_2.Add(item); // Añadimos solo los municipios que no estran asociados a la region
                }
            }


            if (region == null)
            {
                return HttpNotFound();
            }
            ViewBag.lstmunicipiosRegion = lstmunicipiosRegion;
            ViewBag.lstMunicipios = lstMunicipios_2;
            ViewBag.lstMunicipios_Completo = lstMunicipios;
            return View(region);
        }

        // POST: region/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre")] region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: region/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            region region = db.region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<municipio_region> list_municipio_region = new List<municipio_region>();
            list_municipio_region = db.municipio_region.Where(z => z.codigo_region == id).ToList();
            db.municipio_region.RemoveRange(list_municipio_region);
            region region = db.region.Find(id);
            db.region.Remove(region);
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

        public ActionResult Eliminar_Municipio_De_region(int? id_region, int? id_municipio)
        {
            if (id_region == null || id_municipio == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                municipio_region municipiosRegion = new municipio_region();
                municipiosRegion = db.municipio_region.Where(z => z.codigo_region == id_region && z.codigo_municipio == id_municipio).ToList().FirstOrDefault(); //Lista de municipios asocaciados a la region
                db.municipio_region.Remove(municipiosRegion);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = id_region });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
                //  throw;
            }



        }
    }
}
