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
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassengerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Passenger
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Passengers.Include(p => p.Route).Include(p => p.RouteTo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Passenger/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .Include(p => p.Route)
                .Include(p => p.RouteTo)
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // GET: Passenger/Create
        public IActionResult Create()
        {
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code");
            ViewData["RouteToCode"] = new SelectList(_context.RouteTos, "Code", "Code");
            return View();
        }

        // POST: Passenger/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateRolePolicy")]      
        public async Task<IActionResult> Create([Bind("PassengerId,Name,Age,Gender,ContactNo,PassengerNo,Price,JournyDate,Email,RouteCode,RouteToCode")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", passenger.RouteCode);
            ViewData["RouteToCode"] = new SelectList(_context.RouteTos, "Code", "Code", passenger.RouteToCode);
            return View(passenger);
        }

        // GET: Passenger/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", passenger.RouteCode);
            ViewData["RouteToCode"] = new SelectList(_context.RouteTos, "Code", "Code", passenger.RouteToCode);
            return View(passenger);
        }

        // POST: Passenger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditRolePolicy")]    
        public async Task<IActionResult> Edit(int id, [Bind("PassengerId,Name,Age,Gender,ContactNo,PassengerNo,Price,JournyDate,Email,RouteCode,RouteToCode")] Passenger passenger)
        {
            if (id != passenger.PassengerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.PassengerId))
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
            ViewData["RouteCode"] = new SelectList(_context.Routes, "Code", "Code", passenger.RouteCode);
            ViewData["RouteToCode"] = new SelectList(_context.RouteTos, "Code", "Code", passenger.RouteToCode);
            return View(passenger);
        }

        // GET: Passenger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .Include(p => p.Route)
                .Include(p => p.RouteTo)
                .FirstOrDefaultAsync(m => m.PassengerId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // POST: Passenger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]      
      
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(int id)
        {
            return _context.Passengers.Any(e => e.PassengerId == id);
        }
    }
}
