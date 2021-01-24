//Track[0002] Template:WPF_Shared_DataContext.html
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Territories
{
    public class TerritoriesDataContext : INotifyPropertyChanged
    {
        public TerritoriesDataContext()
        {
            this.LabelsAndMessagesTerritories = new LabelsAndMessagesTerritories();
        }

        
        public event PropertyChangedEventHandler PropertyChanged;
        //Track [0001]        
        /// <summary>
        /// Populates combo box item source
        /// </summary>
        public void PopulateComboBoxesItemSource()
        {
            modelNotifiedForRegion_ComboItemSource = new ObservableCollection<ModelNotifiedForRegion>(modelNotifiedForRegion);
        }
        
        /// <summary>
        /// All itens. Used later to populate combo box
        /// </summary>
        public List<ModelNotifiedForRegion> modelNotifiedForRegion {get; set;}
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        private ObservableCollection<ModelNotifiedForRegion> _modelNotifiedForRegion_ComboItemSource { get; set; }
        
        /// <summary>
        /// Combo box item source. If changed, alters the combo box data
        /// </summary>
        public ObservableCollection<ModelNotifiedForRegion> modelNotifiedForRegion_ComboItemSource
        {
            get
            {
                return _modelNotifiedForRegion_ComboItemSource;
            }
            set
            {
                if (value != _modelNotifiedForRegion_ComboItemSource)
                {
                    _modelNotifiedForRegion_ComboItemSource = value;
                    RaiseProperChanged("modelNotifiedForRegion_ComboItemSource");
                }
            }
        }
        
        //old. Remove later. DataContext.html holds the code This is the original.//public ModelNotifiedForTerritories modelNotifiedForTerritoriesMain { get; set; }
        /// <summary>
        /// Contains Form's labels and messages
        /// </summary>        
        public LabelsAndMessagesTerritories LabelsAndMessagesTerritories { get; set; }

        /// <summary>
        /// Defines the main class holding Form's data
        /// </summary>
        public ModelNotifiedForTerritories modelNotifiedForTerritoriesMain { get; set; }


        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

