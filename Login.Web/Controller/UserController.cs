using AutoMapper;
using Login.Web.Data.Model;
using Login.Web.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        [Route("Login/{login}/{password}")]
        public ActionResult<User> Login(string login, string password)
        {
            User user = _context.Users.SingleOrDefault(u => u.Login == login);

            if (user == null)
            {
                return Problem();
            }

            if (!ComparePasswords(user.HashedPassword, password))
            {
                return NotFound();
            }

            return Ok(user);
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
            Sha3Digest hashAlgorithm = new Sha3Digest(512);

            // Choose correct encoding based on your usecase
            byte[] input = Encoding.UTF8.GetBytes(password);

            hashAlgorithm.BlockUpdate(input, 0, input.Length);

            byte[] result = new byte[64]; // 512 / 8 = 64
            hashAlgorithm.DoFinal(result, 0);

            string hashString = BitConverter.ToString(result);

            return hashString.Replace("-", "").ToLowerInvariant();
        }

        private bool ComparePasswords(string hashed, string password)
        {
            return hashed == Hash(password);
        }
    }
}
