//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFList.Products
{
    public class ProductsDataContext : INotifyPropertyChanged
    {
        public ProductsDataContext()
        {
            this.WPFMessageAndLabelForList = new WPFMessageAndLabelForList();
        }

        public WPFMessageAndLabelForList WPFMessageAndLabelForList { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForSuppliers_ComboItemSource = new ObservableCollection<ModelNotifiedForSuppliers>(modelNotifiedForSuppliers);
            modelNotifiedForCategories_ComboItemSource = new ObservableCollection<ModelNotifiedForCategories>(modelNotifiedForCategories);
        }
        
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForSuppliers> modelNotifiedForSuppliers {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForSuppliers> _modelNotifiedForSuppliers_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForSuppliers> modelNotifiedForSuppliers_ComboItemSource
        {
            get
            {
                return _modelNotifiedForSuppliers_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForSuppliers_ComboItemSource)
                {
                    _modelNotifiedForSuppliers_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForSuppliers_ComboItemSource");
                }
            }
        }
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForCategories> modelNotifiedForCategories {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForCategories> _modelNotifiedForCategories_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForCategories> modelNotifiedForCategories_ComboItemSource
        {
            get
            {
                return _modelNotifiedForCategories_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForCategories_ComboItemSource)
                {
                    _modelNotifiedForCategories_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForCategories_ComboItemSource");
                }
            }
        }
        
        //old Remove later. DataContext.html holds the code. This is the original.// public ObservableCollection<ModelNotifiedForProducts> modelNotifiedForProducts { get; set; }

        /// <summary>
        /// Defines the main class holding Grid's data. This data is filtered/copied to "GridData" and binded to grid.
        /// </summary>
        public List<ModelNotifiedForProducts> modelNotifiedForProductsMain { get; set; }

        /// <summary>
        /// Holds the data binded to Grid. 
        /// </summary>
        public ObservableCollection<ModelNotifiedForProducts> GridData { get; set; }

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

