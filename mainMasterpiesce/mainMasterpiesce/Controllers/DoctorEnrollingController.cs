using mainMasterpiesce.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;




namespace mainMasterpiesce.Controllers
{
    public class DoctorEnrollingController : Controller
    {
       
        //public ActionResult STDform([Bind(Include = "Student_ID,Id,Name,Email,Password,NationalNum,Grad,Pic,Status,PhoneNumber,BirthFile,PersonalIdFile,CertificateFile,Gender,Major_ID,Wallet")] Student student, HttpPostedFileBase Pic1, HttpPostedFileBase PersonalIdFile1, HttpPostedFileBase CertificateFile1, HttpPostedFileBase BirthFile1)
       FindingpeaceEntities db=new FindingpeaceEntities();
        // GET: DoctorEnrolling
        [Authorize(Roles = "doctor")]
     
        public ActionResult EnrollDoctor()
        {
          
            var mainId=User.Identity.GetUserId();
            dynamic data = new ExpandoObject();

            var specializations = db.specializations.ToList();
            var doct=db.doctors.Where(c=>c.Id==mainId).ToList();

         

          
          
            data.Specializations = specializations;
            data.doc = doct;
            //data.doctor = doctors;

            return View(data);


            
        }







        [HttpPost]
        public ActionResult EnrollDoctor([Bind(Include = "Id,locationdoctor,doctorName,email,phoneNumber,qualiification,specialization,startedate,idCardfile,picdoctor,certificationfile,PersonalIdFile,CertificateFile,birthfile,specializationId,statedoctor,earningDoctortotal,AmountsDue,IBAN,Gender,infodoctor,pricePerHour,ratingdoctor,ratingint")] doctor doctorr, string location, string locationLink, string price, string qualif, string Iban, string exp, string info, int specializationId, HttpPostedFileBase PersonalIdFile1, HttpPostedFileBase Certification, HttpPostedFileBase BirthFile1, HttpPostedFileBase ExperienceFile1)
        {
            var doctorId = User.Identity.GetUserId();
            var doctor = db.doctors.FirstOrDefault(d => d.Id == doctorId);

            dynamic data = new ExpandoObject();

            var specializations = db.specializations.ToList();
            var doct = db.doctors.Where(c=>c.Id==doctorId).ToList();
            data.Specializations = specializations;
            data.doc = doct;
            if (ModelState.IsValid)
            {
                if (PersonalIdFile1 != null)
                {
                    //string fileName = Path.GetFileName(image.FileName);
                    string path = Server.MapPath("~/FormalFile/") + PersonalIdFile1.FileName;
                    PersonalIdFile1.SaveAs(path);
                    doctor.idCardfile = PersonalIdFile1.FileName;
                }

                if (Certification != null)
                {
                    //string fileName = Path.GetFileName(cv.FileName);
                    string path = Server.MapPath("~/FormalFile/") + Certification.FileName;
                    Certification.SaveAs(path);
                    doctor.certificationfile = Certification.FileName;
                }
                if (ExperienceFile1 != null)
                {
                    //string fileName = Path.GetFileName(cv.FileName);
                    string path = Server.MapPath("~/FormalFile/") + ExperienceFile1.FileName;
                    ExperienceFile1.SaveAs(path);
                    doctor.experience = ExperienceFile1.FileName;
                }

                if (BirthFile1 != null)
                {
                    //string fileName = Path.GetFileName(cv.FileName);
                    string path = Server.MapPath("~/FormalFile/") + BirthFile1.FileName;
                    BirthFile1.SaveAs(path);
                    doctor.birthfile = BirthFile1.FileName;
                }
                var SPECIALIZATIONNAME = db.specializations.FirstOrDefault(C => C.specializationId == specializationId).namespecialization;
                doctor.pricePerHour = Convert.ToInt16(price);
                doctor.qualiification = qualif;
                doctor.locationdoctor = location + "," + locationLink;
                doctor.specialization= SPECIALIZATIONNAME;
                doctor.infodoctor = info;
                if (specializationId != null)
                {

                    doctor.specializationId = specializationId;
                }


                ViewBag.doctorname = doctor.doctorName;

                string[] nameParts = Regex.Replace(User.Identity.Name, "[^a-zA-Z]+", " ").Split(' ');
                string firstName =nameParts[0];

                db.SaveChanges();

                doctor.statedoctor = 0;

                TempData["swal_message"] = $"Dr-{firstName}\tYour registration has been submitted and is waiting for approval. You will receive an email notification when your account has been accepted.";
                ViewBag.title = "success";
                ViewBag.icon = "success";
                ViewBag.redirectUrl = Url.Action("Index", "mainHome");

                return View("EnrollDoctor", data);
            }



            data.Specializations = specializations;
            data.doc = doct;


            //data.doctor = doctors;
        

            return View("EnrollDoctor", data);



        }

        public ActionResult DoctorDashboard()
        {
            string zoomLink = Session["link"] as string;
            var mainId = User.Identity.GetUserId();
            var doctorId = db.doctors.FirstOrDefault(x => x.Id == mainId).doctorId;

            var appointment = db.appointments.Where(c => c.doctorId == doctorId).ToList();
            var doct = db.doctors.Where(c => c.Id == mainId).ToList();


            var patientcount = db.appointments.Where(c=>c.doctorId==doctorId).Select(l=>l.patientId).Distinct().Count();


            var appointmentcount = db.appointments.Where(c => c.doctorId == doctorId).ToList().Count();

            ViewBag.appointmentcount= appointmentcount;


            ViewBag.patientcount = patientcount;

            if  (TempData["welcome"] != null)
                {
                if (TempData["welcome"].ToString() == "wait")
                {

                    TempData["swal_message"] = $"Your appointment has been confirmed. Please wait for your appointment to start before beginning your session.";

                    ViewBag.title = "Appointment Reminder";
                    ViewBag.icon = "info";
                    ViewBag.redirectUrl = Url.Action("DoctorDashboard");

                }

                var aspid = User.Identity.GetUserId();
                var docname = db.doctors.FirstOrDefault(c => c.Id == aspid).doctorName;


                if (TempData["welcome"].ToString() == "GetStart")
            {
                    TempData["swal_message"] = $"Welcome, Dr-{docname}. Your appointment session has started. We will be creating a Zoom meeting link and sending it to your patient shortly.";

                    ViewBag.title = "Appointment Started";
                    ViewBag.icon = "success";
                    ViewBag.redirectUrl = Url.Action("DoctorDashboard");
                }



            }

            return View(Tuple.Create(doct, appointment));



        }



        public ActionResult MyPatient()
        {
            string zoomLink = Session["link"] as string;
            var mainId = User.Identity.GetUserId();
            var doctorId = db.doctors.FirstOrDefault(x => x.Id == mainId).doctorId;

            var appointment = db.appointments.Where(c => c.doctorId == doctorId).ToList();
            var doct = db.doctors.Where(c => c.Id == mainId).ToList();


            var patientcount = db.appointments.Where(c => c.doctorId == doctorId).Select(l => l.patientId).Distinct();


            var appointmentcount = db.appointments.Where(c => c.doctorId == doctorId).ToList().Count();

            ViewBag.appointmentcount = appointmentcount;


            ViewBag.patientcount = patientcount;


            return View(Tuple.Create(doct, appointment));



        }

        public ActionResult NotAvailable()
        {

            int count = (int)(Session["countarrow"] ?? 0);

            ViewBag.weeks = count;



            ViewBag.weeks = count;

            ViewBag.InputStyle = "color:#20BBBD; font-weight:bold";


            var mainId = User.Identity.GetUserId();
            var doctorId = db.doctors.FirstOrDefault(x => x.Id == mainId).doctorId;

            var appointment = db.appointments.Where(c => c.doctorId == doctorId).ToList();
            var doct = db.doctors.Where(c => c.Id == mainId).ToList();


            var patientcount = db.appointments.Where(c => c.doctorId == doctorId).Select(l => l.patientId).Distinct();


            var appointmentcount = db.appointments.Where(c => c.doctorId == doctorId).ToList().Count();

            ViewBag.appointmentcount = appointmentcount;


            ViewBag.patientcount = patientcount;


            return View(Tuple.Create(doct, appointment));



        }




        public ActionResult plusweeks( string arrow)
        {
            int count = (int)(Session["countarrow"] ?? 0);

            count += 7;
            ViewBag.weeks = count;

            // Store the updated count value in session state
            Session["countarrow"] = count;

















            return RedirectToAction("NotAvailable");
        }
        public ActionResult minusWeek( string arrow)
        {
            int count = (int)(Session["countarrow"] ?? 0);

            if (count > 0)
            {



                count -= 7;
                ViewBag.weeks = count;





                // Store the updated count value in session state
                Session["countarrow"] = count;



            }


            return RedirectToAction("NotAvailable");
        }







        public ActionResult Waiting( string wait)
        {

      

            TempData["welcome"] = "wait";


            return RedirectToAction("DoctorDashboard");
        }







        public ActionResult beforeGetStart(string wait)
        {

            
            TempData["welcome"] = "GetStart";

            return RedirectToAction("DoctorDashboard");
        }







        public ActionResult GetStart(string wait)
        {

            var aspid=User.Identity.GetUserId();
            var specialization = db.doctors.FirstOrDefault(c => c.Id == aspid).specialization1.namespecialization;
            var docid = db.doctors.FirstOrDefault(c => c.Id == aspid).doctorId;
            var apttime = db.appointments.FirstOrDefault(c => c.doctorId == docid).starttime;


            DateTime storedDatt = DateTime.ParseExact(apttime, "h:mm ttdd/MM", CultureInfo.InvariantCulture);

            string topic = specialization;
            string startTime = storedDatt.ToString();
            int duration = 60; // in minutes

            //TempData["welcome"] = "GetStart";

            return RedirectToAction("GenerateZoomLink", new { topic = topic, startTime = startTime, duration = duration });
        }



        //var AspId = User.Identity.GetUserId();
        //var patientId = doc.patients.FirstOrDefault(c => c.Id == AspId).PatiantId;
        ////var doctors = doc.doctors.Where(c => c.doctorId == id).ToList();
        //var patientInfo = doc.patients.Where(c => c.Id == AspId).ToList();
        //var appointmentt = doc.appointments.Where(c => c.patientId == patientId).ToList();

        ////var pricedoctor = doc.doctors.FirstOrDefault(c => c.doctorId == id).pricePerHour;
        //var patient = doc.patients.FirstOrDefault(c => c.Id == AspId).PatiantId;


        //    return View(Tuple.Create(patientInfo, appointmentt));



        public async Task<ActionResult> GenerateZoomLink(string topic, string startTime, int duration)
        {
            string apiKey = "O0HA03Z8RjKy0WLdl4HuoA";
            string apiSecret = "efUaUpNA1q7LV648Tjp25NL8MWjMcwYyBmmh";

            try
            {
                var jwt = await GenerateJwt(apiKey, apiSecret);

                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://api.zoom.us/v2/")
                };
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwt);

                var data = new
                {
                    topic,
                    type = 2,
                    start_time = startTime,
                    duration
                };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("users/me/meetings", content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var meeting = JsonConvert.DeserializeObject<ZoomMeeting>(responseContent);

                TempData["ZoomLink"] = meeting.JoinUrl;
                Session["link"] = TempData["ZoomLink"];




                var aspid = User.Identity.GetUserId();
                var docid = db.doctors.FirstOrDefault(c => c.Id == aspid).doctorId;
                var apttime = db.appointments.Where(c => c.doctorId == docid).Select(c => c.starttime).ToList();
                foreach (var item in apttime)
                {
                    DateTime storedDatt = DateTime.ParseExact(item, "h:mm ttdd/MM", CultureInfo.InvariantCulture);
                    string datbasetimeday = storedDatt.ToString("MM/dd");
                    string today = DateTime.Now.ToString("MM/dd");


                    if (datbasetimeday == today)
                    {
                        if (storedDatt.Hour == DateTime.Now.Hour)
                        {
                            var appointment = db.appointments.FirstOrDefault(c => c.doctorId == docid && c.starttime == item);
                            if (appointment != null)
                            {
                                // Update the appointment's JoinUrl with the Zoom link
                                appointment.JoinUrl = meeting.JoinUrl;
                                db.SaveChanges();



                            }


                        }


                    }
                }



                //string topic = specialization;
                //string startTime = storedDatt.ToString();










                return RedirectToAction("DoctorDashboard");
            }

            catch (HttpRequestException ex)
            {
                // Handle the error
                // You can log the exception details, display an error message to the user, etc.
                // Here's an example of displaying a generic error message:
                ModelState.AddModelError("", "An error occurred while generating the Zoom link. Please try again later.");
                return RedirectToAction("DoctorDashboard");
            }


        }

        private async Task<string> GenerateJwt(string apiKey, string apiSecret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1), // Set expiration time to 1 hour from now
                Issuer = apiKey,
                Audience = "https://api.zoom.us",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSecret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private class ZoomMeeting
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("join_url")]
            public string JoinUrl { get; set; }
        }





















    }
}