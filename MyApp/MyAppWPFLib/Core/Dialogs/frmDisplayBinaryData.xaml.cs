using Microsoft.Win32;
using MyAppGlobalLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
    public class DisplayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private byte[] _BinData;
        public byte[] BinData
        {
            get { return _BinData; }
            set
            {
                //ItemChanged = true;
                _BinData = value;
                RaiseProperChanged();
            }
        }

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        //public byte[] binData { get; set; }
    }
    /// <summary>
    /// Interaction logic for frmDisplayBinaryDataData.xaml
    /// </summary>
    public partial class frmDisplayBinaryData : Window
    {
        public DisplayViewModel myViewModel { get; set; }

        public frmDisplayBinaryData(byte[] binData, GlobalEnums.MimeTypes mimeType)
        {
            myViewModel = new DisplayViewModel();
            myViewModel.BinData = binData;
            DataContext = myViewModel;

            InitializeComponent();
            switch (mimeType)
            {
                case GlobalEnums.MimeTypes.Image:
                    break;
                case GlobalEnums.MimeTypes.Pdf:
                    MessageBox.Show("Mime type not supported :" + mimeType.ToString());
                    break;
                case GlobalEnums.MimeTypes.Text:
                    MessageBox.Show("Mime type not supported :" + mimeType.ToString());
                    break;
                default:
                    MessageBox.Show("Mime type not supported :" + mimeType.ToString());
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = "c:\\";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFileDialog.FileName, myViewModel.BinData);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";

            if (openFileDialog.ShowDialog() == true)
            {
                myViewModel.BinData = File.ReadAllBytes(openFileDialog.FileName);
            }
        }
    }
}
