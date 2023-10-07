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
            return PartialView("AdminViewDefault");
        }

        [HttpPost("contact")]
        public IActionResult UpdateAdminContact([FromBody] Admin adminUpdate)
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<Admin>("api/admins/" + Request.Cookies["Username"]);
                    task.Wait();
                    var admin = task.Result;

                    if (admin != null)
                    {
                        if (adminUpdate.Phone != null && adminUpdate.Phone != "")
                        {
                            admin.Phone = adminUpdate.Phone;
                        }
                        if (adminUpdate.Email != null && adminUpdate.Phone != "")
                        {
                            admin.Email = adminUpdate.Email;
                        }
                        var updateTask = client.PutAsJsonAsync<Admin>("api/admins/" + admin.Username, admin);
                        updateTask.Wait();
                        ViewData["Admin"] = admin;
                    }
                    return PartialView("AdminViewAuthenticated");
                }
            }
            return PartialView("AdminViewDefault");
        }

        [HttpPost("password")]
        public IActionResult UpdateAdminPassword([FromBody] Admin adminUpdate)
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<Admin>("api/admins/" + Request.Cookies["Username"]);
                    task.Wait();
                    var admin = task.Result;

                    if (admin != null)
                    {
                        if (adminUpdate.Password != null)
                        {
                            admin.Password = adminUpdate.Password;
                        }
                        var updateTask = client.PutAsJsonAsync<Admin>("api/admins/" + admin.Username, admin);
                        updateTask.Wait();
                        ViewData["Admin"] = admin;
                    }
                    return PartialView("AdminViewAuthenticated");
                }
            }
            return PartialView("AdminViewDefault");
        }

        [HttpGet("users")]
        public IActionResult GetAdminUsersView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    return PartialView("Users/AdminUsersViewAuthenticated");
                }
            }
            return PartialView("Users/AdminUsersViewDefault");
        }

        [HttpPost("users/create")]
        public IActionResult AdminCreateUser([FromBody] User newUser)
        {
            var response = new { success = false };
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    User user = new User(newUser.Username)
                    {
                        Name = newUser.Name,
                        Email = newUser.Email,
                        Address = newUser.Address,
                        Phone = newUser.Phone,
                        Picture = newUser.Picture,
                        Password = newUser.Password
                    };

                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.PostAsJsonAsync<User>("api/users/", user);
                    task.Wait();
                    var result = task.Result;
                    response = new { success = true };
                }
            }
            return Json(response);
        }

        [HttpPost("users/edit")]
        public IActionResult AdminEditUser([FromBody] User editUser)
        {
            var response = new { success = false };
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                if (AdminLoginController.verifyAdminSessionID(Request.Cookies["Username"], Request.Cookies["SessionID"]))
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:5181/");
                    var task = client.GetFromJsonAsync<User>("api/users/" + editUser.Username);
                    task.Wait();
                    var verifyUser = task.Result;
                    if (verifyUser != null)
                    {
                        verifyUser.Name = verifyUser.Name == editUser.Name ? verifyUser.Name : editUser.Name;
                        verifyUser.Email = verifyUser.Email == editUser.Email ? verifyUser.Email : editUser.Email; ;
                        verifyUser.Address = verifyUser.Address == editUser.Address ? verifyUser.Address : editUser.Address; ;
                        verifyUser.Phone = verifyUser.Phone == editUser.Phone ? verifyUser.Phone : editUser.Phone; ;
                        verifyUser.Picture = verifyUser.Picture == editUser.Picture ? verifyUser.Picture : editUser.Picture; ;

                        var updateTask = client.PutAsJsonAsync<User>("api/users/" + verifyUser.Username, verifyUser);
                        updateTask.Wait();
                        var result = updateTask.Result;
                        response = new { success = true };
                    }
                }
            }
            return Json(response);
        }
    }
}