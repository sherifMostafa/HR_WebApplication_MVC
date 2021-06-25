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
    public class OfficialVanacyController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: OfficialVanacy
        public ActionResult Index()
        {
            return View();
        }

        /*Add official Vacation */
        public ActionResult Officialvacation()
        {
            //List<Employee> employees = db.Employees.ToList();
            //SelectList emp = new SelectList(employees, "Id", "Name");
            //ViewBag.Employees = emp;
            return View();
        }

        [HttpPost]
        public ActionResult Officialvacation(VacencyT OfficV)
        {
            if (ModelState.IsValid)
            {
                db.VacencyTs.Add(OfficV);
                db.SaveChanges();
                List<Employee> emps = db.Employees.ToList();
                foreach (var item in emps)
                {
                    VacencyTEmployees eo = new VacencyTEmployees()
                    {
                        Empid = item.Id,
                        Vacnid= OfficV.Id
                    };
                    db.VacancyTEmployeess.Add(eo);
                    db.SaveChanges();
                }
                return RedirectToAction("showOfficalVacation");
            }
            else
            {
                //List<Employee> employees = db.Employees.ToList();
                //SelectList emp = new SelectList(employees, "Id", "Name");
                //ViewBag.Employees = emp;
                //ViewBag.Error = "Invalid Data";
                return View();
            }
        }

        //showOfficalVacation
        public ActionResult showOfficalVacation()
        { 
            //List<Employee> employees = db.Employees.ToList();
            //SelectList emp = new SelectList(employees, "Id", "Name");
            //ViewBag.Employees = emp;
            return View(db.VacencyTs.ToList());
        }

        //show Offical Vacation Id
        //public ActionResult showOfficalVacationById(int? id)
        //{
        //    if (id != null)
        //    {
        //        List<VacencyT> Vt = db.VacencyTs.Where(n => n.Emp_Id == id).ToList();
        //        return PartialView(Vt);
        //    }
        //    else
        //    {
        //        return PartialView(db.VacencyTs.ToList());
        //    }    
        //}
        /*Delete Official Vacation*/
        public ActionResult deleteOfficialVacation(int id)
        {
            VacencyT v = db.VacencyTs.Find(id);
            db.VacencyTs.Remove(v);
            db.SaveChanges();
            return null;
        }

       
        //EditeOfficial

        
        public ActionResult EditeOfficial(int id)
        {
            VacencyT at = db.VacencyTs.Find(id);
            return View(at);
        }
        [HttpPost]
        public ActionResult EditeOfficial(VacencyT at)
        {
            if (ModelState.IsValid)
            {
                VacencyT atinDB = db.VacencyTs.Find(at.Id);
                atinDB.VDate = at.VDate;
                atinDB.Name = at.Name;
                db.SaveChanges();

                //List<Employee> employees = db.Employees.ToList();
                //SelectList emp = new SelectList(employees, "Id", "Name");
                //ViewBag.Employees = emp;
                return View("showOfficalVacation", db.VacencyTs.ToList());
            } 
            else
            {
                VacencyT VT = db.VacencyTs.Find(at.Id);
                ViewBag.Error = "Invalid Data";
                return View(VT);
            }
        }

        //**********************************************
      
    }
}