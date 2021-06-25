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
    public class HrController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: Hr
        public ActionResult Index()
        {
            string s = Privis();
            if (s.Contains("Hr"))
            {
                return View(db.Employees.ToList());
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }


        /*Delete Employee*/
        public ActionResult delete(int id)
        {
            Employee e = db.Employees.Find(id);
            db.Employees.Remove(e);
            db.SaveChanges();
            return null;
        }

        /*Edite Employee*/
        public ActionResult EditeEmployee(int id)
        {
            Employee e = db.Employees.Find(id);
            return View(e);
        }
        /*Edite Employee*/
        [HttpPost]
        public ActionResult EditeEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                Employee e = db.Employees.Find(emp.Id);
                e.Name = emp.Name;
                e.Atendance = emp.Atendance;
                e.Salary = emp.Salary;
                e.Leave = emp.Leave;
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            else
            {
                ViewBag.Error = "Invalid Data ... ";
                return View(emp);
            }
        }
        /* -----------------   */
        string Privis()
        {
            int id = int.Parse(Session["userid"].ToString());
            User currentUser = db.Users.Find(id);
            var roleu = db.Roles.Where(n => n.Id == currentUser.roleId).FirstOrDefault();
            var privss = db.Roleprivs.Where(n => n.RoleId == roleu.Id);
            string s = "";

            foreach (var item in privss)
            {
                s += item.priv.Name + ", ";
            }

            return s;
        }

    }
}