using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverDMSSalemanDAL;
using UnileverObject;

namespace Sale_EmployeeBLL
{
    public class SaleEmployeeBLL : BLL, UnileverObject.IBLL
    {
        public UnileverDMS_SalemansEntities Entities { get; set; }

        public bool IsDbConnected()
        {
            DbConnection conn = this.Entities.Database.Connection;
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public SaleEmployeeBLL()
        {
            this.Entities = new UnileverDMS_SalemansEntities();
        }
        public List<Product> GetListProduct()
        {
            return this.Entities.Products.ToList();
        }
    }

}
