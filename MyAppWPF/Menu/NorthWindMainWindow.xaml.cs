using MyApp.WPFForms;
using MyApp.WPFList;
using MyAppGlobalLib;
using MyAppWPF.Containers;
using MyAppWPF.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyAppWPFLib;

namespace MyAppWPF.NorthWind
{
    /// <summary>
    /// Interaction logic for NorthWindMenu.xaml
    /// </summary>
    public partial class NorthWindMainWindow : Window
    {
        public NorthWindMainWindow()
        {
            InitializeComponent();
            configFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\GlobalConfiguration.json";
            globalConfigReader = new GlobalConfigReader();
            config = globalConfigReader.Load(configFile);
        }
        public string configFile { get; set; }
        public GlobalConfigReader globalConfigReader { get; set; }
        public GlobalConfiguration config { get; set; }

        private void NavigateMasterDetail(Page master, Page detail)
        {
            PageContainerMasterDetailSplitter contentPage = new PageContainerMasterDetailSplitter();
            contentPage.FrameMaster.Navigate(master);
            contentPage.FrameDetail.Navigate(detail);
            FramePlaceHolder.Navigate(contentPage);
        }

        private void NavigateMasterDetailDetail(Page master, Page detailFromMaster, Page detailFromDetail)
        {
            /* this works as well. But i'm using the 2 splitter page to avoid duplication
            PageContainerMasterDetailDetailSplitter pageContainerMasterDetailDetailSplitter = new PageContainerMasterDetailDetailSplitter();
            pageContainerMasterDetailDetailSplitter.FrameMaster.Navigate(master);
            pageContainerMasterDetailDetailSplitter.FrameDetail.Navigate(detailFromMaster);
            pageContainerMasterDetailDetailSplitter.FrameDetailDetail.Navigate(detailFromDetail);
            FramePlaceHolder.Navigate(pageContainerMasterDetailDetailSplitter);*/

            
            PageContainerMasterDetailSplitter contentPageMasterDetailA = new PageContainerMasterDetailSplitter();
            contentPageMasterDetailA.FrameMaster.Navigate(master);

            PageContainerMasterDetailSplitter contentPageMasterDetailB = new PageContainerMasterDetailSplitter();
            contentPageMasterDetailB.FrameMaster.Navigate(detailFromMaster);
            contentPageMasterDetailB.FrameDetail.Navigate(detailFromDetail);

            contentPageMasterDetailA.FrameDetail.Navigate(contentPageMasterDetailB);

            FramePlaceHolder.Navigate(contentPageMasterDetailA);
        }

        private void NavigateSimplePage(Page contentPage)
        {
            PageSimpleContainer pageSimpleContainer = new PageSimpleContainer();
            pageSimpleContainer.FrameSimpleContent.Navigate(contentPage);
            FramePlaceHolder.Navigate(pageSimpleContainer);
            /*grdGridContent.RowDefinitions[0].Height = new GridLength(200, GridUnitType.Star);
            grdGridContent.RowDefinitions[1].Height = new GridLength(0);
            grdSplitter.Visibility = Visibility.Hidden;
            FrameDetail.Content = null;*/
        }

        private void btnListRegion_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Region.ListWPFRegion contentPage = new MyApp.WPFList.Region.ListWPFRegion(config.WPFConfig, true, this.FramePlaceHolder);
            contentPage.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(contentPage);
        }

        private void btnTerritoriesList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Territories.ListWPFTerritories page = new MyApp.WPFList.Territories.ListWPFTerritories(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());            
            NavigateSimplePage(page);
        }

        private void btnRegionTerritories_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Region.ListWPFRegion master = new MyApp.WPFList.Region.ListWPFRegion(config.WPFConfig, true, this.FramePlaceHolder);
            MyApp.WPFList.Territories.ListWPFTerritories detail = new MyApp.WPFList.Territories.ListWPFTerritories(config.WPFConfig, true, this.FramePlaceHolder);

            //detail.Setup_FilterGroupBoxIsVisible(false);
            master.DetailListTerritories = detail;

            master.Setup_SetLanguage(GetLanguage());
            detail.Setup_SetLanguage(GetLanguage());

            NavigateMasterDetail(master, detail);

            /*FrameMaster.Navigate(master);
            FrameDetail.Navigate(detail);*/

        }

        public string GetLanguage()
        {
            //return "de-DE";
            return config.WPFConfig.AppLanguage;
        }

        private void btnEmployeeTerritoriesList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories page = new MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
            /*ClearFrames();
            MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories master = new MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories(config.WPFConfig, true);
            FrameMaster.Navigate(master);*/
        }

        private void ClearFrames()
        {
            /*FrameMaster.Content = null;
            FrameMaster.NavigationService.RemoveBackEntry();
            FrameDetail.Content = null;
            FrameDetail.NavigationService.RemoveBackEntry();*/
        }

        private void btnTerritoriesEmployeesRegions_Click(object sender, RoutedEventArgs e)
        {
            /*ClearFrames();
            MyApp.WPFList.Region.ListWPFRegion master = new MyApp.WPFList.Region.ListWPFRegion(config.WPFConfig, true);
            Container2Tabs detail = new Container2Tabs();
            FrameMaster.Navigate(master);
            FrameDetail.Navigate(detail);*/
        }

        private void btnListSplited_Click(object sender, RoutedEventArgs e)
        {
            PageList master = new PageList("master");
            PageList detail = new PageList("detail");
            NavigateMasterDetail(master, detail);
        }

        private void btnEmployeesList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Employees.ListWPFEmployees contentPage = new MyApp.WPFList.Employees.ListWPFEmployees(config.WPFConfig, true, this.FramePlaceHolder);
            contentPage.Setup_SetColumnsMaxLenght(100);
            contentPage.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(contentPage);
        }

        private void btnOrdersList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Orders.ListWPFOrders contentPage = new MyApp.WPFList.Orders.ListWPFOrders(config.WPFConfig, true, this.FramePlaceHolder);
            contentPage.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(contentPage);
        }

        private void btnShippersList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Shippers.ListWPFShippers contentPage = new MyApp.WPFList.Shippers.ListWPFShippers(config.WPFConfig, true, this.FramePlaceHolder);
            contentPage.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(contentPage);
        }

        private void btnCategoriesList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Categories.ListWPFCategories contentPage = new MyApp.WPFList.Categories.ListWPFCategories(config.WPFConfig, true, this.FramePlaceHolder);
            contentPage.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(contentPage);
        }

        private void btnOrdersDetailsList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.OrderDetails.ListWPFOrderDetails page = new MyApp.WPFList.OrderDetails.ListWPFOrderDetails(config.WPFConfig, true, this.FramePlaceHolder);            
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }


        private void btnProductsList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Products.ListWPFProducts page = new MyApp.WPFList.Products.ListWPFProducts(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }


        private void btnSuppliersList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Suppliers.ListWPFSuppliers page = new MyApp.WPFList.Suppliers.ListWPFSuppliers(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }

        private void btnCustomerDemographics_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.CustomerDemographics.ListWPFCustomerDemographics page = new MyApp.WPFList.CustomerDemographics.ListWPFCustomerDemographics(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }

        private void btnCustomerCustomerDemoList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.CustomerCustomerDemo.ListWPFCustomerCustomerDemo page = new MyApp.WPFList.CustomerCustomerDemo.ListWPFCustomerCustomerDemo(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }

        private void btnCustomerList_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Customers.ListWPFCustomers page = new MyApp.WPFList.Customers.ListWPFCustomers(config.WPFConfig, true, this.FramePlaceHolder);
            page.Setup_SetLanguage(GetLanguage());
            NavigateSimplePage(page);
        }

        private void btnMasterEmployees_DetailTerritories_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Employees.ListWPFEmployees master = new MyApp.WPFList.Employees.ListWPFEmployees(config.WPFConfig, true, this.FramePlaceHolder);
            MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories detail = new MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories(config.WPFConfig, true, this.FramePlaceHolder);
            MyApp.WPFList.Territories.ListWPFTerritories detailDetail = new MyApp.WPFList.Territories.ListWPFTerritories(config.WPFConfig, true, this.FramePlaceHolder);
            detail.Setup_FilterGroupBoxIsVisible(false);
            detailDetail.Setup_FilterGroupBoxIsVisible(false);
            master.DetailListEmployeeTerritories = detail;
            detail.DetailListTerritories = detailDetail;

            master.Setup_SetLanguage(GetLanguage());
            detail.Setup_SetLanguage(GetLanguage());

            NavigateMasterDetailDetail(master, detail, detailDetail);
        }

        private void btnMasterEmployees_DetailOrders_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Employees.ListWPFEmployees master = new MyApp.WPFList.Employees.ListWPFEmployees(config.WPFConfig, true, this.FramePlaceHolder);
            master.Setup_SetColumnsMaxLenght(100);
            master.Setup_SetLanguage(GetLanguage());
            MyApp.WPFList.Orders.ListWPFOrders detail = new MyApp.WPFList.Orders.ListWPFOrders(config.WPFConfig, true, this.FramePlaceHolder);
            master.DetailListOrders = detail;
            detail.Setup_SetLanguage(GetLanguage());
            NavigateMasterDetail(master, detail);
        }

        private void btnMasterOrderss_DetailOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Orders.ListWPFOrders master = new MyApp.WPFList.Orders.ListWPFOrders(config.WPFConfig, true, this.FramePlaceHolder);
            MyApp.WPFList.OrderDetails.ListWPFOrderDetails detail = new MyApp.WPFList.OrderDetails.ListWPFOrderDetails(config.WPFConfig, true, this.FramePlaceHolder);
            
            master.Setup_SetLanguage(GetLanguage());
            detail.Setup_SetLanguage(GetLanguage());

            master.DetailListOrderDetails = detail;
            NavigateMasterDetail(master, detail);
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            frmConfig page = new frmConfig(config);
            NavigateSimplePage(page);
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            MyAppWPF.Core.MyTestWindow w = new MyAppWPF.Core.MyTestWindow();
            w.Show();
        }
    }
}
