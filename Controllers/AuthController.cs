using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ost_Inventory_b4.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtUserName, string txtPassword)
        {
            string Message = "Unauthorized";
            if (txtUserName == "Ost" && txtPassword == "123")
            {
                Message = "Authorized";
                return RedirectToAction("Dashboard", "Inventory");
            }
            ViewBag.Message = Message;
            return View("Login");
        }
    }
}