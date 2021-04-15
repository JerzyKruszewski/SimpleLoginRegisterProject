using Login.Web.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.Data.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string HashedPassword { get; set; }

        public PermissionType Permissions { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
