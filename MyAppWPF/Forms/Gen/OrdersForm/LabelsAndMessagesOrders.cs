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
    public class LabelsAndMessagesOrders
    {    
        public LabelsFromDBOrders LabelsFromDBOrders { get; set; }
        public LanguageElementsControlsOrders LanguageElementsControlsOrders { get; set; }
        public LanguageElementsMessagesOrders LanguageElementsMessagesOrders { get; set; }
        public LabelsAndMessagesOrders()
        {
            this.LabelsFromDBOrders = new LabelsFromDBOrders();
            this.LanguageElementsControlsOrders = new LanguageElementsControlsOrders();
            this.LanguageElementsMessagesOrders = new LanguageElementsMessagesOrders();
        }        
    }

    public class LabelsFromDBOrders
    { 
        public LabelsFromDBOrders()
        {
            this.LabelOrderID = "OrderID";
            this.LabelCustomers_ContactName = "Customers_ContactName";
            this.LabelEmployees_LastName = "Employees_LastName";
            this.LabelOrderDate = "OrderDate";
            this.LabelRequiredDate = "RequiredDate";
            this.LabelShippedDate = "ShippedDate";
            this.LabelShippers_CompanyName = "Shippers_CompanyName";
            this.LabelFreight = "Freight";
            this.LabelShipName = "ShipName";
            this.LabelShipAddress = "ShipAddress";
            this.LabelShipCity = "ShipCity";
            this.LabelShipRegion = "ShipRegion";
            this.LabelShipPostalCode = "ShipPostalCode";
            this.LabelShipCountry = "ShipCountry";
        }
        public string LabelOrderID { get; set; }
        public string LabelCustomers_ContactName { get; set; }
        public string LabelEmployees_LastName { get; set; }
        public string LabelOrderDate { get; set; }
        public string LabelRequiredDate { get; set; }
        public string LabelShippedDate { get; set; }
        public string LabelShippers_CompanyName { get; set; }
        public string LabelFreight { get; set; }
        public string LabelShipName { get; set; }
        public string LabelShipAddress { get; set; }
        public string LabelShipCity { get; set; }
        public string LabelShipRegion { get; set; }
        public string LabelShipPostalCode { get; set; }
        public string LabelShipCountry { get; set; }
    }

    public class LanguageElementsControlsOrders
    {
        public LanguageElementsControlsOrders()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesOrders
    {
        public LanguageElementsMessagesOrders()
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
