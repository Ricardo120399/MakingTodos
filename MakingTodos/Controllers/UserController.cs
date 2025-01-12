﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakingTodos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            var users = await _context.Users.ToListAsync();
            if (users == null)
                return BadRequest("User not found");
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var dbUser = await _context.Users.FindAsync(request.Id);
            if (dbUser == null)
                return BadRequest("User not found");

            dbUser.Name = request.Name;
            dbUser.LastName = request.LastName;
            dbUser.Email = request.Email;
            dbUser.Password = request.Password;
            dbUser.Age = request.Age;
            dbUser.Professition = request.Professition;

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
                return BadRequest("User not found");

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }
    }
}
