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
    public class EmployeeController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: Employee
        public ActionResult Index()
        {
            string s = Privis();
            if (s.Contains("Hr"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }

        public ActionResult addEmp()
        {
            string s = Privis();
            if (s.Contains("Hr"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }

        [HttpPost]
        public ActionResult addEmp(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index", "Hr");
            }
            else
            {
                return View();
            }
        }



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