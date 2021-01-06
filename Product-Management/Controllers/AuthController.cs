using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDB;
using AppModel.Models;
using NLog;

namespace Product_Management.Controllers
{
    // GET: Auth
    public class AuthController : Controller
    {
        private static Logger logger = LogManager.GetLogger("LoggerRule");
        //GET:/Login   
        public ActionResult Login()
        {
            return View();
        }
       
        
        //POST:/Login
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            try
            {
                var context = new ProductDBEntities();
                var EmailCheck = context.Users.FirstOrDefault(m => m.Email.Equals(user.Email));
                if (EmailCheck != null)
                {
                    var PassCheck = context.Users.FirstOrDefault(m => m.Password.Equals(user.Password));
                    if (PassCheck != null)
                    {
                        Session["id"] = PassCheck.Id;
                        Session["Name"] = PassCheck.Name;
                        logger.Info("User logged in with : " + user.Email);
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
            }catch(Exception e)
            {
                logger.Error("Exception in Login : " + e.Message);
                return Content(e.Message);
            }
        }

        //GET:/Register
        public ActionResult Register()
        {
            return View();
        }

        //POST:/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var context = new ProductDBEntities();
                    var EmailCheck = context.Users.FirstOrDefault(m => m.Email.Equals(model.Email));
                    if (EmailCheck == null)
                    {
                        User user = new User()
                        {
                            Email = model.Email,
                            Name = model.Name,
                            Password = model.Password
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                        Session["id"] = user.Id;
                        Session["Name"] = user.Name;
                        logger.Info("User Registered with : " + user.Email);
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.error = "User Already exists...";

                }

                return View();
            }catch(Exception e)
            {
                logger.Error("Exception in register : " + e.Message);
                return Content(e.Message);
            }
 
        }

        //GET:/Logout
        public ActionResult Logout()
        {
            logger.Info("User with id : " + Session["id"] + "is logged out");
            Session["id"] = null;
            Session["name"] = null;
            return RedirectToAction("Login");
            
        }
    }
}