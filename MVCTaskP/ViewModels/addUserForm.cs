using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCTaskP.Models;

namespace MVCTaskP.ViewModels
{
    public class addUserForm
    {
        public User user { get; set; }
        public List<Role> roles { get; set; }
    }
}