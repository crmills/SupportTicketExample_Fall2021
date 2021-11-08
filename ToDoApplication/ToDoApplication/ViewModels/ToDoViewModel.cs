using Library.ToDoApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ToDoApplication.ViewModels
{
    public class ToDoViewModel : ItemViewModel, INotifyPropertyChanged
    {
        public override Visibility IsCompleteable => Visibility.Visible;

        public ToDoViewModel(ToDo todo)
        {
            Item = todo;
        }

        public ToDoViewModel()
        {
            Item = new ToDo();
        }
    }
}
