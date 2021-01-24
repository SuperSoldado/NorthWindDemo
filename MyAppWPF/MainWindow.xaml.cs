
using MyAppGlobalLib;
using MyAppWPF.Forms;
using MyAppWPF.NorthWind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenMyMVVM(object sender, RoutedEventArgs e)
        {
            MyMVVM win2 = new MyMVVM();
            win2.Show();
        }

        private void OpenTestSplitter(object sender, RoutedEventArgs e)
        {
            //TestSplitter win = new TestSplitter();
            //win.Show();
        }

        private void OpenNorthWind(object sender, RoutedEventArgs e)
        {
            NorthWindMainWindow win = new NorthWindMainWindow();
            win.Show();
        }
    }
}
