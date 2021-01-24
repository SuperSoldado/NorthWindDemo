using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyAppWPF.Containers
{
    /// <summary>
    /// Interaction logic for ContainerWindowMasterDetail.xaml
    /// </summary>
    public partial class ContainerWindowMasterDetail : Window
    {
        public ContainerWindowMasterDetail(Page master, Page detail, string pageTitle)
        {
            InitializeComponent();
            this.Title = pageTitle;            
            FrameMaster.Navigate(master);
            FrameDetail.Navigate(detail);
        }
    }
}
