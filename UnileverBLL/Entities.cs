using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverDAL;
using System.Data;

namespace UnileverBLL
{
    public class Entities
    {
        public UnileverEntities UnileverEntities { get; set; }
        public Entities()
        {
            UnileverEntities = new UnileverEntities();
        }
        public List<Product> GetListProducts()
        {
            return UnileverEntities.Products.ToList();
        }
        public List<Category> GetListCategories()
        {
            return UnileverEntities.Categories.ToList();
        }
        public List<Distributor> GetListDistributors()
        {
            return this.UnileverEntities.Distributors.ToList();
        }
    }
}
