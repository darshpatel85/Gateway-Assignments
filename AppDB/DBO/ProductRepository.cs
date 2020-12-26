using AppModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDB.DBO
{
    public class ProductRepository
    {
        public int AddProduct(ProductModel model)
        {
            using (var context = new ProductDBEntities())
            {
                Product product = new Product()
                {
                    Category = model.Category,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    SDes = model.SDes,
                    LDes = model.LDes,
                    ImageURL = model.ImageURL
                };
                context.Product.Add(product);
                context.SaveChanges();
                return product.Id;
            }


        }
        public List<ProductModel> GetAllProducts()
        {
            using (var context = new ProductDBEntities())
            {
                var result = context.Product
                    .Select(x => new ProductModel()
                    {
                        Id = x.Id,
                        Category = x.Category,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        SDes = x.SDes,
                        ImageURL = x.ImageURL,
                        LDes = x.LDes
                    }).ToList();

                return result;
            }
        }
        public ProductModel GetProduct(int id)
        {
            using (var context = new ProductDBEntities())
            {
                var result = context.Product.Where(x => x.Id == id)
                    .Select(x => new ProductModel()
                    {
                        Id = x.Id,
                        Category = x.Category,
                        SDes = x.SDes,
                        LDes = x.LDes,
                        Price = x.Price,
                        ImageURL = x.ImageURL,
                        Quantity = x.Quantity
                    }).FirstOrDefault();
                return (ProductModel)result;
            }
        }

        public bool UpdateProduct(int id, ProductModel product)
        {
            using (var Context = new ProductDBEntities())
            {
                var res = Context.Product.FirstOrDefault(x => x.Id == id);
                if (res != null)
                {
                    res.Category = product.Category;
                    res.LDes = product.LDes;
                    res.Price = product.Price;
                    res.Quantity = product.Quantity;
                    res.SDes = product.SDes;
                    res.ImageURL = product.ImageURL;
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
                var res = context.Product.FirstOrDefault(x => x.Id == id);
                if (res != null)
                {
                    context.Product.Remove(res);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

