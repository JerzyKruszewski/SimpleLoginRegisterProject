using AutoMapper;
using Login.Web.Data.Model;
using Login.Web.Services.Interfaces;
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
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("login/{login}/{password}")]
        public ActionResult<User> Login(string login, string password)
        {
            User user = _service.GetUser(login).Result;

            if (user == null)
            {
                return Problem();
            }

            if (!_service.ComparePasswords(user.HashedPassword, password))
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("getUser/{login}")]
        public ActionResult<User> GetUser(string login)
        {
            User user = _service.GetUser(login).Result;

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
            if (_service.GetUser(userViewModel.Login).Result != null)
            {
                return Problem();
            }

            _service.Register(userViewModel);

            return Ok();
        }
    }
}
