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
    public class ObjectSkillsController : Controller
    {
        private readonly AppDbContext _context;

        public ObjectSkillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ObjectSkills
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ObjectSkills.Include(o => o.ObjectType).Include(o => o.Skill);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ObjectSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectSkill = await _context.ObjectSkills
                .Include(o => o.ObjectType)
                .Include(o => o.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectSkill == null)
            {
                return NotFound();
            }

            return View(objectSkill);
        }

        // GET: ObjectSkills/Create
        public IActionResult Create()
        {
            ViewData["ObjectTypeId"] = new SelectList(_context.ObjectTypes, "Id", "Id");
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id");
            return View();
        }

        // POST: ObjectSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ObjectId,ObjectTypeId,SkillId")] ObjectSkill objectSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objectSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObjectTypeId"] = new SelectList(_context.ObjectTypes, "Id", "Id", objectSkill.ObjectTypeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", objectSkill.SkillId);
            return View(objectSkill);
        }

        // GET: ObjectSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectSkill = await _context.ObjectSkills.FindAsync(id);
            if (objectSkill == null)
            {
                return NotFound();
            }
            ViewData["ObjectTypeId"] = new SelectList(_context.ObjectTypes, "Id", "Id", objectSkill.ObjectTypeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", objectSkill.SkillId);
            return View(objectSkill);
        }

        // POST: ObjectSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ObjectId,ObjectTypeId,SkillId")] ObjectSkill objectSkill)
        {
            if (id != objectSkill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objectSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjectSkillExists(objectSkill.Id))
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
            ViewData["ObjectTypeId"] = new SelectList(_context.ObjectTypes, "Id", "Id", objectSkill.ObjectTypeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "Id", "Id", objectSkill.SkillId);
            return View(objectSkill);
        }

        // GET: ObjectSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectSkill = await _context.ObjectSkills
                .Include(o => o.ObjectType)
                .Include(o => o.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectSkill == null)
            {
                return NotFound();
            }

            return View(objectSkill);
        }

        // POST: ObjectSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objectSkill = await _context.ObjectSkills.FindAsync(id);
            _context.ObjectSkills.Remove(objectSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectSkillExists(int id)
        {
            return _context.ObjectSkills.Any(e => e.Id == id);
        }
    }
}
