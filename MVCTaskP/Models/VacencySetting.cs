using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class VacencySetting
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("employee")]
        public int Emp_Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public virtual Employee employee { get; set; }
    }
}