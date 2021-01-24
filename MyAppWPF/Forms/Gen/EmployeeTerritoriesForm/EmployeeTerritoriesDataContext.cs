//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.EmployeeTerritories
{
    public class EmployeeTerritoriesDataContext : INotifyPropertyChanged
    {
        public EmployeeTerritoriesDataContext()
        {
            this.LabelsAndMessagesEmployeeTerritories = new LabelsAndMessagesEmployeeTerritories();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForEmployees_ComboItemSource = new ObservableCollection<ModelNotifiedForEmployees>(modelNotifiedForEmployees);
            modelNotifiedForTerritories_ComboItemSource = new ObservableCollection<ModelNotifiedForTerritories>(modelNotifiedForTerritories);
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
        public List<ModelNotifiedForTerritories> modelNotifiedForTerritories {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForTerritories> _modelNotifiedForTerritories_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForTerritories> modelNotifiedForTerritories_ComboItemSource
        {
            get
            {
                return _modelNotifiedForTerritories_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForTerritories_ComboItemSource)
                {
                    _modelNotifiedForTerritories_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForTerritories_ComboItemSource");
                }
            }
        }
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritoriesMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesEmployeeTerritories LabelsAndMessagesEmployeeTerritories { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForEmployeeTerritories modelNotifiedForEmployeeTerritoriesMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

