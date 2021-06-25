using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTaskP.Models;
using System.Text;
using MVCTaskP.filter;

namespace MVCTaskP.Controllers
{
 
    public class LoginController : Controller
    {
        
        Piocontext db = new Piocontext();

        // GET: Login
       
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("welcome","User");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        //[authutication]
        public ActionResult Index(User s)
        {
            
                s.UserName = HttpUtility.HtmlEncode(s.UserName);
           
                User sinDb = db.Users.Where(n => n.UserName == s.UserName && n.Password == s.Password).FirstOrDefault();
                if (sinDb != null)
                {
                    Session["userid"] = sinDb.Id;
                    return RedirectToAction("welcome", "User");
                }
                else
                {
                    ViewBag.mes = "Invalid User Name Or Password";
                    return View();
                }
          

        }
        [authutication]
        public ActionResult logout()
        {
            Session["userid"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}