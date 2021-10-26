using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private IList<ToDo> _todoList;
        public ToDoDialog(IList<ToDo> todoList)
        {
            this.InitializeComponent();
            DataContext = new ToDo();
            _todoList = todoList;
        }

        public ToDoDialog(ToDo selectedToDo, IList<ToDo> todoList)
        {
            this.InitializeComponent();
            DataContext = selectedToDo;
            _todoList = todoList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var todo = DataContext as ToDo;
            var todoIsNew = todo.Id <= 0;
            todo.SetId();
            if(todoIsNew)
            {
                _todoList.Add(todo);
            } else
            {
                var todoToEdit = _todoList.FirstOrDefault(t => t.Id == todo.Id);
                var index = _todoList.IndexOf(todoToEdit);
                _todoList.RemoveAt(index);
                _todoList.Insert(index, todo);
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
