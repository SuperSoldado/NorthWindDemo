using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyAppWPF.Core
{

    public class MyItem : INotifyPropertyChanged
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

    public class MyViewModel : INotifyPropertyChanged
    {
        public MyViewModel()
        {
            MyObervableList = new ObservableCollection<MyItem>();
            MyObervableList.CollectionChanged += MyItemsSource_CollectionChanged;
        }
        void MyItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseProperChanged();
        }
        private ObservableCollection<MyItem> _MyObervableList;
        public ObservableCollection<MyItem> MyObervableList
        {
            get { return _MyObervableList; }
            set
            {
                _MyObervableList = value;
                //RaiseProperChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }
    }


    /// <summary>
    /// Interaction logic for MyTestWindow.xaml
    /// </summary>
    public partial class MyTestWindow : Window
    {
        MyViewModel myViewModel { get; set; }

        public MyTestWindow()
        {
            InitializeComponent();
            myViewModel = new MyViewModel();
            DataContext = myViewModel;
            isCanceled = false;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            isCanceled = !isCanceled;
            DoStuff();

            //alternativo: MyRunner();
        }

        public async Task MyRunner()
        {
            for (int i = 0; i < 10; i++)
            {
                MyItem myItem = new MyItem();
                myItem.Message = i.ToString();
                myViewModel.MyObervableList.Add(myItem);
                await Task.Delay(1000);
            }
        }

        private bool isCanceled { get; set; }

        private CancellationTokenSource _cancelTasks;
        
        //fonte:
        //https://stackoverflow.com/questions/20638952/cancellationtoken-and-cancellationtokensource-how-to-use-it
        private void DoStuff()
        {
            _cancelTasks = new CancellationTokenSource();

            var task = new Task(() => 
            { 
                MyRunner(); 
            }, _cancelTasks.Token);
            task.Start();

            if (!task.Wait(5000))
                _cancelTasks.Cancel();
        }

        

        public async Task MyMethodAsync()
        {
            Task<int> longRunningTask = LongRunningOperationAsync();
            // independent work which doesn't need the result of LongRunningOperationAsync can be done here

            //and now we call await on the task 
            int result = await longRunningTask;
            //use the result 
            Console.WriteLine(result);
        }

        public async Task<int> LongRunningOperationAsync() // assume we return an int from this long running operation 
        {
            await Task.Delay(1000); // 1 second delay
            return 1;
        }
    }
}
