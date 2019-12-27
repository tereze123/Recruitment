using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.API.Models;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class ObjectTypesController : Controller
    {
        private readonly AppDbContext _context;

        public ObjectTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ObjectTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ObjectTypes.ToListAsync());
        }

        // GET: ObjectTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectType = await _context.ObjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectType == null)
            {
                return NotFound();
            }

            return View(objectType);
        }

        // GET: ObjectTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ObjectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ObjectType objectType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objectType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(objectType);
        }

        // GET: ObjectTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectType = await _context.ObjectTypes.FindAsync(id);
            if (objectType == null)
            {
                return NotFound();
            }
            return View(objectType);
        }

        // POST: ObjectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ObjectType objectType)
        {
            if (id != objectType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objectType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjectTypeExists(objectType.Id))
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
            return View(objectType);
        }

        // GET: ObjectTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objectType = await _context.ObjectTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (objectType == null)
            {
                return NotFound();
            }

            return View(objectType);
        }

        // POST: ObjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objectType = await _context.ObjectTypes.FindAsync(id);
            _context.ObjectTypes.Remove(objectType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectTypeExists(int id)
        {
            return _context.ObjectTypes.Any(e => e.Id == id);
        }
    }
}
