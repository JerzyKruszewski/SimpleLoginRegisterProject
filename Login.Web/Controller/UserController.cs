using AutoMapper;
using Login.Web.Data.Model;
using Login.Web.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Login.Web.Controller
{
    [Route("api/users/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LoginDatabaseContext _context;

        public UserController(LoginDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetUser/{login}")]
        public ActionResult<User> GetUser(string login)
        {
            User user = _context.Users.SingleOrDefault(u => u.Login == login);

            if (user == null)
            {
                return Problem();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(UserViewModel userViewModel)
        {
            if (_context.Users.SingleOrDefault(u => u.Login == userViewModel.Login) != null)
            {
                return Problem();
            }

            User user = new User()
            {
                Id = default(int),
                Login = userViewModel.Login,
                HashedPassword = Hash(userViewModel.Password),
                Permissions = userViewModel.Permissions,
                RegisterDate = userViewModel.RegisterDate
            };

            _context.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        private string Hash(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
