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
    public class BlogInfoesController : ControllerBase
    {
        private readonly BlogWebapiContext _context;

        public BlogInfoesController(BlogWebapiContext context)
        {
            _context = context;
        }

        // GET: api/BlogInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogInfo>>> GetBlogInfo()
        {
          if (_context.BlogInfo == null)
          {
              return NotFound();
          }
            return await _context.BlogInfo.ToListAsync();
        }

        // GET: api/BlogInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogInfo>> GetBlogInfo(int id)
        {
          if (_context.BlogInfo == null)
          {
              return NotFound();
          }
            var blogInfo = await _context.BlogInfo.FindAsync(id);

            if (blogInfo == null)
            {
                return NotFound();
            }

            return blogInfo;
        }

        // PUT: api/BlogInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogInfo(int id, BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blogInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogInfoExists(id))
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

        // POST: api/BlogInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogInfo>> PostBlogInfo(BlogInfo blogInfo)
        {
          if (_context.BlogInfo == null)
          {
              return Problem("Entity set 'BlogWebapiContext.BlogInfo'  is null.");
          }
            _context.BlogInfo.Add(blogInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogInfo", new { id = blogInfo.BlogId }, blogInfo);
        }

        // DELETE: api/BlogInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogInfo(int id)
        {
            if (_context.BlogInfo == null)
            {
                return NotFound();
            }
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            _context.BlogInfo.Remove(blogInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogInfoExists(int id)
        {
            return (_context.BlogInfo?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
