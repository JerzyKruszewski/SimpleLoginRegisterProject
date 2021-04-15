using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.Data.Model
{
    public class LoginDatabaseContext : DbContext
    {
        public LoginDatabaseContext(DbContextOptions options)
            : base(options)
        {

        }

        DbSet<User> Users { get; set; }
    }
}
