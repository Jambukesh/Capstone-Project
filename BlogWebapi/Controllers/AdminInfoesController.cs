using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogWebapi.Data;
using DAL;

namespace BlogWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoesController : ControllerBase
    {
        private readonly BlogWebapiContext _context;

        public AdminInfoesController(BlogWebapiContext context)
        {
            _context = context;
        }

        // GET: api/AdminInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminInfo>>> GetAdminInfo()
        {
          if (_context.AdminInfo == null)
          {
              return NotFound();
          }
            return await _context.AdminInfo.ToListAsync();
        }

        // GET: api/AdminInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminInfo>> GetAdminInfo(string id)
        {
          if (_context.AdminInfo == null)
          {
              return NotFound();
          }
            var adminInfo = await _context.AdminInfo.FindAsync(id);

            if (adminInfo == null)
            {
                return NotFound();
            }

            return adminInfo;
        }

        // PUT: api/AdminInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminInfo(string id, AdminInfo adminInfo)
        {
            if (id != adminInfo.Email)
            {
                return BadRequest();
            }

            _context.Entry(adminInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AdminInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminInfo>> PostAdminInfo(AdminInfo adminInfo)
        {
          if (_context.AdminInfo == null)
          {
              return Problem("Entity set 'BlogWebapiContext.AdminInfo'  is null.");
          }
            _context.AdminInfo.Add(adminInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminInfoExists(adminInfo.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdminInfo", new { id = adminInfo.Email }, adminInfo);
        }

        // DELETE: api/AdminInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminInfo(string id)
        {
            if (_context.AdminInfo == null)
            {
                return NotFound();
            }
            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            _context.AdminInfo.Remove(adminInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminInfoExists(string id)
        {
            return (_context.AdminInfo?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
