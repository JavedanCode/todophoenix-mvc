using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoPhoenix.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
