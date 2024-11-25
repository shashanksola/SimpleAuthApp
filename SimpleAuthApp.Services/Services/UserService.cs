using SimpleAuthApp.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAuthApp.Utility;
using SimpleAuthApp.Utility.DTOs;
using System.Data;

namespace SimpleAuthApp.Services.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly JWTService _jwtService;

        public UserService(UserRepository userRepository, JWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<dynamic> RegisterUser(UserRequest request)
        {
            if (await _userRepository.GetByUsernameAsync(request.Username) != null)
                return "Username already exists.";
            if (await _userRepository.GetByEmailAsync(request.Email) != null)
                return "Email already exists.";

            _jwtService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var DBUser = new UserDB
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _userRepository.CreateAsync(DBUser);
            return "User registered successfully.";
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !_jwtService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var nuser = new User
            {
                Username = user.Username,
                Email = user.Email
            };

            return _jwtService.CreateToken(nuser);
        }
    }
}
