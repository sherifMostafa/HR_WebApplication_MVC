using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class VacencyTEmployees
    {
        [Key, Column(Order = 1)]
        public int Empid { get; set; }
        [Key, Column(Order = 2)]
        public int Vacnid { get; set; }
        public virtual Employee employee { get; set; }
        public virtual VacencyT Vacencyt { get; set; }
    }
}