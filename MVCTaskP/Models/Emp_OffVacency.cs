using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class Emp_OffVacency
    {
       
        public int Emp_Id { get; set; }

        
        public int VacancyT_Id { get; set; }

        [Key]
        [Column(Order = 0)]
        public int Employee_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Vacencyt_Id { get; set; }

        public virtual Employee Employees { get; set; }

        public virtual VacencyT VacencyTs { get; set; }
    }
}