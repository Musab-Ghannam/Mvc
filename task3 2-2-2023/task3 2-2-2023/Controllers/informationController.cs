using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using task3_2_2_2023.Models;

namespace task3_2_2_2023.Controllers
{
    public class informationController : Controller
    {
        private JordanInfoEntities1 db = new JordanInfoEntities1();

        // GET: information
        public ActionResult Index()
        {
            return View(db.information.ToList());



        }

        [HttpPost]
        public ActionResult Index(string FNAME ,string SEARCH)

            
        {
            if (SEARCH == "FNAME") { 
            var search=db.information.Where(c =>c.First_Name.Contains(FNAME));
            
            return View(search.ToList());
            }else if(SEARCH == "LNAME")
            {

                var search = db.information.Where(c => c.First_Name.Contains(FNAME));

                return View(search.ToList());

            }
            else if(SEARCH == "Email")
            {
                var search = db.information.Where(c => c.First_Name.Contains(FNAME));

                return View(search.ToList());

            }

            return View(db.information.ToList());
        }

        // GET: information/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            information information = db.information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // GET: information/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,First_Name,Last_Name,EMail,Phone,Age,Job_title,Gender,img,Cv")] information information, HttpPostedFileBase img, HttpPostedFileBase Cv)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    img.SaveAs(path);
                    information.img = fileName;
                }
                if (Cv != null && Cv.ContentLength > 0)
                {
                    var fileName = Cv.FileName;
                    var path = Path.Combine(Server.MapPath("~/files"), fileName);
                    Cv.SaveAs(path);
                    information.Cv= fileName;
                }



                db.information.Add(information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(information);
        }

        // GET: information/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            information information = db.information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,First_Name,Last_Name,EMail,Phone,Age,Job_title,Gender")] information information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(information);
        }

        // GET: information/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            information information = db.information.Find(id);
            if (information == null)
            {
                return HttpNotFound();
            }
            return View(information);
        }

        // POST: information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            information information = db.information.Find(id);
            db.information.Remove(information);
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
