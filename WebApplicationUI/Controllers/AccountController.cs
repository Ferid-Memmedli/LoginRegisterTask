using BusinessLayer.Abstract;
using EntitiesLayer.Concrete.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationUI.Models;

namespace WebApplicationUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;
        private readonly UserManager<User> userManager;

        public AccountController(IAuthService authService, UserManager<User> userManager)
        {
            this.authService = authService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Login(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin request)
        {
            string returnUrl = TempData["ReturnUrl"] is not null ? TempData["ReturnUrl"].ToString() : null;

            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var entity = new User
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            };

            var result = await authService.LoginAsync(entity);
            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                };
                return View(request);
            }

            if (!string.IsNullOrEmpty(returnUrl))
                if (Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken,]
        public async Task<IActionResult> Register(User request, string[] addresses)
        {
            if (!ModelState.IsValid) return View(request);

            var result = await authService.RegisterAsync(request, addresses);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors) ModelState.AddModelError("", error);
                return View(request);
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
