using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using SourceControl_Annot.Models;

namespace SourceControl_Annot.Controllers
{
    public class UserController : Controller
    {
        //Get : Form
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        // Post: Form
        [HttpPost]
        public ActionResult Form(User user)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Name = user.Name;
                ViewBag.cred = user.cred;
                ViewBag.email = user.Email;
                ViewBag.Password = user.Password;
                ViewBag.Phone = user.Phone;
                return View();
            }
            else
            {
                return View();
            }
           
        }
    }
}