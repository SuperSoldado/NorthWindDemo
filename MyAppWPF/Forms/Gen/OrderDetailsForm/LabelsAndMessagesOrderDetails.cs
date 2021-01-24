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
    public class LabelsAndMessagesOrderDetails
    {    
        public LabelsFromDBOrderDetails LabelsFromDBOrderDetails { get; set; }
        public LanguageElementsControlsOrderDetails LanguageElementsControlsOrderDetails { get; set; }
        public LanguageElementsMessagesOrderDetails LanguageElementsMessagesOrderDetails { get; set; }
        public LabelsAndMessagesOrderDetails()
        {
            this.LabelsFromDBOrderDetails = new LabelsFromDBOrderDetails();
            this.LanguageElementsControlsOrderDetails = new LanguageElementsControlsOrderDetails();
            this.LanguageElementsMessagesOrderDetails = new LanguageElementsMessagesOrderDetails();
        }        
    }

    public class LabelsFromDBOrderDetails
    { 
        public LabelsFromDBOrderDetails()
        {
            this.LabelOrders_ShipName = "Orders_ShipName";
            this.LabelProducts_ProductName = "Products_ProductName";
            this.LabelUnitPrice = "UnitPrice";
            this.LabelQuantity = "Quantity";
            this.LabelDiscount = "Discount";
        }
        public string LabelOrders_ShipName { get; set; }
        public string LabelProducts_ProductName { get; set; }
        public string LabelUnitPrice { get; set; }
        public string LabelQuantity { get; set; }
        public string LabelDiscount { get; set; }
    }

    public class LanguageElementsControlsOrderDetails
    {
        public LanguageElementsControlsOrderDetails()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesOrderDetails
    {
        public LanguageElementsMessagesOrderDetails()
        {
            this.MessageBoxSaveOK = "Save";
            this.MessageBoxSaveError = "Error:";
            this.MessageBoxDeleteConfirm = "Are you sure?";
            this.MessageBoxDeleteOK = "Deleted";
        }

        public string MessageBoxSaveOK { get; set; }
        public string MessageBoxSaveError { get; set; }
        public string MessageBoxDeleteConfirm { get; set; }
        public string MessageBoxDeleteOK { get; set; }
    }
}
