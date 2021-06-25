using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTaskP.Models
{
    public class Roleprivs
    {

        [Key, Column(Order = 1)]
        public int RoleId { get; set; }
        
        [Key, Column(Order = 2)]
        public int privId { get; set; }
        public virtual Role role { get; set; }
        public virtual priv priv { get; set; }
       
    }
}