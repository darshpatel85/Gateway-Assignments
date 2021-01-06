using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using SourceControlAss.Models;
namespace SourceControlAss.Controllers
{
    public class LoginController : Controller
    {
        private Context context = new Context();
        private static Logger logger = LogManager.GetLogger("MyAppLoggerRule");
        // GET: Login
        public ActionResult Login()
        {
            logger.Info("Into the Login page with GET Request");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee employee)
        {
            try
            {
                logger.Info("Into the Login Controller with POST Request");
                var EmailCheck = context.employees.FirstOrDefault(m => m.Email.Equals(employee.Email));
                if (EmailCheck != null)
                {
                    var PassCheck = context.employees.FirstOrDefault(m => m.Password.Equals(employee.Password));
                    if (PassCheck != null)
                    {
                        logger.Info("Login Successful:-)");
                        TempData["name"] = PassCheck.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        logger.Info("Password is Wrong:-(");
                        ViewBag.error = "Password is wrong...!!";
                        return View();
                    }
                }
                else
                {
                    logger.Info("Email is Wrong:-(");
                    ViewBag.error = "Email is not registered...!!";
                    return View();
                }
            }catch(Exception e)
            {
                logger.Error("Exception : " + e.Message);
                return Content("Exception in login " + e.Message);
            }
            
        }
        //Get /Register
        public ActionResult Register()
        {
            logger.Info("Into the Register page with GET Request");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var EmailCheck = context.employees.FirstOrDefault(m => m.Email.Equals(employee.Email));
                    if (EmailCheck == null)
                    {
                        context.employees.Add(employee);
                        context.SaveChanges();
                        logger.Info("Registration Successful:-)");
                        TempData["name"] = employee.Name;
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.error = "User Already exists...";

                }

                return View();
            }catch(Exception e)
            {
                logger.Error("Error in Registration");
                return Content("Exception : " + e.Message);
            }
        }

    }
}