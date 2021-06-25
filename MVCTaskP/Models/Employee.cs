using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Salary { get; set; }
        [Required]
        [Range(typeof(TimeSpan), "00:00", "11:59", ErrorMessage = "Range must be AM")]
        public TimeSpan Atendance { get; set; }
        [Required]
        [Range(typeof(TimeSpan), "12:00", "23:59", ErrorMessage = "Range must be PM")]
        public TimeSpan Leave { get; set; }
        public virtual List<Attendance_Leave> AttendLeaves { get; set; }
        public virtual List<VacencyT> VacencyTs { get; set; }
        public virtual List<VacencySetting> VacencySettings { get; set; }
    }
}