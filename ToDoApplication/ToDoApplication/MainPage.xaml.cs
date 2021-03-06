using Library.ToDo.Communication;
using Library.ToDoApplication;
using Library.ToDoApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ToDoApplication.Dialogs;
using ToDoApplication.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDoApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        public MainPage()
        {
            this.InitializeComponent();
            //if(File.Exists(MainViewModel.PersistencePath))
            //{
            //    try
            //    {
            //        DataContext = JsonConvert
            //            .DeserializeObject<MainViewModel>(File.ReadAllText(MainViewModel.PersistencePath), MainViewModel.Settings);
            //    } catch(Exception e)
            //    {
            //        DataContext = new MainViewModel();
            //        File.Delete(MainViewModel.PersistencePath);
            //    }

            //} else
            //{
            //    DataContext = new MainViewModel();
            //}
            var mainViewModel = new MainViewModel();
            var todoString = new WebRequestHandler().Get("http://localhost:14102/ToDo").Result;
            var todos = JsonConvert.DeserializeObject<List<ToDo>>(todoString);
            todos.ForEach(t => mainViewModel.ToDoList.Add(new ToDoViewModel(t)));
            var appointmentsString = new WebRequestHandler().Get("http://localhost:14102/Appointment").Result;
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(appointmentsString);
            appointments.ForEach(a => mainViewModel.ToDoList.Add(new AppointmentViewModel(a)));

            DataContext = mainViewModel;
            (DataContext as MainViewModel).RefreshList();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var todoDialog = new ToDoDialog((DataContext as MainViewModel).ToDoList);
            await todoDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = DataContext as MainViewModel;
            var todoDialog = new ToDoDialog(dataContext.SelectedItem, dataContext.ToDoList);
            await todoDialog.ShowAsync();
            (DataContext as MainViewModel).RefreshList();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).ToggleCompleteness();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RefreshList();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }
    }
}
