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
    public class AttendaceLeaveController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: AttendaceLeave
        public ActionResult Index()
        {
            return View();
        }
        /*show Attendance and leave */
        public ActionResult showAttendanceAndLeave()
        {

            List<Employee> employees = db.Employees.ToList();
            SelectList emp = new SelectList(employees, "Id", "Name");
            ViewBag.Employees = emp;
            return View(db.Attendance_Leaves.ToList());
        }

        

        /*Delete Attend Leave by Id*/

        public ActionResult deleteAttend(int id)
        {
            Attendance_Leave at = db.Attendance_Leaves.Find(id);
            db.Attendance_Leaves.Remove(at);
            db.SaveChanges();
            return null;
        }

        /*Add atendance and Leave*/
        public ActionResult AttendLeave()
        {
            List<Employee> employees = db.Employees.ToList();
            SelectList emp = new SelectList(employees, "Id", "Name");
            ViewBag.Employees = emp;
            return View();
        }

        [HttpPost]
        public ActionResult AttendLeave(Attendance_Leave attendLeave)
        {
            if (ModelState.IsValid)
            {
                db.Attendance_Leaves.Add(attendLeave);
                db.SaveChanges();
                return RedirectToAction("showAttendanceAndLeave");
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

        /*Edite atendance and Leave*/
        public ActionResult EditeAttend(int id)
        {
            Attendance_Leave at = db.Attendance_Leaves.Find(id);
            return View(at);
        }
        [HttpPost]
        public ActionResult EditeAttend(Attendance_Leave at)
        {
            if (ModelState.IsValid)
            {
                Attendance_Leave atinDB = db.Attendance_Leaves.Find(at.Id);
                atinDB.Date = at.Date;
                atinDB.Attend_Time = at.Attend_Time;
                atinDB.Leave_Time = at.Leave_Time;
                db.SaveChanges();

                List<Employee> employees = db.Employees.ToList();
                SelectList emp = new SelectList(employees, "Id", "Name");
                ViewBag.Employees = emp;
                return View("showAttendanceAndLeave" , db.Attendance_Leaves.ToList());
            }
            else
            {
                Attendance_Leave VT = db.Attendance_Leaves.Find(at.Id);
                ViewBag.Error = "Invalid Data";
                return View(VT);
            }
        }
    }
}