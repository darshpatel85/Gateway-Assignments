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
        private Context context = new Context();
        // GET: Login
        public ActionResult Login()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee)
        {

            var EmailCheck = context.employees.FirstOrDefault(m => m.Email.Equals(employee.Email));
            if (EmailCheck != null)
            {
                var PassCheck = context.employees.FirstOrDefault(m => m.Password.Equals(employee.Password));
                if (PassCheck != null)
                {
                    TempData["name"] = PassCheck.Name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Password is wrong...!!";
                    return View();
                }           
            }
            else
            {
                ViewBag.error = "Email is not registered...!!";
                return View();
            }
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
                var EmailCheck = context.employees.FirstOrDefault(m => m.Email.Equals(employee.Email));
                if (EmailCheck == null)
                {
                    context.employees.Add(employee);
                    context.SaveChanges();
                    TempData["name"] = employee.Name;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.error = "User Already exists...";

            }

            return View();
        }

    }
}