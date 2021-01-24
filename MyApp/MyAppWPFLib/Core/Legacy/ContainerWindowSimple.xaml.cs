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
    /// Interaction logic for ContainerWindowSimple.xaml
    /// </summary>
    public partial class ContainerWindowSimple : Window
    {
        public ContainerWindowSimple(Page destinationPage, string pageTitle)
        {
            InitializeComponent();
            this.Title = pageTitle;            
            FrameSimpleContent.Navigate(destinationPage);
        }
    }
}
