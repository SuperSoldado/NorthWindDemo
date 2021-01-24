//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Suppliers
{
    public class SuppliersDataContext : INotifyPropertyChanged
    {
        public SuppliersDataContext()
        {
            this.LabelsAndMessagesSuppliers = new LabelsAndMessagesSuppliers();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForSuppliers modelNotifiedForSuppliersMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesSuppliers LabelsAndMessagesSuppliers { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForSuppliers modelNotifiedForSuppliersMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

