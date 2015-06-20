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
using UnileverDAL;
using DevExpress.Skins;
using Unilever.Error;

namespace Unilever
{
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DXRibbonWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                SkinManager.EnableFormSkins();
                UnileverEntities ctx = new UnileverEntities();
                List<Product> listPros = ctx.Products.ToList();
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
                this.gridProducts.ItemsSource = listPros;
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
                this.gridProducts.ItemsSource = listCates;
                if (this.categoriesTab.Visibility == System.Windows.Visibility.Hidden)
                {
                    this.categoriesTab.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception)
            {
                UnileverError.Show(UnileverError.CONNECT_DB_ERR, UnileverError.CAPTION_ERROR);
            }
        }
    }
}
