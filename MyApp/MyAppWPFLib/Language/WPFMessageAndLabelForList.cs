using System;
using System.Collections.Generic;
using System.Text;

namespace MyAppWPFLib
{
    /// <summary>
    /// Defines messages and labels used in Lists
    /// </summary>
    public class WPFMessageAndLabelForList
    {
        public string MessageBoxSaveOK { get; set; }
        public string MessageBoxSaveError { get; set; }
        public string MessageBoxDeleteConfirm { get; set; }
        public string MessageBoxDeleteConfirmCaption { get; set; }
        public string MessageBoxDeleteOK { get; set; }
        public string LabelBtnOpenForm { get; set; }
        public string LabelBtnDelete { get; set; }
        public string LabelBtnSave { get; set; }
        public string LabelBtnFilter { get; set; }
        public string LabelBtnReload { get; set; }
        public string LabelGroupVisibility { get; set; }
        public string LabelGroupSearch { get; set; }
        public string ComboDateBetween { get; set; }

        public WPFMessageAndLabelForList()
        {
            MessageBoxSaveOK = "Saved";
            MessageBoxSaveError = "Error:";
            MessageBoxDeleteConfirm = "Do you want to delete?";
            MessageBoxDeleteConfirmCaption = "Please confirm";
            MessageBoxDeleteOK = "Deleted";
            LabelBtnOpenForm = "Open";
            LabelBtnDelete = "Delete";
            LabelBtnSave = "Save";
            LabelBtnFilter = "Apply";
            LabelGroupVisibility = "Filters";
            LabelGroupSearch = "Search";
            LabelBtnReload = "Reload";
            ComboDateBetween = "Select Option";
        }
    }
}
