using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDB;
using AppModel.Models;
namespace Product_Management.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
     
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
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


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model)
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
                        return RedirectToAction("Index", "Home");
                    }
                    ViewBag.error = "User Already exists...";

                }

                return View();
 
        }
    }
}