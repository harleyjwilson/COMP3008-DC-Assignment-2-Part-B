using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                if (cookieValue == "1234567")
                {
                    return PartialView("AccountViewAuthenticated");
                }

            }
            // Return the partial view as HTML
            return PartialView("AccountViewDefault");
        }
    }
}

