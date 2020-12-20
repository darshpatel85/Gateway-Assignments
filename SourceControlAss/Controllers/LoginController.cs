using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SourceControlAss.Models;
namespace SourceControlAss.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee)
        {
            return employee.Email=="darsh123@xyz.com" && employee.Password=="12345678" ? RedirectToAction("Index", "Home") : (ActionResult)View();
        }

        //Get /Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Employee employee)
        {
            
            if (ModelState.IsValid)
            {
                TempData["Name"] = employee.Name;
                return RedirectToAction("Index","Home");
            }

            return View();
        }
         
    }
}