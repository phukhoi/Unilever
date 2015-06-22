using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnileverObject;

namespace Unilever
{
    public class BLLFactory
    {
        public static IBLL Bus { get; set; }

        public const string UNILEVER_BLL = "UNILEVER_BLL";
        public const string DISTRIBUTOR_BLL = "DISTRIBUTOR_BLL";
        public const string SALE_EMPLOYEE_BLL = "SALE_EMPLOYEE_BLL";

        public static IBLL CreateInstance(string busName)
        {
            if (busName == BLLFactory.UNILEVER_BLL)
            {
                Bus = new UnileverBLL.UnileverBLL();
            }
            if (busName == BLLFactory.DISTRIBUTOR_BLL)
            {
                Bus = new DistributorBLL.DistributorBLL();
            }
            if (busName == BLLFactory.SALE_EMPLOYEE_BLL)
            {
                Bus = new Sale_EmployeeBLL.SaleEmployeeBLL();
            }
            return Bus;
        }
    }
}
