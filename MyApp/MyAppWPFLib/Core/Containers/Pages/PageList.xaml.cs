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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAppWPFLib
{
    public class MyItem
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Interaction logic for PageList.xaml
    /// </summary>
    public partial class PageList : Page
    {
        public string listContent;
        public PageList(string listContent)
        {
            InitializeComponent();
            this.listContent = listContent;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            int numberOfItens = int.Parse(txtNumberOfItens.Text);
            List<MyItem> list = new List<MyItem>();

            for (int i = 0; i < numberOfItens; i++)
            {
                MyItem item = new MyItem();
                item.Name = listContent + "item " + i;
                list.Add(item);
            }

            MyList.ItemsSource = list;
        }
    }
}
