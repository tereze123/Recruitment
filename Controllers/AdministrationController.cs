using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            List<UserTypeViewModel> userTypeViewModels = new List<UserTypeViewModel>();

            foreach (var user in users)
            {
                IList<string> roleList = await _userManager.GetRolesAsync(user);
                UserTypeViewModel userTypeViewModel = new UserTypeViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = roleList.ToList().FirstOrDefault(),
                };

                userTypeViewModels.Add(userTypeViewModel);
            }
            return View(userTypeViewModels);
        }

        // GET: Administrator/Details/5
        public async Task<IActionResult> Details(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            IList<string> roleList = await _userManager.GetRolesAsync(user);

            UserTypeViewModel userTypeViewModel = new UserTypeViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roleList.ToList().FirstOrDefault(),
            };
            return View(userTypeViewModel);
        }

        // GET: Administrator/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            IList<string> roleList = await _userManager.GetRolesAsync(user);

            UserTypeViewModel userTypeViewModel = new UserTypeViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roleList.ToList().FirstOrDefault(),
            };

            ViewData["RoleNames"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

            return View(userTypeViewModel);
        }

        // POST: Administrator/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Role,FirstName,LastName,Email")] UserTypeViewModel userTypeViewModel)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(id);

                user.FirstName = userTypeViewModel.FirstName;
                user.LastName = userTypeViewModel.LastName;
                user.Email = userTypeViewModel.Email;


                var identityResult = await _userManager.UpdateAsync(user);
                var identityRoleResult = await _userManager.AddToRoleAsync(user, userTypeViewModel.Role);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            IList<string> roleList = await _userManager.GetRolesAsync(user);

            UserTypeViewModel userTypeViewModel = new UserTypeViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roleList.ToList().FirstOrDefault(),
            };

            return View(userTypeViewModel);
        }

        // POST: Administrator/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}