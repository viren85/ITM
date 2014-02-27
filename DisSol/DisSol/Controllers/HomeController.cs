using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DisSol.Models;  //Models Namespace
using ITM.LogManager;

namespace DisSol.Controllers
{
    public class HomeController : Controller
    {
        string userName, password;

        [HttpGet]
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {

            try
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    DoctorDetailsModel dbm = new DoctorDetailsModel().UserAuthentication(userName, password);
                    if (dbm != null)
                    {
                        Session["DocName"] = dbm.UserName;
                        Session["DocId"] = dbm.Id;

                        return RedirectToAction("AddPatient", "Disease");
                    }
                    else
                    {
                        TempData["Error"] = "Invalid Username or Password.";
                    }

                }
                else
                {
                    TempData["Error"] = "Please enter Username or Password.";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }



            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {


            var model = new DoctorDetailsModel()
            {
                Areas = GetAreas()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(DoctorDetailsModel doctorDetailsModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    DoctorDetailsModel dbm = new DoctorDetailsModel().GetDoctorDetailByUserName(doctorDetailsModel.UserName);
                    if (dbm == null)
                    {
                        doctorDetailsModel.CreateDate = DateTime.Now;
                        doctorDetailsModel.Save();
                        TempData["Success"] = "You have successfully registered";

                        ModelState.Clear();
                        //making form empty after submission of the data.
                        var model1 = new DoctorDetailsModel()
                        {
                            Areas = GetAreas()
                        };
                        return View(model1);
                    }
                    else
                    {
                        TempData["Error"] = "User Name already exist. Please choose another User name.";

                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            var model = new DoctorDetailsModel()
            {
                Areas = GetAreas()
            };

            return View(model);
        }

        public IEnumerable<SelectListItem> GetAreas()
        {
            var areaModel = new AreaMasterModel();
            var area = areaModel.GetAreaMasterAll().Select(x =>
                        new SelectListItem
                            {
                                Value = x.AreaId.ToString(),
                                Text = x.AreaName
                            });

            return new SelectList(area, "Value", "Text");

        }

        public ActionResult Logout()
        {
            if (Session["DocName"] != null)
            {
                Session["DocId"] = null;
                Session["DocName"] = null;
                Session.Abandon();
                Session.Clear();
            }

            return RedirectToAction("Login");
        }


    }
}
