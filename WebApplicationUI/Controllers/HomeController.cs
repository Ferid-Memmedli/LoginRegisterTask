using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Constants;

namespace WebApplicationUI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Authorize(Roles = $"{RoleConstant.Admin},{RoleConstant.Guest}")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}