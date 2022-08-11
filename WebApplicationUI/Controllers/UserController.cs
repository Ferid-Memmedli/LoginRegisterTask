using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Constants;
using WebApplicationUI.Models;

namespace WebApplicationUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserService manager;
        protected readonly IAuthService authService;

        public UserController(IAppUserService manager, IAuthService authService)
        {
            this.manager = manager;
            this.authService = authService;
        }

        [Authorize(Roles = $"{RoleConstant.Admin}, {RoleConstant.Guest}")]
        public async Task<IActionResult> Index()
        {
            var users = await manager.GetAllAsync();

            UserViewModel userViewModel = new()
            {
                Users = users.Data
            };
            return View(userViewModel);
        }

        [Authorize(Roles = $"{RoleConstant.Admin}, {RoleConstant.Guest}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await manager.GetByIdAsync(id);
            if (result.IsSuccess) return PartialView("_DetailPartial", result.Data);
            return NotFound(result);
        }
    }
}
