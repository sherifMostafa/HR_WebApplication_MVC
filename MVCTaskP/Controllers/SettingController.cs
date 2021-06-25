using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTaskP.filter;
using MVCTaskP.Models;

namespace MVCTaskP.Controllers
{
    [authutication]
    public class SettingController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: Setting
        public ActionResult Index()
        {
            settings st = db.settings.Where(n => n.Id == 1).FirstOrDefault();
            return View(st);
        }
        [HttpPost]
        public ActionResult ApplySetting(string satarday, string SunDay, string MonDay, string TuesDay, string wednesday, string thursday, string Friday, double over, double deduction)
        {
            string s = "";
            if (satarday == "true")
            {
                s += "satarday , ";
            }
            if (SunDay == "true")
            {
                s += "SunDay , ";
            }
            if (MonDay == "true")
            {
                s += "MonDay , ";
            }
            if (TuesDay == "true")
            {
                s += "TuesDay , ";
            }
            if (wednesday == "true")
            {
                s += "wednesday , ";
            }
            if (thursday == "true")
            {
                s += "thursday , ";
            }
            if (Friday == "true")
            {
                s += "Friday , ";
            }
            settings st = db.settings.Where(n => n.Id == 1).FirstOrDefault();
            st.weekends = s;
            st.over = over;
            st.deduction = deduction;
            db.SaveChanges();
            TempData["Sucess"] = "updated sucessfully ..."; 
            return RedirectToAction("Index");
        }



    }
}