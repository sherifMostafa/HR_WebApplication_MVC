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
    public class EmployeeReportsController : Controller
    {
        Piocontext db = new Piocontext();
        // GET: EmployeeReports
        public ActionResult Index(int id , int Month , int Year)
        {
            string s = Privis();
            if (s.Contains("Hr"))
            {
                Employee emp = db.Employees.Find(id);
                List<Attendance_Leave> Attendleave = db.Attendance_Leaves.Where(n => n.Emp_Id == emp.Id).ToList();
                List<VacencyT> Vacancy = db.VacencyTs.ToList();
                //List<VacencyTEmployees> Vacancy = db.VacancyTEmployeess.Where(n => n.Empid == emp.Id).ToList();
                //List<VacencyT> Vacancy = db.VacencyTs.Where(n => n.Emp_Id == emp.Id).ToList();
                List<VacencySetting> vacencySettings = db.VacencySettings.Where(n => n.Emp_Id == emp.Id).ToList();
            
                // calculate over Time And Deduction Time 
                int overTime = 0, dect = 0;
                foreach (var item in Attendleave)
                {
                    if (Month == int.Parse(item.Date.Month.ToString()) && Year == int.Parse(item.Date.Year.ToString()))
                    {
                        if (emp.Atendance > item.Attend_Time)
                        {
                            overTime += (int)emp.Atendance.TotalHours - (int)item.Attend_Time.TotalHours;
                        }
                        if (emp.Atendance < item.Attend_Time)
                        {
                            dect += (int)item.Attend_Time.TotalHours - (int)emp.Atendance.TotalHours;
                        }
                        if (emp.Leave < item.Leave_Time)
                        {
                            overTime += (int)item.Leave_Time.TotalHours - (int)emp.Leave.TotalHours;
                        }
                        if (emp.Leave > item.Leave_Time)
                        {
                            dect += (int)emp.Leave.TotalHours - (int)item.Leave_Time.TotalHours;
                        }
                    }
                }

                /************************************************************************************/
                
                int DaysinMonth = int.Parse(DateTime.DaysInMonth(Year, Month).ToString());
                int leave = int.Parse(emp.Leave.TotalHours.ToString());
                int Attend = int.Parse(emp.Atendance.TotalHours.ToString());
                double HourRate = emp.Salary / (DaysinMonth * (leave - Attend));
                
                //-----------------------------------------------------------------------------------------
                int OFV = 0, EMV = 0, AT = 0;
                foreach (var item in Attendleave)
                {
                    if (Month == int.Parse(item.Date.Month.ToString()) && Year == int.Parse(item.Date.Year.ToString()))
                    {
                        AT++;
                    }
                }
                foreach (var item in Vacancy)
                {
                    //if (Month == int.Parse(item.Vacencyt.VDate.Month.ToString()) && Year == int.Parse(item.Vacencyt.VDate.Year.ToString()))
                    if (Month == int.Parse(item.VDate.Month.ToString()))
                    {
                        OFV++;
                    }
                }
                foreach (var item in vacencySettings)
                {
                    if (Month == int.Parse(item.Date.Month.ToString()) && Year == int.Parse(item.Date.Year.ToString()))
                    {
                        EMV++;
                    }
                }

                //Dayes Of Absence Withowt Permision:
                int DaysAbsence = DaysinMonth - (AT + OFV + EMV);

                //----------------------------------------------------------------------------------------------------------------------------
                // ---------   other controler  ------------------

                //calculate number Of Days ---------------------
                List<DateTime> lstSundays = new List<DateTime>();
                settings st = db.settings.Find(1);
                int intMonth = Month;
                int intYear = Year;
                int intDaysThisMonth = DateTime.DaysInMonth(intYear, intMonth);
                DateTime oBeginnngOfThisMonth = new DateTime(intYear, intMonth, 1);
                for (int i = 1; i < intDaysThisMonth + 1; i++)
                {
                    //string satarday, string SunDay, string MonDay, string TuesDay, string wednesday, string thursday, string Friday
                    if (st.weekends.Contains("satarday"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("SunDay"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("MonDay"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("TuesDay"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("wednesday"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Wednesday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("thursday"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Thursday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                    if (st.weekends.Contains("Friday"))
                    {
                        if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Friday)
                        {
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                        }
                    }
                }
                //calculate number Of Days ----------------------

                double overSalary = (overTime * st.over) * HourRate;
                double deducSalary = (dect * st.deduction) * HourRate;
                int numberOfHoursMustHeWork = leave - Attend;
                double totalSalary = (HourRate * (numberOfHoursMustHeWork * (AT + lstSundays.Count + OFV + EMV))) + overSalary - deducSalary;
                DaysAbsence -= lstSundays.Count;

                ViewBag.Month = Month;
                ViewBag.Year = Year;
                ViewBag.over = overTime;
                ViewBag.dect = dect;

                ViewBag.numberOfDaysInThisMonth = DaysinMonth;
                ViewBag.WeekEnd = lstSundays.Count;
                ViewBag.overSalary = overSalary;
                ViewBag.deducSalary = deducSalary;
                ViewBag.AttendDaysinTheMonth = AT;
                ViewBag.OffcialVacencyintTheMonth = OFV;
                ViewBag.EmergencyVacencyintTheMonth = EMV;
                ViewBag.DaysAbsence = DaysAbsence;
                ViewBag.totalSalary = totalSalary;
                ViewBag.HourRate = HourRate;
                ViewBag.numberOfHoursMustHeWork = numberOfHoursMustHeWork;

                //---------------------------------------------------------------------------

                return View(emp);

            }
            else
            {
                return RedirectToAction("welcome", "User");
            }
        }

        //public ActionResult calculateTotalSalary(int Month, int Year , int AttendDaysinTheMonth, int OffcialVacencyintTheMonth, int EmergencyVacencyintTheMonth , int empId , int HourRate, int DaysAbsence , int overTime ,int dectTime)
        //{
        //    string s = Privis();
        //    if (s.Contains("Hr"))
        //    {
        //        List<DateTime> lstSundays = new List<DateTime>();
        //        settings st = db.settings.Find(1);
        //        int intMonth = Month;
        //        int intYear = Year;
        //        int intDaysThisMonth = DateTime.DaysInMonth(intYear, intMonth);
        //        DateTime oBeginnngOfThisMonth = new DateTime(intYear, intMonth, 1);

        //        for (int i = 1; i < intDaysThisMonth + 1; i++)
        //        {
        //            //string satarday, string SunDay, string MonDay, string TuesDay, string wednesday, string thursday, string Friday
        //            if (st.weekends.Contains("satarday"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Saturday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("SunDay"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("MonDay"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("TuesDay"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Tuesday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("wednesday"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Wednesday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("thursday"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Thursday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //            if (st.weekends.Contains("Friday"))
        //            {
        //                if (oBeginnngOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Friday)
        //                {
        //                    lstSundays.Add(new DateTime(intYear, intMonth, i));
        //                }
        //            }
        //        }
        //        ViewBag.WeekEnd = lstSundays.Count;
        //        double overSalary = (overTime * st.over) * HourRate;
        //        double deducSalary = (dectTime * st.deduction) * HourRate;



        //    Employee e = db.Employees.Find(empId);

        //    int leave = int.Parse(e.Leave.TotalHours.ToString());
        //    int Attend = int.Parse(e.Atendance.TotalHours.ToString());

        //    int numberOfHoursMustHeWork = leave - Attend;
        //    double totalSalary = ( HourRate* (numberOfHoursMustHeWork * (AttendDaysinTheMonth + lstSundays.Count + OffcialVacencyintTheMonth + EmergencyVacencyintTheMonth))) + overSalary - deducSalary;
        //    DaysAbsence -= lstSundays.Count;
        //    ViewBag.WeekEnd = lstSundays.Count;
        //    ViewBag.overSalary = overSalary;
        //    ViewBag.deducSalary = deducSalary;
        //    ViewBag.AttendDaysinTheMonth = AttendDaysinTheMonth;
        //    ViewBag.OffcialVacencyintTheMonth = OffcialVacencyintTheMonth;
        //    ViewBag.EmergencyVacencyintTheMonth = EmergencyVacencyintTheMonth;
        //    ViewBag.DaysAbsence = DaysAbsence;
        //    ViewBag.totalSalary = totalSalary;
        //    ViewBag.HourRate = HourRate;
        //    ViewBag.numberOfHoursMustHeWork = numberOfHoursMustHeWork;

        //    return View(e);
        //    }
        //    else
        //    {
        //        return RedirectToAction("welcome", "User");
        //    }
        //}
        //*************************************************
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