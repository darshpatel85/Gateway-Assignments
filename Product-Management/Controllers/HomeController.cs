using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDB.DBO;
namespace Product_Management.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository produtctRepository = null;
        public HomeController()
        {
            produtctRepository = new ProductRepository();
        }

        public ActionResult Index()
        {
            if (Session["Name"] != null) return View();
            else return RedirectToAction("Login", "Auth");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel productModel, HttpPostedFileBase sfile,HttpPostedFileBase lfile)
        {
            var Uid = (int)Session["id"];
            if (sfile != null)
            {
                string ImageName = System.IO.Path.GetFileName(sfile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                sfile.SaveAs(physicalPath);
                productModel.SImg = ImageName;
                ImageName = System.IO.Path.GetFileName(lfile.FileName);
                physicalPath = Server.MapPath("~/Images/" + ImageName);
                lfile.SaveAs(physicalPath);
                productModel.LImg = ImageName;
                if (ModelState.IsValid)
                    {
                        int id = produtctRepository.AddProduct(productModel,Uid);
                        if (id > 0)
                        {
                            ModelState.Clear();
                            return RedirectToAction("getAll");
                        }
                        else
                        {
                            ViewBag.status = "Not stored..";
                        }
                    }
                else
                {
                    ViewBag.status = "State Invalid";
                }
            }
            else
            {
                ViewBag.Status = "file Invalid..";
            }
            return View();
        }

        public ActionResult getAll()
        {
            var uid = (int)Session["id"];
            var result = produtctRepository.GetAllProducts(uid);
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }
        public ActionResult Edit(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = produtctRepository.UpdateProduct(product.Id, product);
            }
            return RedirectToAction("getAll", "Home");
        }

        public ActionResult Delete(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult Delete(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var result = produtctRepository.DeleteProduct(product.Id, product);
            }
            return RedirectToAction("getAll", "Home");
        }
    }
}