using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MyAppWPFLib;

namespace MyApp.WPFForms.Products
{
    public class LabelsAndMessagesProducts
    {    
        public LabelsFromDBProducts LabelsFromDBProducts { get; set; }
        public LanguageElementsControlsProducts LanguageElementsControlsProducts { get; set; }
        public LanguageElementsMessagesProducts LanguageElementsMessagesProducts { get; set; }
        public LabelsAndMessagesProducts()
        {
            this.LabelsFromDBProducts = new LabelsFromDBProducts();
            this.LanguageElementsControlsProducts = new LanguageElementsControlsProducts();
            this.LanguageElementsMessagesProducts = new LanguageElementsMessagesProducts();
        }        
    }

    public class LabelsFromDBProducts
    { 
        public LabelsFromDBProducts()
        {
            this.LabelProductID = "ProductID";
            this.LabelProductName = "ProductName";
            this.LabelSuppliers_CompanyName = "Suppliers_CompanyName";
            this.LabelCategories_CategoryName = "Categories_CategoryName";
            this.LabelQuantityPerUnit = "QuantityPerUnit";
            this.LabelUnitPrice = "UnitPrice";
            this.LabelUnitsInStock = "UnitsInStock";
            this.LabelUnitsOnOrder = "UnitsOnOrder";
            this.LabelReorderLevel = "ReorderLevel";
            this.LabelDiscontinued = "Discontinued";
        }
        public string LabelProductID { get; set; }
        public string LabelProductName { get; set; }
        public string LabelSuppliers_CompanyName { get; set; }
        public string LabelCategories_CategoryName { get; set; }
        public string LabelQuantityPerUnit { get; set; }
        public string LabelUnitPrice { get; set; }
        public string LabelUnitsInStock { get; set; }
        public string LabelUnitsOnOrder { get; set; }
        public string LabelReorderLevel { get; set; }
        public string LabelDiscontinued { get; set; }
    }

    public class LanguageElementsControlsProducts
    {
        public LanguageElementsControlsProducts()
        {
            this.LabelBtnDelete = "btnDelete";
            this.LabelBtnNew = "btnNew";
            this.LabelBtnUpdate = "btnUpdate";            
        }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnNew { get; set; }
        public string LabelBtnUpdate { get; set; }
    }

    public class LanguageElementsMessagesProducts
    {
        public LanguageElementsMessagesProducts()
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
