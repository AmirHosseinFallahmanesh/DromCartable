using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Controllers
{
    public class DormsController : Controller
    {
        private readonly MyContext _context;

        public DormsController(MyContext context)
        {
            _context = context;
        }

        // GET: Dorms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dorms.ToListAsync());
        }

        // GET: Dorms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .FirstOrDefaultAsync(m => m.DormId == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        // GET: Dorms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dorms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DormId,Name,Type,Capacity")] Dorm dorm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dorm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dorm);
        }

        // GET: Dorms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms.FindAsync(id);
            if (dorm == null)
            {
                return NotFound();
            }
            return View(dorm);
        }

        // POST: Dorms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DormId,Name,Type,Capacity")] Dorm dorm)
        {
            if (id != dorm.DormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dorm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormExists(dorm.DormId))
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
            return View(dorm);
        }

        // GET: Dorms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .FirstOrDefaultAsync(m => m.DormId == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        // POST: Dorms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dorm = await _context.Dorms.FindAsync(id);
            _context.Dorms.Remove(dorm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormExists(int id)
        {
            return _context.Dorms.Any(e => e.DormId == id);
        }
    }
}
