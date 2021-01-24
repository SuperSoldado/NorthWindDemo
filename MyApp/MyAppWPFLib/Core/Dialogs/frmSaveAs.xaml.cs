using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyAppWPFLib
{
    /// <summary>
    /// Interaction logic for frmSaveAs.xaml
    /// </summary>
    public partial class frmSaveAs : Window
    {
        public frmSaveAs(string sugestedFileName, byte[] binData)
        {
            InitializeComponent();
            this.binData = binData;
            this.txtFileName.Text = sugestedFileName;
        }

        private byte[] binData { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllBytes(txtFileName.Text, binData);
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
