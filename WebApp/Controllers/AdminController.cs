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
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<User>("api/admins/" + Request.Cookies["Username"]);
                    task.Wait();
                    var admin = task.Result;
                    ViewData["Admin"] = admin;
                    return PartialView("AdminViewAuthenticated");
                }

            }
            // Return the partial view as HTML
            return PartialView("AdminViewDefault");
        }

        [HttpGet("contact")]
        public IActionResult UpdateAdminContact()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                // if (cookieValue == "1234567")
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<User>("api/admins/" + Request.Cookies["Username"]);
                    task.Wait();
                    var admin = task.Result;
                    ViewData["Admin"] = admin;
                    return PartialView("AdminViewAuthenticated");
                }
            }
            // Return the partial view as HTML
            return PartialView("AdminViewDefault");
        }
    }
}

