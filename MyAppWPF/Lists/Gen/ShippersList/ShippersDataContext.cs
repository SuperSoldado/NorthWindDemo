//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFList.Shippers
{
    public class ShippersDataContext : INotifyPropertyChanged
    {
        public ShippersDataContext()
        {
            this.WPFMessageAndLabelForList = new WPFMessageAndLabelForList();
        }

        public WPFMessageAndLabelForList WPFMessageAndLabelForList { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        
        
        //old Remove later. DataContext.html holds the code. This is the original.// public ObservableCollection<ModelNotifiedForShippers> modelNotifiedForShippers { get; set; }

        /// <summary>
        /// Defines the main class holding Grid's data. This data is filtered/copied to "GridData" and binded to grid.
        /// </summary>
        public List<ModelNotifiedForShippers> modelNotifiedForShippersMain { get; set; }

        /// <summary>
        /// Holds the data binded to Grid. 
        /// </summary>
        public ObservableCollection<ModelNotifiedForShippers> GridData { get; set; }

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

