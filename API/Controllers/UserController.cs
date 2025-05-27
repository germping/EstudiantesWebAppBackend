using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UserController:BaseApiController 
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;

        public UserController(ApplicationDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpGet]//api/user
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users =await  _db.Users.ToListAsync();
            return Ok(users);
        }
        [Authorize]
        [HttpGet ("{id}")]
        public async Task<ActionResult<User>> GetUser(int id) { 
            //var user = _db.Users.SingleOrDefault(x => x.Id == id);
            var user =await  _db.Users.FindAsync(id);
            return Ok(user);
            
        }
        [HttpPost("register")] //POST: api/user/register
        public async Task<ActionResult<UserDto>> Register(UserRegisterDTO registerDTO)
        {
            if(await UserExits(registerDTO.Username)) return BadRequest("User already exits");
            using var hmac= new HMACSHA512();
            var user = new User
            {
                Username = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                IdProfile= registerDTO.idProfile,
                Email = registerDTO.email,
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            //return Ok(user);
            return new UserDto
            {
                UserName = registerDTO.Username,
                Token = _tokenService.CreateToken(user)
            };

        }
        private async Task<bool> UserExits(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username == username.ToLower());
        }
        [HttpPost("login")] // POST: api/user/login
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x=> x.Username == loginDTO.Username);
            if (user == null) return Unauthorized("User invalid");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Password invalid");
            }
            //return Ok(user);
            return new UserDto { UserName = loginDTO.Username, Token = _tokenService.CreateToken(user) };   

        }
    }
}
