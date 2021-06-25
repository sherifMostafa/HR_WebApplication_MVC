using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTaskP.filter;
using MVCTaskP.Models;
using MVCTaskP.ViewModels;

namespace MVCTaskP.Controllers
{
    [authutication]
    public class UserController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: User
        public ActionResult Index()
        {
            string s = Privis();
            if (s.Contains("Users"))
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }
        public ActionResult Welcome()
        {
                int userId = int.Parse(Session["userid"].ToString());
                return View(db.Users.Find(userId));
        }
        public ActionResult addUser()
        {
            string s = Privis();
            if (s.Contains("Users"))
            {
                List<Role> roles = db.Roles.ToList();
                SelectList role = new SelectList(roles, "Id", "Name");
                ViewBag.roles = role;
                return View();
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }
        [HttpPost]
        public ActionResult addUser(User s)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            else
            {
                List<Role> roles = db.Roles.ToList();
                SelectList role = new SelectList(roles, "Id", "Name");
                ViewBag.roles = role;
                ViewBag.Error = "Invalid Data";
                return View();
            }
        }
        public ActionResult edite(int id)
        {
            string s = Privis();
            if (s.Contains("Users"))
            {
                User us = db.Users.Find(id);
                List<Role> roles = db.Roles.ToList();
                SelectList role = new SelectList(roles, "Id", "Name");
                ViewBag.roles = role;
                return View(us);
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }

        [HttpPost]
        public ActionResult edite(int id,User s)
        {
            User userindb = db.Users.Find(id);
            userindb.UserName = s.UserName;
            userindb.age = s.age;
            userindb.Name = s.Name;
            userindb.ConfirmPassword = userindb.Password;
            userindb.roleId = s.roleId;
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        public ActionResult delete(int id)
        {
            string s = Privis();
            if (s.Contains("Users"))
            {
                User us = db.Users.Find(id);
                db.Users.Remove(us);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }
        public ActionResult editePassword(int id)
        {
                string s = Privis();
                if (s.Contains("Users"))
                {
                    User us = db.Users.Find(id);
                    return View(us);
                }
                else
                {
                    return RedirectToAction("welcome", "User");
                }
        }
        [HttpPost]
        public ActionResult editePassword(User s , string OP)
        {
            User myUser = db.Users.Where(us => us.Id == s.Id).FirstOrDefault();
            if (OP == myUser.Password)
            {
                myUser.Password = s.Password;
                myUser.ConfirmPassword = s.ConfirmPassword;
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.mes = "You have Entered wrong PAssword";
                User sameUser = db.Users.Where(usr => usr.Id == s.Id).FirstOrDefault();
                return View(myUser);
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