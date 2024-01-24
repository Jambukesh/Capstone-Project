using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using WebApiMVC.Data;

namespace WebApiMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly WebApiMVCContext _context;

        public BlogController(WebApiMVCContext context)
        {
            _context = context;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var webApiMVCContext = _context.BlogInfo.Include(b => b.EmpInfo);
            return View(await webApiMVCContext.ToListAsync());
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .Include(b => b.EmpInfo)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            ViewData["Email"] = new SelectList(_context.EmpInfo, "Email", "Email");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,Email")] BlogInfo blogInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = new SelectList(_context.EmpInfo, "Email", "Email", blogInfo.Email);
            return View(blogInfo);
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }
            ViewData["Email"] = new SelectList(_context.EmpInfo, "Email", "Email", blogInfo.Email);
            return View(blogInfo);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,Email")] BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogInfoExists(blogInfo.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = new SelectList(_context.EmpInfo, "Email", "Email", blogInfo.Email);
            return View(blogInfo);
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .Include(b => b.EmpInfo)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlogInfo == null)
            {
                return Problem("Entity set 'WebApiMVCContext.BlogInfo'  is null.");
            }
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo != null)
            {
                _context.BlogInfo.Remove(blogInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogInfoExists(int id)
        {
          return (_context.BlogInfo?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
