using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recruitment.API.Enums;
using Recruitment.API.Models;
using Recruitment.API.ViewModels;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly AppDbContext _context;

        public CandidatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Candidates
        public async Task<IActionResult> Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return View(await _context
                    .Candidates
                    .Where(candidate => candidate.Name.Contains(id.ToLower()) || candidate.Surname.Contains(id.ToLower()))
                    .Include(c => c.Status)
                    .Include(c => c.Test)
                    .Include(c => c.Vacancy)
                    .ToListAsync());
            }

            var appDbContext = _context.Candidates.Include(c => c.Status).Include(c => c.Test).Include(c => c.Vacancy);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Status)
                .Include(c => c.Test)
                .Include(c => c.Vacancy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            List<int> skillIds = _context.ObjectSkills
                 .Where(os => os.ObjectId == candidate.Id)
                 .Where(os => os.ObjectTypeId == (int)ObjectTypeEnum.Kandidats).Select(o => o.Id)
                 .ToList();

            CandidateViewModel candidateViewModel = new CandidateViewModel
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Surname = candidate.Surname,
                PhoneNumber = candidate.PhoneNumber,
                Email = candidate.Email,
                VacancyId = candidate.VacancyId,
                Vacancy = candidate.Vacancy,
                StatusId = candidate.StatusId,
                Status = candidate.Status,
                TestId = candidate.TestId,
                Test = candidate.Test,
                RegistrationDate = candidate.RegistrationDate,
                Skills = _context.Skills.Where(s => skillIds.Contains(s.Id)).ToList(),
            };

            return View(candidateViewModel);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            ViewData["StatusName"] = new SelectList(_context.Status, "Id", "Name");
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id");
            ViewData["VacancyName"] = new SelectList(_context.Vacancies, "Id", "Name");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,PhoneNumber,Email,VacancyId,StatusId,TestId,RegistrationDate,Skills")] CandidateViewModel candidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidate);
                await _context.SaveChangesAsync();

                foreach (var item in candidate.Skills)
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

                    int candidateId = candidate.Id;
                    int skillId = skill.Id;

                    ObjectSkill objectSkill = new ObjectSkill
                    {
                        ObjectId = candidateId,
                        ObjectTypeId = (int)ObjectTypeEnum.Kandidats,
                        SkillId = skillId,
                    };
                    _context.Add(objectSkill);

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusName"] = new SelectList(_context.Status, "Id", "Name", candidate.StatusId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", candidate.TestId);
            ViewData["VacancyName"] = new SelectList(_context.Vacancies, "Id", "Name", candidate.VacancyId);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            List<int> skillIds = _context.ObjectSkills
                .Where(os => os.ObjectId == candidate.Id)
                .Where(os => os.ObjectTypeId == (int)ObjectTypeEnum.Kandidats).Select(o => o.Id)
                .ToList();

            CandidateViewModel candidateViewModel = new CandidateViewModel
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Surname = candidate.Surname,
                PhoneNumber = candidate.PhoneNumber,
                Email = candidate.Email,
                VacancyId = candidate.VacancyId,
                Vacancy = candidate.Vacancy,
                StatusId = candidate.StatusId,
                Status = candidate.Status,
                TestId = candidate.TestId,
                Test = candidate.Test,
                RegistrationDate = candidate.RegistrationDate,
                Skills = _context.Skills.Where(s => skillIds.Contains(s.Id)).ToList(),
            };

            ViewData["StatusName"] = new SelectList(_context.Status, "Id", "Name", candidate.StatusId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", candidate.TestId);
            ViewData["VacancyName"] = new SelectList(_context.Vacancies, "Id", "Name", candidate.VacancyId);
            return View(candidateViewModel);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,PhoneNumber,Email,VacancyId,StatusId,TestId,RegistrationDate,Skills")] CandidateViewModel candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                foreach (var item in candidate.Skills)
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
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", candidate.StatusId);
            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Id", candidate.TestId);
            ViewData["VacancyId"] = new SelectList(_context.Vacancies, "Id", "Id", candidate.VacancyId);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Status)
                .Include(c => c.Test)
                .Include(c => c.Vacancy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ToDo(int? id)
        {
            ViewData["VacancyNames"] = new SelectList(_context.Vacancies, "Id", "Name");
            if (id != null)
            {
                var candidatesFiltered = _context.Candidates.Include(c => c.Status).Include(c => c.Vacancy).Where(c => c.VacancyId == id);
                return View(await candidatesFiltered.ToListAsync());
            }

            var candidates = _context.Candidates.Include(c => c.Status).Include(c => c.Vacancy);
            int firstVacancyId = candidates.FirstOrDefault().VacancyId;

            return View(await candidates.Where(c => c.VacancyId == firstVacancyId).ToListAsync());
        }


        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}
