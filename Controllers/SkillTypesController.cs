using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruitment.API.Models;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class SkillTypesController : Controller
    {
        private readonly AppDbContext _context;

        public SkillTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SkillTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SkillTypes.ToListAsync());
        }

        // GET: SkillTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillType = await _context.SkillTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillType == null)
            {
                return NotFound();
            }

            return View(skillType);
        }

        // GET: SkillTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SkillTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SkillType skillType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skillType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skillType);
        }

        // GET: SkillTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillType = await _context.SkillTypes.FindAsync(id);
            if (skillType == null)
            {
                return NotFound();
            }
            return View(skillType);
        }

        // POST: SkillTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SkillType skillType)
        {
            if (id != skillType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillTypeExists(skillType.Id))
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
            return View(skillType);
        }

        // GET: SkillTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillType = await _context.SkillTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillType == null)
            {
                return NotFound();
            }

            return View(skillType);
        }

        // POST: SkillTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skillType = await _context.SkillTypes.FindAsync(id);
            _context.SkillTypes.Remove(skillType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillTypeExists(int id)
        {
            return _context.SkillTypes.Any(e => e.Id == id);
        }
    }
}
