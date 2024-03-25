using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Demo1_Day2.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult CreateCookie()
        {
            int id = 1;
            string name = "Aya";
            Response.Cookies.Append("id", id.ToString());
            Response.Cookies.Append("firstName", name);
            
            Response.Cookies.Append("id", id.ToString(), new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(10)

            });
            return View();
        }
        public IActionResult GetCookie()
        {
            int id = int.Parse(Request.Cookies["id"]); 
            //this will throw an exception if the cookie does not exist
            string name = Request.Cookies["firstName"];
            //to delete a cookie
            Response.Cookies.Delete("id");
            return Content($"id: {id}, name: {name}");
        }

        public IActionResult CreateSession(int id, string name)
        {
            
            HttpContext.Session.SetInt32("id", id);
            HttpContext.Session.SetString("name", name);
            return View();
        }
        public IActionResult GetSession()
        {
            int? id = HttpContext.Session.GetInt32("id");//nullable int
            string name = HttpContext.Session.GetString("name");
            return Content($"id: {id}, name: {name}");
        }
        public IActionResult CreateTempData()
        {
            TempData["id"] = 1;
            TempData["name"] = "Aya";
            return View();
        }
        
        public IActionResult GetTempData()
        {
           /* int id = (int)TempData["id"];
            string name = (string)TempData["name"];*/
            int id = (int)TempData.Peek("id");
            string name = (string)TempData.Peek("name");
            return Content($"id: {id}, name: {name}");
        }

    }
}
