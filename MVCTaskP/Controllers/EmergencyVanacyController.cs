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
    public class EmergencyVanacyController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: EmergencyVanacy
        public ActionResult Index()
        {
            return View();
        }
        /*Show Emergency Vacation*/
        public ActionResult showEmergencyVacation()
        {
            List<Employee> employees = db.Employees.ToList();
            SelectList emp = new SelectList(employees, "Id", "Name");
            ViewBag.Employees = emp;
            return View(db.VacencySettings.ToList());
        }
        /*Show Emergency Vacation by id*/
        public ActionResult showEmergencyVacationById(int? id)
        {
            if (id != null)
            {
                List<VacencySetting> Vt = db.VacencySettings.Where(n => n.Emp_Id == id).ToList();
                return PartialView(Vt);
            }
            else
            {
                return PartialView(db.VacencySettings.ToList());
            }
        }

        /*Add Emergency Leave*/
        public ActionResult Emergencyleave()
        {
            List<Employee> employees = db.Employees.ToList();
            SelectList emp = new SelectList(employees, "Id", "Name");
            ViewBag.Employees = emp;
            return View();
        }

        [HttpPost]
        public ActionResult Emergencyleave(VacencySetting EmergencyVacation)
        {
            if (ModelState.IsValid)
            {
                db.VacencySettings.Add(EmergencyVacation);
                db.SaveChanges();
                return RedirectToAction("showEmergencyVacation");
            }
            else
            {
                List<Employee> employees = db.Employees.ToList();
                SelectList emp = new SelectList(employees, "Id", "Name");
                ViewBag.Employees = emp;
                //ViewBag.Error = "Invalid Data";
                return View();
            }
        }

        /*Edite Emergency Vacation and Leave*/
        public ActionResult EditeEmergency(int id)
        {
            VacencySetting at = db.VacencySettings.Find(id);
            return View(at);
        }

        [HttpPost]
        public ActionResult EditeEmergency(VacencySetting at)
        {
            if (ModelState.IsValid)
            {
                VacencySetting atinDB = db.VacencySettings.Find(at.Id);
                atinDB.Date = at.Date;
                atinDB.Name = at.Name;

                db.SaveChanges();


                List<Employee> employees = db.Employees.ToList();
                SelectList emp = new SelectList(employees, "Id", "Name");
                ViewBag.Employees = emp;
                return View("showEmergencyVacation", db.VacencySettings.ToList());
            }
            else
            {
                VacencySetting VT = db.VacencySettings.Find(at.Id);
                ViewBag.Error = "Invalid Data";
                return View(VT);
            }
            }



        /*Delete Emergency  Leave by Id*/

        public ActionResult deleteEmergency(int id)
        {
            VacencySetting at = db.VacencySettings.Find(id);
            db.VacencySettings.Remove(at);
            db.SaveChanges();
            return null;
        }


    }
}