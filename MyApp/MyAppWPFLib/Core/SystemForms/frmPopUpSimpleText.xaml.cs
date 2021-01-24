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

namespace MyAppWPFLib.Core.SystemForms
{
    /// <summary>
    /// Example: MyStringToEdit = frmPopUpSimpleText.Open(MyStringToEdit);
    /// </summary>
    public partial class frmPopUpSimpleText : Window
    {
        public frmPopUpSimpleText(string msg)
        {
            InitializeComponent();
            FormResult = msg;
            txtText.Text = msg;
        }

        private void btnSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            FormResult = txtText.Text;
            this.Close();
        }

        public string FormResult = "";

        public static string Open(string msg)
        {
            frmPopUpSimpleText dialogAc = new frmPopUpSimpleText(msg);
            //this line will run through only when dialogAc is closed
            dialogAc.ShowDialog();            
            return dialogAc.FormResult;
        }

      
    }
}
