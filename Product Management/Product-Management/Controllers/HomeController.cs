using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDB.DBO;
using PagedList;
using NLog;

namespace Product_Management.Controllers
{
    //check session is set or not
    [SessionCheck]
    // : /Home
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetLogger("LoggerRule");
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
        public ActionResult Create(ProductModel productModel, HttpPostedFileBase sfile, HttpPostedFileBase lfile)
        {
            try
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
                        int id = produtctRepository.AddProduct(productModel, Uid);
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
            catch (Exception e)
            {
                logger.Error("Exception in Adding Product :" + e.Message);
                return Content(e.Message);
            }
        }

        //GET:/getAll
        public ActionResult getAll(string sortBy, string order, string search, int? page)
        {
            try
            {
                var result = new List<ProductModel>();
                int pageSize = 5;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                ViewBag.order = order;
                ViewBag.sortBy = sortBy;
                var uid = (int)Session["id"];
                if (!String.IsNullOrEmpty(search))
                {
                    result = produtctRepository.GetAllProducts(uid, sortBy, order, search);
                }
                else
                {
                    result = produtctRepository.GetAllProducts(uid, sortBy, order, null);
                }
                var products = result.ToPagedList(pageIndex, pageSize);
                return View(products);
            }
            catch (Exception e)
            {
                logger.Error("Exception at adding all product : " + e.Message);
                return Content(e.Message);
            }
        }



        //GET:/Details
        public ActionResult Details(int id)
        {
            try
            {
                var result = produtctRepository.GetProduct(id);
                return View(result);
            }
            catch (Exception e)
            {
                logger.Error("Exception at Product detail of Id : " + id + " : " + e.Message);
                return Content(e.Message);
            }
        }

        //GET:/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var result = produtctRepository.GetProduct(id);
                return View(result);
            }
            catch (Exception e)
            {
                logger.Error("Exception at Product Edit of Id : " + id + " : " + e.Message);
                return Content(e.Message);
            }
        }
        //POST:/Edit
        [HttpPost]
        public ActionResult Edit(ProductModel product, HttpPostedFileBase sfile, HttpPostedFileBase lfile)
        {
            try
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
            catch (Exception e)
            {
                logger.Error("Exception at Product Edit : " + e.Message);
                return Content(e.Message);
            }
        }


        //GET:/Delete
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var result = produtctRepository.GetProduct(id);
        //        return View(result);
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("Exception at Product delete of Id : " + id + " : " + e.Message);
        //        return Content(e.Message);
        //    }
        //}

        ////POST:/Delete
        //[HttpPost]
        //public ActionResult Delete(int id, ProductModel product)
        //{
        //    try
        //    {
        //        var result = produtctRepository.DeleteProduct(product.Id);
        //        if (result) ViewBag.status = "Product Deleted Successfully";
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("Exception at Product delete of Id : " + id + " : " + e.Message);
        //        return Content(e.Message);
        //    }
        //}



        //POST:/Delete
        [HttpPost]
        public ActionResult getAll(FormCollection formCollection)
        {
            try
            {
                string[] ids = formCollection["ID"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    int x;
                    if (id == "false") continue;
                    else
                        x = Convert.ToInt32(id);
                    var result = produtctRepository.DeleteProduct(x);
                }

                return RedirectToAction("getAll");
            }
            catch (Exception e)
            {
                logger.Error("Exception at Product delete : " + e.Message);
                return Content(e.ToString());
            }
        }
        //GET:/About
        public ActionResult About()
        {
            return View();
        }
    }
}