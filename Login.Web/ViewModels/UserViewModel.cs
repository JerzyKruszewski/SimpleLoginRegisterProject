using Login.Web.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.ViewModels
{
    public class UserViewModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public PermissionType Permissions { get; set; }
    }
}
