using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruitment.API.Enums;
using Recruitment.API.Models;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class VacanciesController : Controller
    {
        private readonly AppDbContext _context;

        public VacanciesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vacancies
        public async Task<IActionResult> Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return View(await _context.Vacancies.Where(vacancy => vacancy.Name.Contains(id.ToLower())).ToListAsync());
            }

            return View(await _context.Vacancies.ToListAsync());
        }

        // GET: Vacancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vacancy == null)
            {
                return NotFound();
            }

            List<int> skillIds = _context.ObjectSkills
                 .Where(os => os.ObjectId == vacancy.Id)
                 .Where(os => os.ObjectTypeId == (int)ObjectTypeEnum.Vakance).Select(o => o.Id)
                 .ToList();

            VacancyViewModel vacancyViewModel = new VacancyViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                OpeningDate = vacancy.OpeningDate,
                ClosingDate = vacancy.ClosingDate,
                Test = vacancy.Test,
                TestId = vacancy.TestId,
                CandidateCount = _context.Candidates.Where(c => c.VacancyId == vacancy.Id).Count(),
                Skills = _context.Skills.Where(s => skillIds.Contains(s.Id)).ToList(),
            };


            return View(vacancyViewModel);
        }

        // GET: Vacancies/Create
        public IActionResult Create()
        {

            ViewData["SkillTypeName"] = new SelectList(_context.SkillTypes, "Id", "Name");
            return View();
        }

        // POST: Vacancies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VacancyViewModel VacancyViewModel)
        {
            if (ModelState.IsValid)
            {
                Vacancy vacancy = new Vacancy
                {
                    Name = VacancyViewModel.Name,
                    OpeningDate = VacancyViewModel.OpeningDate,
                    ClosingDate = VacancyViewModel.ClosingDate,
                    TestId = VacancyViewModel.TestId,
                    Test = VacancyViewModel.Test,
                };
                _context.Add(vacancy);

                await _context.SaveChangesAsync();

                foreach (var item in VacancyViewModel.Skills)
                {
                    if (string.IsNullOrEmpty(item.Value))
                    {
                        continue;
                    }

                    Skill skill = new Skill
                    {
                        Value = item.Value,
                        SkillTypeId = item.SkillTypeId,
                    };
                    _context.Add(skill);

                    await _context.SaveChangesAsync();

                    int vacancyId = vacancy.Id;
                    int skillId = skill.Id;

                    ObjectSkill objectSkill = new ObjectSkill
                    {
                        ObjectId = vacancyId,
                        ObjectTypeId = (int)ObjectTypeEnum.Vakance,
                        SkillId = skillId,
                    };
                    _context.Add(objectSkill);

                    await _context.SaveChangesAsync();
                }
                    return RedirectToAction(nameof(Index));
                
            }
            ViewData["SkillTypeName"] = new SelectList(_context.SkillTypes, "Id", "Name");
            return View(VacancyViewModel);
        }

        // GET: Vacancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            List<int> skillIds = _context.ObjectSkills
                .Where(os => os.ObjectId == vacancy.Id)
                .Where(os => os.ObjectTypeId == (int)ObjectTypeEnum.Vakance).Select(o => o.Id)
                .ToList();

            VacancyViewModel vacancyViewModel = new VacancyViewModel
            {
                Name = vacancy.Name,
                OpeningDate = vacancy.OpeningDate,
                ClosingDate = vacancy.ClosingDate,
                TestId = vacancy.TestId,
                Test = vacancy.Test,
                CandidateCount = _context.Candidates.Where(c => c.VacancyId == vacancy.Id).Count(),
                Skills = _context.Skills.Where(s => skillIds.Contains(s.Id)).ToList(),
            };


            return View(vacancyViewModel);
        }

        // POST: Vacancies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VacancyViewModel vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyExists(vacancy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                foreach (var item in vacancy.Skills)
                {
                    try
                    {
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SkillExists(item.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(vacancy);
        }

        // GET: Vacancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // POST: Vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancies.Any(e => e.Id == id);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
