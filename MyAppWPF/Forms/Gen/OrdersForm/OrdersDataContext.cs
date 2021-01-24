//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Orders
{
    public class OrdersDataContext : INotifyPropertyChanged
    {
        public OrdersDataContext()
        {
            this.LabelsAndMessagesOrders = new LabelsAndMessagesOrders();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForCustomers_ComboItemSource = new ObservableCollection<ModelNotifiedForCustomers>(modelNotifiedForCustomers);
            modelNotifiedForEmployees_ComboItemSource = new ObservableCollection<ModelNotifiedForEmployees>(modelNotifiedForEmployees);
            modelNotifiedForShippers_ComboItemSource = new ObservableCollection<ModelNotifiedForShippers>(modelNotifiedForShippers);
        }
        
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForCustomers> modelNotifiedForCustomers {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForCustomers> _modelNotifiedForCustomers_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForCustomers> modelNotifiedForCustomers_ComboItemSource
        {
            get
            {
                return _modelNotifiedForCustomers_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForCustomers_ComboItemSource)
                {
                    _modelNotifiedForCustomers_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForCustomers_ComboItemSource");
                }
            }
        }
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForEmployees> modelNotifiedForEmployees {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForEmployees> _modelNotifiedForEmployees_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForEmployees> modelNotifiedForEmployees_ComboItemSource
        {
            get
            {
                return _modelNotifiedForEmployees_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForEmployees_ComboItemSource)
                {
                    _modelNotifiedForEmployees_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForEmployees_ComboItemSource");
                }
            }
        }
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForShippers> modelNotifiedForShippers {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForShippers> _modelNotifiedForShippers_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForShippers> modelNotifiedForShippers_ComboItemSource
        {
            get
            {
                return _modelNotifiedForShippers_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForShippers_ComboItemSource)
                {
                    _modelNotifiedForShippers_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForShippers_ComboItemSource");
                }
            }
        }
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForOrders modelNotifiedForOrdersMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesOrders LabelsAndMessagesOrders { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForOrders modelNotifiedForOrdersMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

