using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using UnileverDMSSalemanDAL;
using Sale_EmployeeBLL;
using UnileverDMSSalemanDAL;

namespace Unilever.DistributorLayout
{
    /// <summary>
    /// Interaction logic for DistributorWindow.xaml
    /// </summary>
    public partial class DistributorWindow : DXRibbonWindow
    {
        /* field - property */
        public SaleEmployeeBLL SalemanBLL { get; set; }
        /* --/ */
        public DistributorWindow()
        {
            InitializeComponent();
            SalemanBLL = (SaleEmployeeBLL)BLLFactory.CreateInstance(BLLFactory.SALE_EMPLOYEE_BLL);
        }

        private void btnViewPros_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (this.SalemanBLL.IsDbConnected())
            {
                this.gridSalemans.ItemsSource = this.SalemanBLL.GetListProduct();
            }
        }

        private void btnViewCats_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnAddOrder_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnAddSalemans_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnSaleRevenue_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnManageInventory_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnViewSalemans_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnManageLiability_ItemClick_1(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void tblSalemans_FocusedRowChanged_1(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

        }
    }
}
