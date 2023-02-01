using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task31_1_2023.Controllers
{
    public class MosabController : Controller
    {
        // GET: Mosab
        public ActionResult Index()
        {
            return View();
        }
        public string MyAge()
        {
            return "my age is 26";


        }
        public string NyName()
        {

            return "my Name is Mosab";
        }

        public string Myspecialization()
        {
            return "my major is civil engineer";
        }
        public FileResult File()
        {

            var path = Server.MapPath("~/Content/j.png");
            return File(path, "png");

        }
    }
}