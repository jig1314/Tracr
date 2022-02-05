using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraCR.Server.Data;
using TraCR.Server.Models;

namespace TraCR.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
