﻿using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DBO
{
    public class ProductRepository
    {

        public int AddProduct(ProductModel model, int id)
        {
            using (var context = new ProductDBEntities())
            {
                Product product = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    SDes = model.SDes,
                    LDes = model.LDes,
                    SImg = model.SImg,
                    LImg = model.LImg,
                    User_id = id,
                };
                context.Products.Add(product);
                context.SaveChanges();
                return product.Id;
            }


        }
        public List<ProductModel> GetAllProducts(int id, string sortBy, string order)
        {
            using (var context = new ProductDBEntities())
            {
                var result = context.Products.Where(x => x.User_id == id).ToList();
                switch (sortBy)
                {
                    case "Name":
                        if (order == "Desc")    result = context.Products.Where(x => x.User_id == id).OrderByDescending(x => x.Name).ToList();
                        else result = context.Products.Where(x => x.User_id == id).OrderBy(x => x.Name).ToList();
                        break;
                    case "Category":
                        if (order == "Desc") result = context.Products.Where(x => x.User_id == id).OrderByDescending(x => x.Category).ToList();
                        else result = context.Products.Where(x => x.User_id == id).OrderBy(x => x.Category).ToList();
                        break;
                    case "Price":
                        if (order == "Desc") result = context.Products.Where(x => x.User_id == id).OrderByDescending(x => x.Price).ToList();
                        else result = context.Products.Where(x => x.User_id == id).OrderBy(x => x.Price).ToList();
                        break;
                    default:
                        if (order == "Desc") result = context.Products.Where(x => x.User_id == id).OrderByDescending(x => x.Name).ToList();
                        else result = context.Products.Where(x => x.User_id == id).OrderBy(x => x.Name).ToList();
                        break;
                }
                    var ans = result.Select(x => new ProductModel() {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        SDes = x.SDes,
                        SImg = x.SImg,
                        LImg = x.LImg,
                        LDes = x.LDes
                    }).ToList();
                    return ans;   
            }
        }
        public List<ProductModel> GetAllProducts(int id, string search)
        {
            if (search != null)
            {
                using (var context = new ProductDBEntities())
                {
                    var result = context.Products.Where(x => x.User_id == id && (x.Name == search || x.Price.ToString() == search || x.Category == search))
                        .Select(x => new ProductModel()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Category = x.Category,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            SDes = x.SDes,
                            SImg = x.SImg,
                            LImg = x.LImg,
                            LDes = x.LDes
                        }).ToList();
                    return result;
                }
            }
            else
            {
                return GetAllProducts(id,"Name","Ass");
            }
        }
        public ProductModel GetProduct(int id)
        {
            using (var context = new ProductDBEntities())
            {
                var result = context.Products.Where(x => x.Id == id)
                    .Select(x => new ProductModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category,
                        SDes = x.SDes,
                        LDes = x.LDes,
                        Price = x.Price,
                        SImg = x.SImg,
                        LImg = x.LImg,
                        Quantity = x.Quantity
                    }).FirstOrDefault();
                return (ProductModel)result;
            }
        }

        public bool UpdateProduct(int id, ProductModel product)
        {
            using (var Context = new ProductDBEntities())
            {
                var res = Context.Products.FirstOrDefault(x => x.Id == id);
                if (res != null)
                {
                    res.Name = product.Name;
                    res.Category = product.Category;
                    res.LDes = product.LDes;
                    res.Price = product.Price;
                    res.Quantity = product.Quantity;
                    res.SDes = product.SDes;
                    if (product.SImg != null) res.SImg = product.SImg;
                    if (product.LImg != null) res.LImg = product.LImg;
                    Context.SaveChanges();
                    return true;
                }
                return false;

            }
        }

        public bool DeleteProduct(int id, ProductModel product)
        {
            using (var context = new ProductDBEntities())
            {
                var res = context.Products.FirstOrDefault(x => x.Id == id);
                if (res != null)
                {
                    context.Products.Remove(res);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

