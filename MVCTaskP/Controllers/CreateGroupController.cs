using MVCTaskP.filter;
using MVCTaskP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTaskP.Controllers
{
    [authutication]
    public class CreateGroupController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: CreateGroup
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(string Dashboard , string Hr, string Users,string CreateGroup, Role r)
        {
            /*XSS*/
            string str = HttpUtility.HtmlEncode(r.Name);
            r.Name = str;
            /*XSS*/

            db.Roles.Add(r);
            db.SaveChanges();
         
            if(Dashboard == "true")
            {
                Roleprivs rp = new Roleprivs() { RoleId = r.Id, privId = 1 };
                db.Roleprivs.Add(rp);
            }

            if(Hr == "true")
            {
                Roleprivs rp = new Roleprivs() { RoleId = r.Id, privId = 3 };
                db.Roleprivs.Add(rp);
            }

            if(Users == "true")
            {
                Roleprivs rp = new Roleprivs() { RoleId = r.Id, privId = 2 };
                db.Roleprivs.Add(rp);
            }

            if(CreateGroup == "true")
            {
                Roleprivs rp = new Roleprivs() { RoleId = r.Id, privId = 5 };
                db.Roleprivs.Add(rp);
            }
            db.SaveChanges();
            return RedirectToAction("welcome" , "User");
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