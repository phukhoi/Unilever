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
using Unilever.Error;

namespace Unilever
{
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                SkinManager.EnableFormSkins();
            }
            catch (Exception)
            {
                Unilever.Error.UnileverError.Show("Something fail, try again.!", Unilever.Error.UnileverError.CAPTION_ERROR);
            }
        }
        private void SetGridDataSource(GridControl gc, System.Collections.IList ds, DocumentPanel ownedTab)
        {
            gc.ItemsSource = ds;
            if (ownedTab.Visibility == System.Windows.Visibility.Hidden || ownedTab.IsClosed)
            {
                ownedTab.Visibility = System.Windows.Visibility.Visible;
                ownedTab.Closed = false;
            }
            ownedTab.Focus();
            gc.Focus();
        }
        private void DXRibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Entities ctx = new Entities();
                List<Product> listPros = ctx.GetListProducts();
                this.gridProducts.ItemsSource = listPros;
            }
            catch (Exception)
            {
                Unilever.Error.UnileverError.Show(Unilever.Error.UnileverError.CONNECT_DB_ERR, Unilever.Error.UnileverError.CAPTION_ERROR);
            }
        }
        private void btnViewPros_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // view products list
            
            try
            {
                UnileverEntities ctx = new UnileverEntities();
                List<Product> listPros = ctx.Products.ToList();
                SetGridDataSource(this.gridProducts, listPros, this.productTab);
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERROR);
            }
        }
        private void btnViewCats_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // view categories list
            try
            {
                UnileverEntities ctx = new UnileverEntities();
                List<Category> listCates = ctx.Categories.ToList();
                SetGridDataSource(this.gridCategories, listCates,this.categoriesTab);
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERROR);
            }
        }
        private void btnViewDistributor_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                Entities ctx = new Entities();
                List<Distributor> listDistribs = ctx.GetListDistributors();
                SetGridDataSource(this.gridDistributors, listDistribs, this.distributorsTab);
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERROR);
            }
        }

        private void btnAddDistribubor_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }
    }
}
