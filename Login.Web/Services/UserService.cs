using Login.Web.Data.Model;
using Login.Web.Services.Interfaces;
using Login.Web.ViewModels;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Web.Services
{
    public class UserService : IUserService
    {
        private readonly LoginDatabaseContext _context;

        public UserService(LoginDatabaseContext context)
        {
            _context = context;
        }

        public Task<User> GetUser(string login)
        {
            return Task.FromResult(_context.Users.SingleOrDefault(u => u.Login == login));
        }

        public async Task Register(UserViewModel userViewModel)
        {
            User user = new User()
            {
                Id = default(int),
                Login = userViewModel.Login,
                HashedPassword = Hash(userViewModel.Password),
                Permissions = userViewModel.Permissions,
                RegisterDate = DateTime.UtcNow
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return;
        }

        public bool ComparePasswords(string hashed, string password)
        {
            return hashed == Hash(password);
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
    }
}
