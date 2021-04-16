using Login.Web.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.Data.Model
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Login { get; set; }

        [Required]
        [StringLength(128)]
        public string HashedPassword { get; set; }

        [Required]
        public PermissionType Permissions { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }
    }
}
