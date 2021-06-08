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
    public class RouteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Route
        public async Task<IActionResult> Index()
        {
            return View(await _context.Routes.ToListAsync());
        }

        // GET: Route/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.Code == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Route/Create
        [Authorize(Policy = "CreateRolePolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> Create([Bind("Code,Name")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // GET: Route/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Route/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditRolePolicy")]  
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name")] Route route)
        {
            if (id != route.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Code))
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
            return View(route);
        }

        // GET: Route/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.Code == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(string id)
        {
            return _context.Routes.Any(e => e.Code == id);
        }
    }
}
