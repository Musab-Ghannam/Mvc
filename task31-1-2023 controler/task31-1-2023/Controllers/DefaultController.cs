using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace task31_1_2023.Controllers
{
    public class DefaultController : Controller
    {

        // GET: Default

        public string download()
        {
            //return "<a href='https://localhost:44381//Default/Index'></a>";
            return "<a href = \"/yazeed.jfif\" download > <img src='/yazeed.jfif'> </a>\r\n        "; }


        public string AboutUs()
        {
            return "AboutUs";
        }
        public string ContactUs()
        {
            return "ContactUs";
        }


    }
  
   
   
}