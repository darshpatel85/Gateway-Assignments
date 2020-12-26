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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductModel productModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalPath);
                productModel.ImageURL = ImageName;
                if (ModelState.IsValid)
                    {
                        int id = produtctRepository.AddProduct(productModel);
                        if (id > 0)
                        {
                            ModelState.Clear();
                            return RedirectToAction("Index");
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

        public ActionResult Index()
        {
            var result = produtctRepository.GetAllProducts();
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
            return RedirectToAction("getAllProducts", "Home");
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
            return RedirectToAction("getAllProducts", "Home");
        }
    }
}