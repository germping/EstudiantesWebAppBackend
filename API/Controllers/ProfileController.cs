using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _db.Profiles.ToListAsync();   
        }
        [HttpPost("register")]
        public async Task<ActionResult<Profile>> SaveProfile(ProfileRegisterDTO profileRegisterDTO)
        {
            if (await ProfileExists(profileRegisterDTO.Description)) return BadRequest("Profile already exists");
            var profile = new Profile
            {
                Description = profileRegisterDTO.Description
            };
            _db.Profiles.Add(profile);
            _db.SaveChangesAsync();
            return Ok(profile);

        }

        private async Task<bool> ProfileExists(string description)
        {
            return await _db.Profiles.AnyAsync(x => x.Description == description.ToLower());
        }
    }
}
