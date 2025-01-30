using Ost_Inventory_b4.Models;
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
            Session["UserName"] = "";
            if (Session["Message"] == null)
                Session["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(string txtUserName, string txtPassword) 
        {
            Session["UserName"] = "";
            string Message = "Unauthorized";
            //if (txtUserName == "Ost" && txtPassword == "123")
            BaseAccount baseAccount = new BaseAccount();
            if(baseAccount.VerifyUser(txtUserName,txtPassword))
            {
                Message = "Authorized";
                Session["UserName"] = txtUserName;
                return RedirectToAction("Dashboard", "Inventory");
            }
            ViewBag.Message = Message;
            return View("Login");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return View("Login");
        }
    }
}