//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.OrderDetails
{
    public class OrderDetailsDataContext : INotifyPropertyChanged
    {
        public OrderDetailsDataContext()
        {
            this.LabelsAndMessagesOrderDetails = new LabelsAndMessagesOrderDetails();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForOrders_ComboItemSource = new ObservableCollection<ModelNotifiedForOrders>(modelNotifiedForOrders);
            modelNotifiedForProducts_ComboItemSource = new ObservableCollection<ModelNotifiedForProducts>(modelNotifiedForProducts);
        }
        
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForOrders> modelNotifiedForOrders {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForOrders> _modelNotifiedForOrders_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForOrders> modelNotifiedForOrders_ComboItemSource
        {
            get
            {
                return _modelNotifiedForOrders_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForOrders_ComboItemSource)
                {
                    _modelNotifiedForOrders_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForOrders_ComboItemSource");
                }
            }
        }
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForProducts> modelNotifiedForProducts {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForProducts> _modelNotifiedForProducts_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForProducts> modelNotifiedForProducts_ComboItemSource
        {
            get
            {
                return _modelNotifiedForProducts_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForProducts_ComboItemSource)
                {
                    _modelNotifiedForProducts_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForProducts_ComboItemSource");
                }
            }
        }
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForOrderDetails modelNotifiedForOrderDetailsMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesOrderDetails LabelsAndMessagesOrderDetails { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForOrderDetails modelNotifiedForOrderDetailsMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

