using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [Route("api/[controller]")] // localhost:5000/api/members
    // [ApiController]
   
    public class MembersController(AppDbContext context) : BaseApiController // return data from the database
    {
      
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers() // async is used for non-blocking operations
        {
            var members = await context.Users.ToListAsync();
            return members;
        }
        
        [Authorize]
        [HttpGet("{id}")] //localhost:5000/api/members/bob-id
        public  async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id);
           if(member == null) return NotFound();
           return member;
        }
    }
}
