using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetViewAsync()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                // if (cookieValue == "1234567")
                if (LoginController.verifySessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<User>("api/admins/" + Request.Cookies["Username"]);
                    task.Wait();
                    var admin = task.Result;
                    ViewData["Admin"] = admin;
                    return PartialView("AccountViewAuthenticated");
                }

            }
            // Return the partial view as HTML
            return PartialView("AccountViewDefault");
        }
    }
}

