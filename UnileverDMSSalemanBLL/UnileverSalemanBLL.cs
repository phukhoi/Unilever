using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverDMSSalemanDAL;

namespace UnileverDMSSalemanBLL
{
    public class UnileverSalemanBLL
    {
        public UnileverDMS_SalemansEntities Entities { get; set; }

        public List<Product> GetListProduct()
        {
            return Entities.Products.ToList();
        }
    }
}
