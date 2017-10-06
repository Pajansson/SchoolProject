using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOAD_assignment_1.Data;
using OOAD_assignment_1.Models;
using OOAD_assignment_1.Services;

namespace OOAD_assignment_1.Controllers
{
    public class AccountabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeProvider _timeProvider;

        public AccountabilitiesController(ApplicationDbContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        // GET: Accountabilities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Accountabilities
                .Include(a => a.TimePeriod)
                .Include(a => a.AccountabilityType)
                .Include(a => a.Accountable)
                .Include(a => a.Commissioner)
                .Where(a => a.TimePeriodId == null
                    || a.TimePeriod.EndTime < _timeProvider.GetTime()
                    && a.TimePeriod.StartTime > _timeProvider.GetTime());
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
            ViewData["CommisionerName"] = new SelectList(_context.Parties, "PartyId", "Name");
            ViewData["ResponsibleName"] = new SelectList(_context.Parties, "PartyId", "Name");
            return View();
        }

        // POST: Accountabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommissionerId,Name,AccountableId, Name,AccountabilityTypeId, Description")] Accountability accountability)
        {
            if (ModelState.IsValid)
            {
                if (accountability.AccountableId == accountability.CommissionerId)
                {
                    return RedirectToAction(nameof(Create));
                }

                if (await _context.Accountabilities.SingleOrDefaultAsync(x => x == accountability) != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                _context.Add(accountability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountabilityTypeId"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "Description", accountability.AccountabilityTypeId, accountability.AccountabilityType.Description);
            ViewData["AccountableId"] = new SelectList(_context.Parties, "PartyId", "Name", accountability.AccountableId, accountability.Accountable.Name);
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "Name", accountability.CommissionerId, accountability.Accountable.Name);

            return View(accountability);
        }

        // GET: Accountabilities/Edit/5
        public async Task<IActionResult> Edit(int? typeId, int accountableId, int commisionerId)
        {
            var accountability = await _context.Accountabilities.SingleOrDefaultAsync(m => m.AccountableId == accountableId && m.CommissionerId == commisionerId && m.AccountabilityTypeId == typeId);
            if (accountability == null)
            {
                return NotFound();
            }
            ViewData["Description"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "Description");
            ViewData["CommisionerName"] = new SelectList(_context.Parties, "PartyId", "Name");
            ViewData["ResponsibleName"] = new SelectList(_context.Parties, "PartyId", "Name");
            return View(accountability);
        }

        // POST: Accountabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CommissionerId,Name,AccountableId, Name,AccountabilityTypeId, Description")] Accountability accountability)
        {
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
            ViewData["AccountabilityTypeId"] = new SelectList(_context.AccountabilityTypes, "AccountabilityTypeId", "Description", accountability.AccountabilityTypeId, accountability.AccountabilityType.Description);
            ViewData["AccountableId"] = new SelectList(_context.Parties, "PartyId", "Name", accountability.AccountableId, accountability.Accountable.Name);
            ViewData["CommissionerId"] = new SelectList(_context.Parties, "PartyId", "Name", accountability.CommissionerId, accountability.Accountable.Name);
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
