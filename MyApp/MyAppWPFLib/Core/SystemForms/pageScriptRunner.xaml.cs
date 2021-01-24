using Microsoft.Win32;
using MyAppDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
    public class ModelNotifiedLog : INotifyPropertyChanged
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                RaiseProperChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }

    public class ScriptRunnerViewModel : INotifyPropertyChanged
    {
        public ScriptRunnerViewModel()
        {
            SqlScriptLog = new ObservableCollection<ModelNotifiedLog>();
            SqlScriptLog.CollectionChanged += MyItemsSource_CollectionChanged;
        }

        void MyItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseProperChanged();
        }

        public string SqlFile { get; set; }

        private string _SqlScript { get; set; }

        public string SqlScript
        {
            get
            {
                return _SqlScript;
            }
            set
            {
                _SqlScript = value;
                RaiseProperChanged();
            }
        }
        public string ConnectionString { get; set; }

        //public List<string> SqlScriptLog { get; set; }

        private ObservableCollection<ModelNotifiedLog> _SqlScriptLog;
        public ObservableCollection<ModelNotifiedLog> SqlScriptLog
        {
            get { return _SqlScriptLog; }
            set
            {
                _SqlScriptLog = value;
                RaiseProperChanged("SqlScriptLog");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }


    }


    /// <summary>
    /// Interaction logic for ScriptRunner.xaml
    /// </summary>
    public partial class pageScriptRunner : Page
    {
        public ScriptRunnerViewModel ScriptRunnerViewModel { get; set; }
        public string PathForScripts { get; set; }

        public pageScriptRunner(string connectionString, string scriptFileName)
        {
            ScriptRunnerViewModel = new ScriptRunnerViewModel();

            InitializeComponent();
            if (File.Exists(scriptFileName))
            {
                ScriptRunnerViewModel.SqlScript = File.ReadAllText(scriptFileName);
                ScriptRunnerViewModel.SqlFile = scriptFileName;
                PathForScripts = System.IO.Path.GetDirectoryName(scriptFileName);
            }
            ScriptRunnerViewModel.ConnectionString = connectionString;
            this.DataContext = ScriptRunnerViewModel;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = PathForScripts;
            chkTreatAsCVS.IsChecked = false;

            if (openFileDialog.ShowDialog() == true)
            {
                ScriptRunnerViewModel.SqlFile = openFileDialog.FileName;
                ScriptRunnerViewModel.SqlScript = File.ReadAllText(openFileDialog.FileName);
                if (openFileDialog.FileName.EndsWith("csv"))
                {
                    var filename = System.IO.Path.GetFileName(openFileDialog.FileName);
                    chkTreatAsCVS.IsChecked = true;
                    txtCVSTableName.Text = filename.Replace(".csv", "");
                }                
            }
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            btnRun.IsEnabled = false;
            MyAppDB.ScriptRunner scriptRunner = new MyAppDB.ScriptRunner();
            ScriptRunnerViewModel.SqlScriptLog.Clear();
            //await scriptRunner.Test(ScriptRunnerViewModel.SqlScriptLog, "Message"); For test. Dont remove

            DateTime dateTimeBegin = DateTime.Now;

            if ((bool)chkTreatAsCVS.IsChecked)
            {
                ScriptRunnerParams scriptRunnerParams = new ScriptRunnerParams();
                scriptRunnerParams.IncludeDBMessages = (bool)chkAddDBOutput.IsChecked;
                scriptRunnerParams.CVSParams = new CVSParams();
                scriptRunnerParams.CVSParams.IsCVS = true;
                scriptRunnerParams.CVSParams.GenerateSQLFileAndDontRunCVS = (bool)chkCreateSQL.IsChecked;
                scriptRunnerParams.CVSParams.CVSTableName = txtCVSTableName.Text;
                await scriptRunner.TryRunScriptFileWithMultipleCommandsAsyncV2(scriptRunnerParams, ScriptRunnerViewModel.SqlFile, ScriptRunnerViewModel.ConnectionString, ScriptRunnerViewModel.SqlScriptLog, "Message");
            }
            else
            {
                ScriptRunnerParams scriptRunnerParams = new ScriptRunnerParams();
                scriptRunnerParams.IncludeDBMessages = (bool)chkAddDBOutput.IsChecked;
                scriptRunnerParams.CVSParams = null;
                await scriptRunner.TryRunScriptFileWithMultipleCommandsAsync((bool)chkAddDBOutput.IsChecked, ScriptRunnerViewModel.SqlFile, ScriptRunnerViewModel.ConnectionString, ScriptRunnerViewModel.SqlScriptLog, "Message");
            }

            DateTime dateTimeEnd = DateTime.Now;
            TimeSpan difference = dateTimeEnd - dateTimeBegin;
            string timeUsed = difference.ToString();
            MessageBox.Show("Completed:" + timeUsed);
            btnRun.IsEnabled = true;
        }
    }
}
