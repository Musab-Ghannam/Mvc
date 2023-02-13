using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using taskApi13_2_2023.Models;

namespace taskApi13_2_2023.Controllers
{
    public class specializationsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: specializations
        public ActionResult Index()
        {
            var specializations = db.specializations.Include(s => s.Faculty);
            return View(specializations.ToList());
        }

        specialization sp=new specialization();
        public ActionResult special(int id)
        {

            var sp = db.specializations.Where(c=>c.faculty_id==id);
            return View(sp);
        }


        // GET: specializations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            specialization specialization = db.specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // GET: specializations/Create
        public ActionResult Create()
        {
            ViewBag.faculty_id = new SelectList(db.Faculties, "faculty_id", "facultyName");
            return View();
        }

        // POST: specializations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "specialization_id,faculty_id,MajorName")] specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.specializations.Add(specialization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.faculty_id = new SelectList(db.Faculties, "faculty_id", "facultyName", specialization.faculty_id);
            return View(specialization);
        }

        // GET: specializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            specialization specialization = db.specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            ViewBag.faculty_id = new SelectList(db.Faculties, "faculty_id", "facultyName", specialization.faculty_id);
            return View(specialization);
        }

        // POST: specializations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "specialization_id,faculty_id,MajorName")] specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.faculty_id = new SelectList(db.Faculties, "faculty_id", "facultyName", specialization.faculty_id);
            return View(specialization);
        }

        // GET: specializations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            specialization specialization = db.specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // POST: specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            specialization specialization = db.specializations.Find(id);
            db.specializations.Remove(specialization);
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
