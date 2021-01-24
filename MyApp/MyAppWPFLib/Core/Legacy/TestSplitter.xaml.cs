using MyApp.WPFForms;
using MyApp.WPFList;
using MyAppGlobalLib;
using MyAppWPF.Containers;
using System;
using System.Collections.Generic;
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
using WpfApp1.Pages;

namespace MyAppWPF.NorthWind
{
    /// <summary>
    /// Interaction logic for NorthWindMenu.xaml
    /// </summary>
    public partial class TestSplitter : Window
    {
        public TestSplitter()
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
            FrameMaster.Navigate(master);
            FrameDetail.Navigate(detail);

            grdGridContent.RowDefinitions[0].Height = new GridLength(200);
            grdGridContent.RowDefinitions[1].Height = new GridLength(200, GridUnitType.Star);

            grdSplitter.Visibility = Visibility.Visible;
        }

        private void NavigateSimplePage(Page contentPage)
        {
            FrameMaster.Navigate(contentPage);
            grdGridContent.RowDefinitions[0].Height = new GridLength(200, GridUnitType.Star);
            grdGridContent.RowDefinitions[1].Height = new GridLength(0);
            grdSplitter.Visibility = Visibility.Hidden;
            FrameDetail.Content = null;
        }

        private void btnRegion_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Region.ListWPFRegion contentPage = new MyApp.WPFList.Region.ListWPFRegion(config.WPFConfig, true);
            NavigateSimplePage(contentPage);
        }

        private void btnTerritories_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Territories.ListWPFTerritories contentPage = new MyApp.WPFList.Territories.ListWPFTerritories(config.WPFConfig, true);
            NavigateSimplePage(contentPage);
        }

        private void btnRegionTerritories_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.Region.ListWPFRegion master = new MyApp.WPFList.Region.ListWPFRegion(config.WPFConfig, true);
            MyApp.WPFList.Territories.ListWPFTerritories detail = new MyApp.WPFList.Territories.ListWPFTerritories(config.WPFConfig, false);

            //detail.Setup_FilterGroupBoxIsVisible(false);
            master.DetailListTerritories = detail;
            NavigateMasterDetail(master, detail);

            /*FrameMaster.Navigate(master);
            FrameDetail.Navigate(detail);*/

        }

        private void btnEmployeeTerritories_Click(object sender, RoutedEventArgs e)
        {
            MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories page = new MyApp.WPFList.EmployeeTerritories.ListWPFEmployeeTerritories(config.WPFConfig, true);
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

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            PageList master = new PageList("master");
            PageList detail = new PageList("detail");
            NavigateMasterDetail(master, detail);
        }
    }
}
