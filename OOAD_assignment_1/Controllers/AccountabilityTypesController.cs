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
    public class AccountabilityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountabilityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AccountabilityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountabilityTypes.ToListAsync());
        }

        // GET: AccountabilityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountabilityType = await _context.AccountabilityTypes
                .SingleOrDefaultAsync(m => m.AccountabilityTypeId == id);
            if (accountabilityType == null)
            {
                return NotFound();
            }

            return View(accountabilityType);
        }

        // GET: AccountabilityTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountabilityTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountabilityTypeId,Description")] AccountabilityType accountabilityType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountabilityType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountabilityType);
        }

        // GET: AccountabilityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountabilityType = await _context.AccountabilityTypes.SingleOrDefaultAsync(m => m.AccountabilityTypeId == id);
            if (accountabilityType == null)
            {
                return NotFound();
            }
            return View(accountabilityType);
        }

        // POST: AccountabilityTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountabilityTypeId,Description")] AccountabilityType accountabilityType)
        {
            if (id != accountabilityType.AccountabilityTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountabilityType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountabilityTypeExists(accountabilityType.AccountabilityTypeId))
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
            return View(accountabilityType);
        }

        // GET: AccountabilityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountabilityType = await _context.AccountabilityTypes
                .SingleOrDefaultAsync(m => m.AccountabilityTypeId == id);
            if (accountabilityType == null)
            {
                return NotFound();
            }

            return View(accountabilityType);
        }

        // POST: AccountabilityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountabilityType = await _context.AccountabilityTypes.SingleOrDefaultAsync(m => m.AccountabilityTypeId == id);
            _context.AccountabilityTypes.Remove(accountabilityType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountabilityTypeExists(int id)
        {
            return _context.AccountabilityTypes.Any(e => e.AccountabilityTypeId == id);
        }
    }
}
