using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class Attendance_Leave
    {
        public int Id { get; set; }

        [ForeignKey("employee")]
        public int Emp_Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(typeof(TimeSpan), "00:00", "11:59", ErrorMessage = "Range must be AM")]
       
        public TimeSpan Attend_Time { get; set; }
        [Required]
        [Range(typeof(TimeSpan), "12:00", "23:59",ErrorMessage ="Range must be PM")]
        public TimeSpan Leave_Time { get; set; }

        public virtual Employee employee { get; set; }
    }
}