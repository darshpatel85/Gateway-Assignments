using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DBO
{
    //Database Operations
    public class ProductRepository
    {
        //Add new  Product
        //return Product Id

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



        //return Product List with sorting in asceding and dseceding order
        //Name,Category,price
        public List<ProductModel> GetAllProducts(int id, string sortBy, string order, string search)
        {
            using (var context = new ProductDBEntities())
            {
                var result = new List<Product>();
                if (search == null)
                    result = context.Products.Where(x => x.User_id == id).ToList();
                else
                {
                    result = context.Products.Where(x => x.User_id == id && (x.Name.Contains(search) || x.Price.ToString().Contains(search) || x.Category.Contains(search)))
                              .ToList();
                }
                switch (sortBy)
                {
                    case "Name":
                        if (order == "Desc") result = result.OrderByDescending(x => x.Name).ToList();
                        else result = result.OrderBy(x => x.Name).ToList();
                        break;
                    case "Category":
                        if (order == "Desc") result = result.OrderByDescending(x => x.Category).ToList();
                        else result = result.OrderBy(x => x.Category).ToList();
                        break;
                    case "Price":
                        if (order == "Desc") result = result.OrderByDescending(x => x.Price).ToList();
                        else result = result.OrderBy(x => x.Price).ToList();
                        break;
                    default:
                        if (order == "Desc") result = result.OrderByDescending(x => x.Name).ToList();
                        else result = result.OrderBy(x => x.Name).ToList();
                        break;
                }
                var ans = result.Select(x => new ProductModel()
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
                return ans;
            }
        }
        //return Product based on Id
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

        //update product details
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

        //Delete Product
        public bool DeleteProduct(int id)
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

