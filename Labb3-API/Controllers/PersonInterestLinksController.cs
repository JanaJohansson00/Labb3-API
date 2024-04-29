using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3_API.Data;
using Labb3_API.Models;

namespace Labb3_API.Controllers
{
    public class PersonInterestLinksController : Controller
    {
        private readonly PersonInterestDbContext _context;

        public PersonInterestLinksController(PersonInterestDbContext context)
        {
            _context = context;
        }

        // GET: PersonInterestLinks
        //public async Task<IActionResult> Index()
        //{
        //    var personInterestDbContext = _context.PersonInterestLinks.Include(p => p.interest).Include(p => p.link).Include(p => p.person);
        //    return View(await personInterestDbContext.ToListAsync());
        //}

        // GET: PersonInterestLinks/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var personInterestLink = await _context.PersonInterestLinks
        //        .Include(p => p.interest)
        //        .Include(p => p.link)
        //        .Include(p => p.person)
        //        .FirstOrDefaultAsync(m => m.PersonInterestId == id);
        //    if (personInterestLink == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(personInterestLink);
        //}

        // GET: PersonInterestLinks/Create
        public IActionResult Create()
        {
            ViewData["InterestId"] = new SelectList(_context.Interests, "InterestId", "InterestId");
            ViewData["LinkId"] = new SelectList(_context.Links, "LinkId", "LinkId");
            ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId");
            return View();
        }

        // POST: PersonInterestLinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonInterestId,PersonId,InterestId,LinkId")] PersonInterest personInterestLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personInterestLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personInterestLink.PersonId);
            //ViewData["InterestId"] = new SelectList(_context.Interests, "InterestId", "InterestId", personInterestLink.InterestId);
            return View(personInterestLink);
        }

        // GET: PersonInterestLinks/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var personInterestLink = await _context.PersonInterestLinks.FindAsync(id);
        //    if (personInterestLink == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["InterestId"] = new SelectList(_context.Interests, "InterestId", "InterestId", personInterestLink.InterestId);
        //    ViewData["LinkId"] = new SelectList(_context.Links, "LinkId", "LinkId", personInterestLink.LinkId);
        //    ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personInterestLink.PersonId);
        //    return View(personInterestLink);
        //}

        // POST: PersonInterestLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PersonInterestId,PersonId,InterestId,LinkId")] PersonInterest personInterestLink)
        //{
        //    if (id != personInterestLink.PersonInterestId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(personInterestLink);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PersonInterestLinkExists(personInterestLink.PersonInterestId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["InterestId"] = new SelectList(_context.Interests, "InterestId", "InterestId", personInterestLink.InterestId);
        //    ViewData["LinkId"] = new SelectList(_context.Links, "LinkId", "LinkId", personInterestLink.LinkId);
        //    ViewData["PersonId"] = new SelectList(_context.Persons, "PersonId", "PersonId", personInterestLink.PersonId);
        //    return View(personInterestLink);
        //}

        // GET: PersonInterestLinks/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var personInterestLink = await _context.PersonInterestLinks
        //        .Include(p => p.interest)
        //        .Include(p => p.link)
        //        .Include(p => p.person)
        //        .FirstOrDefaultAsync(m => m.PersonInterestId == id);
        //    if (personInterestLink == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(personInterestLink);
        //}

        // POST: PersonInterestLinks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var personInterestLink = await _context.PersonInterestLinks.FindAsync(id);
        //    if (personInterestLink != null)
        //    {
        //        _context.PersonInterestLinks.Remove(personInterestLink);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PersonInterestLinkExists(int id)
        //{
        //    return _context.PersonInterestLinks.Any(e => e.PersonInterestId == id);
        //}
    }
}
