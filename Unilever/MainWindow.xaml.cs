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
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Gauges;
using UnileverBLL;
using UnileverDAL;
using DevExpress.Skins;
using Unilever.Handle;

namespace Unilever
{
    public partial class App : Application
    {
        private void Application_Startup (object sender, StartupEventArgs e)
        {
            DistributorLayout.DistributorWindow dw = new DistributorLayout.DistributorWindow();
            dw.Show();
            //Unilever.MainWindow mw = new MainWindow();
            //mw.Show();

        }
    }
    public partial class MainWindow : DXRibbonWindow
    {
        public UnileverBLL.UnileverBLL UnileverBll { get; set; }

        private void SetGridDataSource(GridControl gc, System.Collections.IList ds)
        {
            gc.ItemsSource = ds;
            gc.Focus();
        }
        private void LoadDistributorToControl()
        {
            List<Distributor> listDistribs = UnileverBll.GetListDistributors();
            SetGridDataSource(this.gridDistributors, listDistribs);
            Handle.Utils.WakeUp(this.distributorsTab);
        }
        private void LoadProductToControl()
        {
            List<Product> listPros = UnileverBll.GetListProducts();
            SetItemSourceProducts(listPros, this.gridProducts);
            Handle.Utils.WakeUp(this.productTab);
        }
        private void ClearTextBox()
        {
            this.txtdbName.Text = "";
            this.txtdbEmail.Text = "";
            this.txtdbPhone.Text = "";
            this.txtdbAddr.Text = "";

            // textbox product
            this.txtpName.Text = "";
            this.txtpDescript.Text = "";
            this.txtpRemain.Text = "";
            this.txtpPrice.Text = "";
        }
        private void CRUD_Product(int? proId, UnileverObject.BLL.CRUDOPTION option)
        {
            Product p = new Product();

            string name = this.txtpName.Text;
            string price = this.txtpPrice.Text;
            string remain = this.txtpRemain.Text;
            string descript = this.txtpDescript.Text;
            string catid = null;
            try
            {
                catid = ((Category)this.cbxpCategory.SelectedItemValue).ID.ToString();
            }
            catch (Exception)
            {
                UnileverError.Show("Choose a category for this product",
                    UnileverError.WARN_CAPTION, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            string importdate = this.depImportDate.EditValue.ToString();

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(price) ||
                string.IsNullOrEmpty(remain) ||
                string.IsNullOrEmpty(descript) ||
                string.IsNullOrEmpty(catid) ||
                string.IsNullOrEmpty(importdate))
            {
                UnileverError.Show("Missing field !",
                    UnileverError.WARN_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Warning
                    );
            }
            else
            {
                try
                {
                    if (this.UnileverBll.SaveChangesProduct(proId.Value, name, catid, price, importdate, remain, descript, option))
                    {
                        LoadProductToControl();
                    }
                }
                catch (Exception) // lưu dữ liệu bị lỗi
                {
                    UnileverError.Show("Something fail, try again!",
                        UnileverError.ERR_CAPTION,
                        System.Windows.Forms.MessageBoxIcon.Warning
                        );
                }
            }
        }
        private void CRUD_Distributor(int? distribId, UnileverObject.BLL.CRUDOPTION option)
        {
            string name = this.txtdbName.Text;
            string email = this.txtdbEmail.Text;
            string phone = this.txtdbPhone.Text;
            string addr = this.txtdbAddr.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(addr))
            {
                Handle.UnileverError.Show("Missing field.",
                    Handle.UnileverError.WARN_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (this.UnileverBll.SaveChangesDistributor(distribId.Value, name, email, phone, addr, option))
                    {
                        LoadDistributorToControl();
                    }
                    ClearTextBox();
                }
                catch (Exception) // có lỗi trong quá trình lưu vào db
                {
                    UnileverError.Show(UnileverError.SAVE_DB_ERRORMSG, UnileverError.WARN_CAPTION);
                }
            }
        }
        private void SetItemSourceProducts(System.Collections.IList list, GridControl gc)
        {
            gc.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew;
            gc.ItemsSource = list;
            gc.Columns.Remove(gridProducts.Columns[gridProducts.Columns.Count - 1]);
        }
        private void LoadDefferredLiabilities()
        {
            try
            {
                List<sp_getDistributorLiabilitiesSumary_Result> list = this.UnileverBll.GetListDefferredLiabilities();
                this.gridLiabities.ItemsSource = list;
            }
            catch (Exception)
            {

                UnileverError.Show("Something fail when getting data from db, try again",
                    UnileverError.WARN_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Asterisk);
            }
        }

        private void LoadSaleRevenues()
        {
            List<SaleRevenue> list = this.UnileverBll.GetListSaleRevenues();
            this.gridSaleRevenues.ItemsSource = list;
        }
        private void InitGridFindPanel()
        {
            this.gridCategories.View.ShowSearchPanel(true);
            this.gridDistributors.View.ShowSearchPanel(true);
            this.gridLiabities.View.ShowSearchPanel(true);
            this.gridProducts.View.ShowSearchPanel(true);
            this.gridSaleRevenues.View.ShowSearchPanel(true);
        }
        /* ----------------------------------------------------------------------------------- */



        public MainWindow()
        {
            try
            {
                InitializeComponent();
                SkinManager.EnableFormSkins();
                UnileverBll = (UnileverBLL.UnileverBLL)BLLFactory.CreateInstance(BLLFactory.UNILEVER_BLL);
                if (UnileverBll == null)
                {
                    MessageBox.Show("Missing Bus");
                    Application.Current.Shutdown();
                }
            }
            catch (Exception)
            {
                Unilever.Handle.UnileverError.Show("Something fail, try again.!", Unilever.Handle.UnileverError.ERR_CAPTION);
            }
        }
        private void DXRibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            InitGridFindPanel();

            try
            {
                List<Product> listPros = UnileverBll.GetListProducts();
                SetItemSourceProducts(listPros, this.gridProducts);
            }
            catch (Exception)
            {
                Unilever.Handle.UnileverError.Show(Unilever.Handle.UnileverError.CONNECT_DB_ERRORMSG, Unilever.Handle.UnileverError.ERR_CAPTION);
            }
        }


        private void btnViewPros_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // view products list

            try
            {
                LoadProductToControl();
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERRORMSG, UnileverError.ERR_CAPTION);
            }
        }
        private void btnViewCats_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // view categories list
            try
            {
                List<Category> listCates = UnileverBll.GetListCategories();
                SetGridDataSource(this.gridCategories, listCates);
                Handle.Utils.WakeUp(categoriesTab);
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERRORMSG, UnileverError.ERR_CAPTION);
            }
        }
        private void btnViewDistributor_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                LoadDistributorToControl();
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERRORMSG, UnileverError.ERR_CAPTION);
            }
        }
        private void btnAddDistribubor_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Utils.WakeUp(this.manageTab);
        }
        private void btndbAdd_Click_1(object sender, RoutedEventArgs e)
        {
            CRUD_Distributor(0, UnileverObject.BLL.CRUDOPTION.CREATE);
        }
        private void tblProducts_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                int ID = int.Parse(gridProducts.GetFocusedRowCellValue("ID").ToString());
                Utils.TempProid = ID;
                Product p = UnileverBll.GetProductById(ID);
                this.txtpName.Text = p.Name;
                this.txtpPrice.Text = p.Price.ToString();
                List<Category> listCates = UnileverBll.GetListCategories();
                this.cbxpCategory.ItemsSource = listCates;
                this.cbxpCategory.ValueMember = "ID";
                this.cbxpCategory.DisplayMember = "Name";
                this.cbxpCategory.SelectedItem = p.Category;
                this.txtpRemain.Text = p.RemainingAmount.ToString();
                this.depImportDate.EditValue = System.DateTime.Today;
                this.txtpDescript.Text = p.Descript;
            }
            catch (Exception)
            {
                // HACK
            }
        }
        private void btnUpdatePros_Click_1(object sender, RoutedEventArgs e)
        {
            CRUD_Product((int)Utils.TempProid, UnileverObject.BLL.CRUDOPTION.UPDATE);
        }
        private void btnAddPros_Click_1(object sender, RoutedEventArgs e)
        {
            CRUD_Product(0, UnileverObject.BLL.CRUDOPTION.CREATE);
        }
        private void btnClearText_Click_1(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }
        private void btnRemovePros_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult dr = DevExpress.XtraEditors.XtraMessageBox.Show(
                "Are your sure?", "Confirm",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Asterisk,
                DevExpress.Utils.DefaultBoolean.True);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                CRUD_Product((int)Utils.TempProid, UnileverObject.BLL.CRUDOPTION.DELETE);
            }
        }
        private void btndbUpdate_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CRUD_Distributor((int)Utils.TempDistribId, UnileverObject.BLL.CRUDOPTION.UPDATE);
            }
            catch (Exception) // some thing fail when update
            {
                UnileverError.Show("Something fail, try again.", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void btndbRemove_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CRUD_Distributor((int)Utils.TempDistribId, UnileverObject.BLL.CRUDOPTION.DELETE);
            }
            catch (Exception) // some thing fail when remove
            {
                UnileverError.Show("Something fail, try again.", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        private void tblDistributors_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                int ID = int.Parse(gridDistributors.GetFocusedRowCellValue("ID").ToString());
                Utils.TempDistribId = ID;
                this.txtdbId.Text = Utils.TempDistribId.ToString();
                this.txtdbName.Text = gridDistributors.GetFocusedRowCellValue("Name").ToString();
                this.txtdbEmail.Text = gridDistributors.GetFocusedRowCellValue("Email").ToString();
                this.txtdbAddr.Text = gridDistributors.GetFocusedRowCellValue("Addr").ToString();
                this.txtdbPhone.Text = gridDistributors.GetFocusedRowCellValue("Phone").ToString();
                Utils.WakeUp(this.manageTab);
            }
            catch (Exception)
            {
                // HACK
            }
        }

        private void btnManageLiability_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                LoadDefferredLiabilities();
                Utils.WakeUp(this.liabilitiesTab);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void btndlUpdate_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DefferredLiability dl = this.UnileverBll.GetDefferredLiabilityById((int)Utils.TempLiabilitiyId);
                string resday = this.txtdlresday.Text;
                if (string.IsNullOrEmpty(resday))
                {
                    UnileverError.Show("Missing field", UnileverError.WARN_CAPTION,
                        System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                this.UnileverBll.UpdateDefferredLiability((int)Utils.TempLiabilitiyId, dl.DebtDate.ToString(), resday);
                LoadDefferredLiabilities();
            }
            catch (NullReferenceException)
            {
                UnileverError.Show("Missing defferred liability Id", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                UnileverError.Show("Something fail when proceed database", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        private void btndlRemove_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult dr = DevExpress.XtraEditors.XtraMessageBox.Show(
                "Are your sure?", "Confirm",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Asterisk,
                DevExpress.Utils.DefaultBoolean.True);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    DefferredLiability dl = this.UnileverBll.GetDefferredLiabilityById((int)Utils.TempLiabilitiyId);
                    this.UnileverBll.RemoveDefferredLiability((int)Utils.TempLiabilitiyId);
                    LoadDefferredLiabilities();
                }
            }
            catch (NullReferenceException)
            {
                UnileverError.Show("Missing defferred liability Id", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                UnileverError.Show("Something fail when proceed database", UnileverError.ERR_CAPTION,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void tblLiabilities_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                Utils.WakeUp(this.manageTab);
                this.liabilityTab.IsActive = true;
                sp_getDistributorLiabilitiesSumary_Result sp = e.NewRow as sp_getDistributorLiabilitiesSumary_Result;
                if (sp != null)
                {
                    DefferredLiability dl = this.UnileverBll.GetDefferredLiabilityByOrderId(sp.OrderId.Value);
                    Utils.TempLiabilitiyId = dl.ID;
                    Distributor db = this.UnileverBll.GetDistributorById(sp.DistributorId.Value);
                    this.txtdldbid.Text = sp.DistributorId.Value.ToString();
                    this.txtdlid.Text = dl.ID.ToString();
                    this.txtdlorderid.Text = sp.OrderId.Value.ToString();
                    this.txtdldbname.Text = db.Name;
                }
            }
            catch (Exception)
            {
                // HACK
            }
        }

        private void btnSaleRevenue_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            LoadSaleRevenues();
            Utils.WakeUp(this.salerevenueTab);
        }

        private void tblSaleRevenues_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                SaleRevenue sr = e.NewRow as SaleRevenue;
                if (sr != null)
                {
                    List<sp_getSaleRevenueSumaryByDistribId_Result> list =
                        this.UnileverBll.GetListSaleRevenueByDistributorId(sr.DistributorID);
                    this.gridSaleRevenueDetails.ItemsSource = list;

                    var srSumaryReduce = list.Select(s => new
                    {
                        Sold = list.Sum(srsr => srsr.SoldQuantity),
                        TotalCash = list.Sum(selr => selr.TotalCash)
                    }).Distinct();
                    //from srsr in list
                    //                     select new
                    //                     {
                    //                        Sold = list.Sum(s => s.SoldQuantity),
                    //                        TotalCash = list.Sum(s => s.TotalCash)
                    //                     };
                    this.gridSaleRevenueReduce.ItemsSource = srSumaryReduce;
                    Utils.WakeUp(dpSaleRevenues);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnManageInventory_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<Inventory> list = this.UnileverBll.GetListInventories();
                this.gridInventories.ItemsSource = list;
                Utils.WakeUp(inventoriesTab);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddOrder_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }
    }
}
