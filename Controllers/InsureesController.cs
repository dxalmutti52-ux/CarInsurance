using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureesController : Controller
    {
        private readonly InsuranceContext _context;

        public InsureesController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Insurees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insurees.ToListAsync());
        }

        // GET: Insurees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (insuree == null)
                return NotFound();

            return View(insuree);
        }

        // GET: Insurees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insurees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                decimal quote = 50;

                // Age calculation
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > DateTime.Now.AddYears(-age))
                    age--;

                if (age <= 18)
                    quote += 100;
                else if (age <= 25)
                    quote += 50;
                else
                    quote += 25;

                // Car Year
                if (insuree.CarYear < 2000)
                    quote += 25;
                if (insuree.CarYear > 2015)
                    quote += 25;

                // Car Make / Model
                if (insuree.CarMake.ToLower() == "porsche")
                {
                    quote += 25;
                    if (insuree.CarModel.ToLower() == "911 carrera")
                        quote += 25;
                }

                // Speeding Tickets
                quote += insuree.SpeedingTickets * 10;

                // DUI
                if (insuree.DUI)
                    quote *= 1.25m;

                // Coverage Type
                if (insuree.CoverageType)
                    quote *= 1.5m;

                insuree.Quote = quote;

                _context.Add(insuree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(insuree);
        }

        // GET: Insurees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var insuree = await _context.Insurees.FindAsync(id);
            if (insuree == null)
                return NotFound();

            return View(insuree);
        }

        // POST: Insurees/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (id != insuree.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                decimal quote = 50;

                // Age calculation
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > DateTime.Now.AddYears(-age))
                    age--;

                if (age <= 18)
                    quote += 100;
                else if (age <= 25)
                    quote += 50;
                else
                    quote += 25;

                // Car Year
                if (insuree.CarYear < 2000)
                    quote += 25;
                if (insuree.CarYear > 2015)
                    quote += 25;

                // Car Make / Model
                if (insuree.CarMake.ToLower() == "porsche")
                {
                    quote += 25;
                    if (insuree.CarModel.ToLower() == "911 carrera")
                        quote += 25;
                }

                // Speeding Tickets
                quote += insuree.SpeedingTickets * 10;

                // DUI
                if (insuree.DUI)
                    quote *= 1.25m;

                // Coverage Type
                if (insuree.CoverageType)
                    quote *= 1.5m;

                insuree.Quote = quote;

                try
                {
                    _context.Update(insuree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsureeExists(insuree.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (insuree == null)
                return NotFound();

            return View(insuree);
        }

        // POST: Insurees/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuree = await _context.Insurees.FindAsync(id);
            if (insuree != null)
            {
                _context.Insurees.Remove(insuree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsureeExists(int id)
        {
            return _context.Insurees.Any(e => e.Id == id);
        }

        // 🔥 Admin Page
        public async Task<IActionResult> Admin()
        {
            return View(await _context.Insurees.ToListAsync());
        }
    }
}
