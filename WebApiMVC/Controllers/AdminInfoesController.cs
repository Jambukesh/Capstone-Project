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
    public class AdminInfoesController : Controller
    {
        private readonly WebApiMVCContext _context;

        public AdminInfoesController(WebApiMVCContext context)
        {
            _context = context;
        }

        // GET: AdminInfoes
        public async Task<IActionResult> Index()
        {
              return _context.AdminInfo != null ? 
                          View(await _context.AdminInfo.ToListAsync()) :
                          Problem("Entity set 'WebApiMVCContext.AdminInfo'  is null.");
        }

        // GET: AdminInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo
                .FirstOrDefaultAsync(m => m.Email == id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            return View(adminInfo);
        }

        // GET: AdminInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminInfo);
        }

        // GET: AdminInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo == null)
            {
                return NotFound();
            }
            return View(adminInfo);
        }

        // POST: AdminInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password")] AdminInfo adminInfo)
        {
            if (id != adminInfo.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminInfoExists(adminInfo.Email))
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
            return View(adminInfo);
        }

        // GET: AdminInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AdminInfo == null)
            {
                return NotFound();
            }

            var adminInfo = await _context.AdminInfo
                .FirstOrDefaultAsync(m => m.Email == id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            return View(adminInfo);
        }

        // POST: AdminInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AdminInfo == null)
            {
                return Problem("Entity set 'WebApiMVCContext.AdminInfo'  is null.");
            }
            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo != null)
            {
                _context.AdminInfo.Remove(adminInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminInfoExists(string id)
        {
          return (_context.AdminInfo?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
