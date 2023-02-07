using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taskhaithem.Models;

namespace taskhaithem.Controllers
{
    public class resturentController : Controller
    {
        RestuarentEntities1 rest=new RestuarentEntities1(); 
        // GET: resturent
        public ActionResult Index()
        {

            var products = rest.products.ToList();
            var category=rest.Categories.ToList();
           
            return View(Tuple.Create(products,category));
        }

        public PartialViewResult _CATEGORY()
        {
            
            var d = rest.Categories;
            return PartialView("_CATEGORY", d);
        }
    }
}