using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverDAL;
using System.Data;
using UnileverObject;
using System.Linq.Expressions;

namespace UnileverBLL
{
    public class UnileverBLL : IBLL
    {
        public UnileverEntities UnileverEntities { get; set; }
        public UnileverBLL()
        {
            UnileverEntities = new UnileverEntities();
        }
        public List<Product> GetListProducts()
        {
            return UnileverEntities.Products.ToList();
        }
        public Category GetCategoryById(int catid)
        {
            return UnileverEntities.Categories.Where(c => c.ID == catid)
                .FirstOrDefault();
        }
        public List<Category> GetListCategories()
        {
            return UnileverEntities.Categories.ToList();
        }
        public List<Distributor> GetListDistributors()
        {
            return this.UnileverEntities.Distributors.ToList();
        }

        public void AddDistributor(string name, string email, string phone, string addr)
        {
            try
            {
                Distributor dt = new Distributor
                    {
                        Name = name,
                        Email = email,
                        Phone = phone,
                        Addr = addr
                    };
                this.UnileverEntities.Distributors.Add(dt);
                this.UnileverEntities.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public Product GetProductById(int ID)
        {
            Product p = UnileverEntities.Products.Where(pr => pr.ID == ID).FirstOrDefault();
            return p;
        }
    }
}
