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
                if(todo.Item.Id <= 0)
                {
                    FakeDatabase.LastTodoId++;
                    todo.Item.Id = FakeDatabase.LastTodoId;
                    _todoList.Add(todo);

                    await new WebRequestHandler().Post("http://localhost:14102/ToDo/AddOrUpdate", todo.Item);
                }
            } else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if(appointment.Item.Id <= 0)
                {
                    FakeDatabase.LastTodoId++;
                    appointment.Item.Id = FakeDatabase.LastTodoId;
                    _todoList.Add(appointment);
                }
            }
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
