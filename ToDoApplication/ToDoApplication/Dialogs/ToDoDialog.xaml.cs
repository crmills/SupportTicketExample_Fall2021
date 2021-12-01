using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Library.ToDoApplication.Models;
using Library.ToDoApplication.Persistence;
using Library.ToDo.Communication;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoApplication.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {
        private IList<ItemViewModel> _todoList;
        public ToDoDialog(IList<ItemViewModel> todoList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(null);
            _todoList = todoList;
        }

        public ToDoDialog(ItemViewModel selectedToDo, IList<ItemViewModel> todoList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(selectedToDo);
            _todoList = todoList;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if(context.BoundToDo != null)
            {
                var todo = context.BoundToDo;
                var isNew = string.IsNullOrEmpty(todo.Item._id);
                var newToDoResponse = await new WebRequestHandler().Post("http://localhost:14102/ToDo/AddOrUpdate", todo.Item);
                var newToDo = JsonConvert.DeserializeObject<ToDo>(newToDoResponse);
                if(isNew)
                {
                    _todoList.Add(new ToDoViewModel(newToDo));
                } else
                {
                    _todoList.FirstOrDefault(t => t.Item._id.Equals(newToDo._id)).Item = newToDo;
                }
            } else if (context.BoundAppointment != null)
            {
                var app = context.BoundAppointment;
                var isNew = string.IsNullOrEmpty(app.Item._id);
                var newAppResponse = await new WebRequestHandler().Post("http://localhost:14102/Appointment/AddOrUpdate", app.Item);
                var newApp = JsonConvert.DeserializeObject<Appointment>(newAppResponse);
                if (isNew)
                {
                    _todoList.Add(new AppointmentViewModel(newApp));
                }
                else
                {
                    _todoList.FirstOrDefault(t => t.Item._id.Equals(newApp._id)).Item = newApp;
                }
            }
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
