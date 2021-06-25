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
    public class DashboardController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: Dashboard
        public ActionResult Index()
        {
            string s = Privis();
            if (s.Contains("Dashboard"))
            {
                int userid = int.Parse(Session["userid"].ToString());
                return View(db.Users.Find(userid));
            }
            else
            {
                return RedirectToAction("welcome", "User");
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