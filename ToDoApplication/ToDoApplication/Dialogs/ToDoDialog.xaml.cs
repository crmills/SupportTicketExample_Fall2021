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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ToDoApplication.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {
        private IList<Item> _todoList;
        public ToDoDialog(IList<Item> todoList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(null);
            _todoList = todoList;
        }

        public ToDoDialog(Item selectedToDo, IList<Item> todoList)
        {
            this.InitializeComponent();
            DataContext = new DialogViewModel(selectedToDo);
            _todoList = todoList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var context = DataContext as DialogViewModel;
            if(context.BoundToDo != null)
            {
                var todo = context.BoundToDo;
                if(todo.Id <= 0)
                {
                    FakeDatabase.LastTodoId++;
                    todo.Id = FakeDatabase.LastTodoId;
                    _todoList.Add(todo);
                }
            } else if (context.BoundAppointment != null)
            {
                var appointment = context.BoundAppointment;
                if(appointment.Id <= 0)
                {
                    FakeDatabase.LastTodoId++;
                    appointment.Id = FakeDatabase.LastTodoId;
                    _todoList.Add(appointment);
                }
            }
            
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
