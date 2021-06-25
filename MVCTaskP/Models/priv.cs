using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class priv
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}