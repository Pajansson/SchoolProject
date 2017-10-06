using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOAD_assignment_1.Data;
using OOAD_assignment_1.Models;
using OOAD_assignment_1.ViewModels;

namespace OOAD_assignment_1.Controllers
{
    public class PartiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parties
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parties.ToListAsync());
        }

        public List<Accountability> GetAccountabilities(int ?id)
        {
            var accountabilities = _context.Accountabilities.Where(x => x.CommissionerId == id || x.AccountableId == id)
                .Include(x => x.Accountable)
                .Include(x => x.Commissioner);

            return accountabilities.ToList();
        }
    
        // GET: Parties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyVm = new PartyDetailsViewModel
            {
                Party = await _context.Parties
                    .SingleOrDefaultAsync(m => m.PartyId == id),
                Accountabilities = GetAccountabilities(id)
            };


            if (partyVm.Party == null)
            {
                return NotFound();
            }

            return View(partyVm);
        }

        // GET: Parties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartyId,Name")] Party party)
        {
            if (ModelState.IsValid)
            {
                _context.Add(party);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(party);
        }

        // GET: Parties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.SingleOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }
            return View(party);
        }

        // POST: Parties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartyId,Name")] Party party)
        {
            if (id != party.PartyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(party);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyExists(party.PartyId))
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
            return View(party);
        }

        // GET: Parties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties
                .SingleOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var party = await _context.Parties.SingleOrDefaultAsync(m => m.PartyId == id);
            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyExists(int id)
        {
            return _context.Parties.Any(e => e.PartyId == id);
        }
    }
}