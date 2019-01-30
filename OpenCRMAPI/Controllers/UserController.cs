using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenCRMAPI.Models;

namespace OpenCRMAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;

            if(_context.UserDetailsList.Count() == 0)
            {
                //Create user if the list is empty.
                _context.UserDetailsList.Add(new UserDetails {Name = "Permanent User"});
                _context.SaveChanges();
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUsers()
        {
            return await _context.UserDetailsList.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserById(long id)
        {
            var userDetails = await _context.UserDetailsList.FindAsync(id);

            if(userDetails == null)
            {
                return NotFound();
            }

            return userDetails;
        }

        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserDetails userDetails)
        {
            _context.UserDetailsList.Add(userDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new {id = userDetails.Id}, userDetails);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDetails>> PutUserDetails(long id, UserDetails userDetails)
        {
            if(id != userDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var userDetails = await _context.UserDetailsList.FindAsync(id);

            if(userDetails == null)
            {
                return NotFound();
            }

            _context.UserDetailsList.Remove(userDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }   



        

    }
}