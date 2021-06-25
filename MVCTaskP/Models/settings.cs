using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class settings
    {
        public int Id { get; set; }
        public double over { get; set; }
        public double deduction { get; set; }
        public string weekends { get; set; }
    }
}