using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recruitment.API.Models;
using Recruitment.API.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: Administrator
        [HttpGet]
        public ActionResult Index()
        {
            var users = _userManager.Users;

            foreach (var item in collection)
            {

            }
            return View(users);
        }

        // GET: Administrator/Details/5
        public async Task<IActionResult> Details(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            IList<string> roleList = await _userManager.GetRolesAsync(user);

            UserTypeViewModel userTypeViewModel = new UserTypeViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roleList.ToList().FirstOrDefault(),
            };

            return View(userTypeViewModel);
        }

        // GET: Administrator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Administrator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Administrator/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}