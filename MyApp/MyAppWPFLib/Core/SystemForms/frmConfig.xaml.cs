using MyAppDataAccessLib;
using MyAppGlobalLib;
using MyAppWPF.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAppWPFLib
{
    /// <summary>
    /// Interaction logic for frmConfig.xaml
    /// </summary>
    public partial class frmConfig : Page
    {
        public frmConfig(GlobalConfiguration globalConfiguration)
        {
            InitializeComponent();
            this.globalConfiguration = globalConfiguration;
            //GlobalConfigReader globalConfigReader = new GlobalConfigReader();
            //globalConfiguration = globalConfigReader.Load();

            DataContext = globalConfiguration;

            List<string> languages = new List<string>();
            languages.Add("de-DE");
            languages.Add("pt-BR");
            languages.Add("en-US");
            languages.Add("fr-FR");
            cbLanguages.ItemsSource = languages;


            List<string> connectionTypeList = new List<string>();
            connectionTypeList.Add("SqlServer");
            connectionTypeList.Add("REST");
            cbConectionType.ItemsSource = connectionTypeList;
            cbConectionType.SelectedValue = globalConfiguration.WPFConfig.connectionType.ToString();
        }

        public string configFile { get; set; }
        public GlobalConfiguration globalConfiguration { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Enum.TryParse(cbConectionType.SelectedValue.ToString(), out WPFConfig.ConnectionType connectionType);
                globalConfiguration.WPFConfig.connectionType = connectionType;
                //MessageBox.Show(globalConfiguration.WPFConfig.AppLanguage + Environment.NewLine + globalConfiguration.WPFConfig.connectionType.ToString());
                GlobalConfigReader globalConfigReader = new GlobalConfigReader();
                globalConfigReader.Save(globalConfiguration);
                MessageBox.Show("Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string btnTag = ((Button)sender).Tag.ToString();
                AppFolder itemSelected = globalConfiguration.WPFConfig.AppFolders.Where(x => x.Name == btnTag).FirstOrDefault();
                string directory = itemSelected.Value;
                if (Directory.Exists(directory))
                {
                    Process.Start("explorer.exe", directory);
                }
                else
                {
                    MessageBox.Show("Can't open directory:" + Environment.NewLine + directory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error trying to open directory:" + ex.Message);
            }

        }

        private void ShowPasswordCharsCheckBox_Checked(object sender, RoutedEventArgs e)
        {            
            MyPassword.Visibility = System.Windows.Visibility.Visible;
            MyPassword.Focus();
        }

        private void ShowPasswordCharsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MyPassword.Visibility = System.Windows.Visibility.Collapsed;
            MyPassword.Focus();
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            Motor motor = new Motor(MyPassword.Text);

            try
            {
                motor.OpenConnection();
                motor.CloseConnection();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btnResetDatabank_Click(object sender, RoutedEventArgs e)
        {
            var file = this.globalConfiguration.WPFConfig.ProjectFiles.Where(x => x.Name == "DefaultDBScript").FirstOrDefault();
            pageScriptRunner page = new pageScriptRunner(this.globalConfiguration.WPFConfig.GetMainConnectionString, file.Value); ;
            ContainerWindowSimple win = new ContainerWindowSimple(page, "Script Runner (Reset Database)");
            win.Show();
        }
    }
}
