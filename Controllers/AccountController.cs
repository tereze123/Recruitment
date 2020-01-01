using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recruitment.API.Models;
using Recruitment.API.ViewModels;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace Recruitment.API.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signManager;
        private UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager, AppDbContext context)
        {
            _userManager = userManager;
            _signManager = signManager;
            _context = context;
        }

        [HttpGet]
        public ViewResult Register() {
            return View();                
        }

        [HttpGet]
        public async Task<IActionResult> UserInformation()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            UserInformationViewModel userInformationViewModel = new UserInformationViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return View(userInformationViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserInformationEdit()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            UserInformationViewModel userInformationViewModel = new UserInformationViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return View(userInformationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserInformationEdit(UserInformationViewModel userInformationViewModel)
        {
            AppUser user = await _userManager.GetUserAsync(User);
            user.FirstName = userInformationViewModel.FirstName;
            user.LastName = userInformationViewModel.LastName;
            user.Email = userInformationViewModel.Email;

            var result = await _userManager.UpdateAsync(user);

            return RedirectToAction("UserInformation", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username, FirstName = model.FirstName, LastName = model.LastName, };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(
                    user,
                    passwordChangeViewModel.CurrentPassword,
                    passwordChangeViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View();
                }
            await _signManager.RefreshSignInAsync(user);

                return View("UserInformation");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }
    }
}