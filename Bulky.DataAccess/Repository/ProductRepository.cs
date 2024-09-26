using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var tempObj = _db.Products.FirstOrDefault(u => u.ProductId == obj.ProductId);
            if(tempObj != null)
            {
                tempObj.Title = obj.Title;
                tempObj.ISBN = obj.ISBN;
                tempObj.ListPrice = obj.ListPrice;
                tempObj.Price = obj.Price;
                tempObj.Price50 = obj.Price50;
                tempObj.Price100 = obj.Price100;
                tempObj.Description = obj.Description;
                tempObj.CategoryId = obj.CategoryId;
                tempObj.Author = obj.Author;
                if(obj.ImgUrl != null)
                {
                    tempObj.ImgUrl = obj.ImgUrl;
                }
            }

        }
    }
}
