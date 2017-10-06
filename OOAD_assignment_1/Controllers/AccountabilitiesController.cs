using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOAD_assignment_1.Data;
using OOAD_assignment_1.Models;

namespace OOAD_assignment_1.Controllers
{
    public class AccountabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountabilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accountabilities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Accountabilities.Include(a => a.AccountabilityType).Include(a => a.Accountable).Include(a => a.Commissioner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Accountabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountability = await _context.Accountabilities
                .Include(a => a.AccountabilityType)
                .Include(a => a.Accountable)
                .Include(a => a.Commissioner)
                .SingleOrDefaultAsync(m => m.AccountableId == id);
            if (accountability == null)
            {
                return NotFound();
            }

            return View(accountability);
        }

        // GET: Accountabilities/Create
        public IActionResult Create()
        {
            ViewData["Description"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "Description");
            ViewData["Name"] = new SelectList(_context.Parties, "PartyId", "Name");
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "PartyId");
            return View();
        }

        // POST: Accountabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommissionerId,AccountableId,AccountabilityTypeId")] Accountability accountability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountabilityTypeId"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "AccountabilityTypeId", accountability.AccountabilityTypeId);
            ViewData["AccountableId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.AccountableId);
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.CommissionerId);
            return View(accountability);
        }

        // GET: Accountabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountability = await _context.Accountabilities.SingleOrDefaultAsync(m => m.AccountableId == id);
            if (accountability == null)
            {
                return NotFound();
            }
            ViewData["AccountabilityTypeId"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "AccountabilityTypeId", accountability.AccountabilityTypeId);
            ViewData["AccountableId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.AccountableId);
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.CommissionerId);
            return View(accountability);
        }

        // POST: Accountabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommissionerId,AccountableId,AccountabilityTypeId")] Accountability accountability)
        {
            if (id != accountability.AccountableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountabilityExists(accountability.AccountableId))
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
            ViewData["AccountabilityTypeId"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "AccountabilityTypeId", accountability.AccountabilityTypeId);
            ViewData["AccountableId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.AccountableId);
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "PartyId", accountability.CommissionerId);
            return View(accountability);
        }

        // GET: Accountabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountability = await _context.Accountabilities
                .Include(a => a.AccountabilityType)
                .Include(a => a.Accountable)
                .Include(a => a.Commissioner)
                .SingleOrDefaultAsync(m => m.AccountableId == id);
            if (accountability == null)
            {
                return NotFound();
            }

            return View(accountability);
        }

        // POST: Accountabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountability = await _context.Accountabilities.SingleOrDefaultAsync(m => m.AccountableId == id);
            _context.Accountabilities.Remove(accountability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountabilityExists(int id)
        {
            return _context.Accountabilities.Any(e => e.AccountableId == id);
        }
    }
}
