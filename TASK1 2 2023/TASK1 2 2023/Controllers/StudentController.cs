using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TASK1_2_2023.Models;

namespace TASK1_2_2023.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            List<Student>students = new List<Student>();

            students.Add(new Student() {id=1,StudentName="haitham",StudentMajor="islamic law",Studentfaculity="islamic" });

            students.Add(new Student() { id = 2, StudentName = "Odat", StudentMajor = "engineer", Studentfaculity = "engineer" });

            students.Add(new Student() { id = 3, StudentName = "Malik", StudentMajor = "islamic law", Studentfaculity = "islamic" });


            return View(students);
        }
    }
}