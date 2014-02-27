using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DisSol.Models;

namespace DisSol.Controllers
{
    public class DiseaseController : Controller
    {
        //
        // GET: /Disease/

        [HttpGet]
        public ActionResult AddPatient()
        {

            var model = new DiseaseDetailModel()
            {
                Areas = GetAreas(),
                Disease = GetDisease()
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult AddPatient(DiseaseDetailModel diseaseDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    diseaseDetail.DocId = Convert.ToInt32(Session["DocId"]);
                    
                    

                    diseaseDetail.PatientAddDate = DateTime.Now;
                    diseaseDetail.Save();
                    TempData["Success"] = "Patient added Successfully.";

                    ModelState.Clear();

                    // making form, emty after submission of form
                    
                    

                }
                else
                {
                    //empData["Error"] = "Please provide Required data.";
                }

                var model1 = new DiseaseDetailModel()
                {
                    Areas = GetAreas(),
                    Disease = GetDisease()
                };
                return View(model1);

            }
            catch (Exception ex)
            {

                throw ex;
            }


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

        public IEnumerable<SelectListItem> GetDisease()
        {
            var diseaseModel = new DiseaseMasterModel();
            var disease = diseaseModel.GetDiseaseMasterAll().Select(x =>
                        new SelectListItem
                        {
                            Value = x.Id.ToString(),
                            Text = x.DiseaseName
                        });

            return new SelectList(disease, "Value", "Text");

        }


    }
}
