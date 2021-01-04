using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDB.DBO;
using PagedList;

namespace Product_Management.Controllers
{
    //check session is set or not
    [SessionCheck]
    // : /Home
    public class HomeController : Controller
    {
        ProductRepository produtctRepository = null;
        public HomeController()
        {
            //connect with database Access layer 
            produtctRepository = new ProductRepository();
        }

        //GET:/Index
        public ActionResult Index()
        {
            if (Session["Name"] != null) return View();
            else return RedirectToAction("Login", "Auth");
        }
        //GET:/Index
        public ActionResult Create()
        {
            return View();
        }

        //POST:/Index
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
                            ViewBag.status = "Product added successfully..";
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

        //GET:/getAll
        public ActionResult getAll(string sortBy,string order,string search,int? page)
        {
            var result =new List<ProductModel>();
            int pageSize = 2;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.order = order;
            ViewBag.sortBy = sortBy;
            var uid = (int)Session["id"];
            if (!String.IsNullOrEmpty(search))
            { 
                result = produtctRepository.GetAllProducts(uid, sortBy, order,search);
            }
            else
            {
                result = produtctRepository.GetAllProducts(uid, sortBy, order,null);
            }
            var products = result.ToPagedList(pageIndex, pageSize);
            return View(products);
            
        }



        //GET:/Details
        public ActionResult Details(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }

        //GET:/Edit
        public ActionResult Edit(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }
        //POST:/Edit
        [HttpPost]
        public ActionResult Edit(ProductModel product, HttpPostedFileBase sfile, HttpPostedFileBase lfile)
        {
            if (sfile != null)
            {
                string ImageName = System.IO.Path.GetFileName(sfile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                sfile.SaveAs(physicalPath);
                product.SImg = ImageName;
            }
            if (lfile != null)
            {
                string ImageName = System.IO.Path.GetFileName(lfile.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                lfile.SaveAs(physicalPath);
                product.LImg = ImageName;
            }
                    var result = produtctRepository.UpdateProduct(product.Id, product);
                    if (result)
                    {
                        ModelState.Clear();
                        ViewBag.status = "Product Updated Successfully..";
                    
                    }
                    else
                    {
                        ViewBag.status = "Not stored..";
                    }
                
            return View();
        }


        //GET:/Delete
        public ActionResult Delete(int id)
        {
            var result = produtctRepository.GetProduct(id);
            return View(result);
        }

        //POST:/Delete
        [HttpPost]
        public ActionResult Delete(int id,ProductModel product)
        {
                var result = produtctRepository.DeleteProduct(product.Id);
                if(result) ViewBag.status = "Product Deleted Successfully";            
            return View();
        }
        //GET:/About
        public ActionResult About()
        {
            return View();
        }
    }
}