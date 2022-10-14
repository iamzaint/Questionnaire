using StealthLibrary.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stealth.Controllers
{
    public class QuestionnaireController : Controller
    {
        // GET: Questionnaire
        public ActionResult Index()
        {
            tbl_Questionnaire a = new tbl_Questionnaire();
            return View(a);
        }

        public ActionResult create(tbl_Questionnaire a)
        {
            try
            {
                //a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                //a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                //a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                //a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
               

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }


        public ActionResult Save(tbl_Questionnaire model)
        {
            try
            {
                bool check = false;
                if (model.QuestionID > 0)
                {
                    
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = 1;
                    model.CreatedOn = Request.UserHostAddress;
                    //new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    check = model.AddData(model);
                }

                if (check==true)
                {
                    return RedirectToAction("Index");

                }
                return RedirectToAction("create", model);

            }
            catch (Exception ex)
            {

                return View("create");
            }
        }

        public ActionResult Edit(string id)
        {
            try
            {


                tbl_Questionnaire a = new tbl_Questionnaire();
                var obj = a.getAlldataByID(Convert.ToInt32(id));

                //ViewData["Editmode"] = true;
                //if (IDo[1] == "0")
                //{
                //    a.IsView = true;
                //}
               
                a.IsView = true;
                obj.IsView = a.IsView;
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }

        }

        public JsonResult delete(int id)
        {
            try
            {
                tbl_Questionnaire a = new tbl_Questionnaire();

                bool c = a.DeleteData(id);
                if (c)
                {
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}