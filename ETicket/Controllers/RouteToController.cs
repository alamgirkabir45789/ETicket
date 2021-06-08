using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicket.Data;
using ETicket.Models;
using Microsoft.AspNetCore.Authorization;

namespace ETicket.Controllers
{
    public class RouteToController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteToController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteTo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RouteTos.Include(r => r.Route);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RouteTo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeTo = await _context.RouteTos
                .Include(r => r.Route)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (routeTo == null)
            {
                return NotFound();
            }

            return View(routeTo);
        }

        // GET: RouteTo/Create
        public IActionResult Create()
        {
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code");
            return View();
        }

        // POST: RouteTo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]    
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create([Bind("Code,Name,RouteCode")] RouteTo routeTo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeTo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", routeTo.RouteCode);
            return View(routeTo);
        }

        // GET: RouteTo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeTo = await _context.RouteTos.FindAsync(id);
            if (routeTo == null)
            {
                return NotFound();
            }
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", routeTo.RouteCode);
            return View(routeTo);
        }

        // POST: RouteTo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditRolePolicy")]   
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name,RouteCode")] RouteTo routeTo)
        {
            if (id != routeTo.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeTo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteToExists(routeTo.Code))
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
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", routeTo.RouteCode);
            return View(routeTo);
        }

        // GET: RouteTo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeTo = await _context.RouteTos
                .Include(r => r.Route)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (routeTo == null)
            {
                return NotFound();
            }

            return View(routeTo);
        }

        // POST: RouteTo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var routeTo = await _context.RouteTos.FindAsync(id);
            _context.RouteTos.Remove(routeTo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteToExists(string id)
        {
            return _context.RouteTos.Any(e => e.Code == id);
        }
    }
}
