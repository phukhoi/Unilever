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
                    UnileverError.CAPTION_WARN, System.Windows.Forms.MessageBoxIcon.Warning);
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
                UnileverError.Show("Missing fill !", 
                    UnileverError.CAPTION_WARN, 
                    System.Windows.Forms.MessageBoxIcon.Warning
                    );
            }
            else
            {
                try
                {
                    if (this.UnileverBll.SaveChanges(proId.Value, name, catid, price, importdate, remain, descript, option))
                    {
                        LoadProductToControl();
                    }
                }
                catch (Exception) // lưu dữ liệu bị lỗi
                {
                    UnileverError.Show("Something fail, try again!", 
                        UnileverError.CAPTION_ERR, 
                        System.Windows.Forms.MessageBoxIcon.Warning
                        );
                }
            }
        }
        private void SetItemSourceProducts(System.Collections.IList list, GridControl gc)
        {
            gc.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew;
            gc.ItemsSource = list;
            gc.Columns.Remove(gridProducts.Columns[gridProducts.Columns.Count - 1]);
        }
        /**/

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
                Unilever.Handle.UnileverError.Show("Something fail, try again.!", Unilever.Handle.UnileverError.CAPTION_ERR);
            }
        }      
        private void DXRibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Product> listPros = UnileverBll.GetListProducts();
                SetItemSourceProducts(listPros, this.gridProducts);
            }
            catch (Exception)
            {
                Unilever.Handle.UnileverError.Show(Unilever.Handle.UnileverError.CONNECT_DB_ERR, Unilever.Handle.UnileverError.CAPTION_ERR);
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
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERR);
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
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERR);
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
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERR);
            }
        }
        private void btnAddDistribubor_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            Utils.WakeUp(this.addTab);
            addTab.IsActive = true;
        }
        private void btndbAdd_Click_1(object sender, RoutedEventArgs e)
        {
            string name = this.txtdbName.Text;
            string email = this.txtdbEmail.Text;
            string phone = this.txtdbPhone.Text;
            string addr = this.txtdbAddr.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(addr))
            {
                Handle.UnileverError.Show("Missing field.",
                    Handle.UnileverError.CAPTION_WARN,
                    System.Windows.Forms.MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    this.UnileverBll.AddDistributor(name, email, phone, addr);
                    LoadDistributorToControl();
                    ClearTextBox();
                }
                catch (Exception)
                {
                    UnileverError.Show(UnileverError.SAVE_DB_ERR, UnileverError.CAPTION_WARN);
                }
            }
        }
        private void tblProducts_FocusedRowChanged_1(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                int ID = int.Parse(gridProducts.GetFocusedRowCellValue("ID").ToString());
                Utils.Temp = ID;
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
                UnileverError.Show("Something fail", "Error");
            }
        }
        private void btnUpdatePros_Click_1(object sender, RoutedEventArgs e)
        {
            CRUD_Product((int)Utils.Temp, UnileverObject.BLL.CRUDOPTION.UPDATE);
        }  
        private void btnAddPros_Click_1(object sender, RoutedEventArgs e)
        {
            CRUD_Product(null, UnileverObject.BLL.CRUDOPTION.CREATE);
        }
        private void btnClearText_Click_1(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }
    }
}
 