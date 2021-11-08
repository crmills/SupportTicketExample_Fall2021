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
    public class AppointmentViewModel : ItemViewModel, INotifyPropertyChanged
    {
        public override Visibility IsCompleteable => Visibility.Collapsed;

        public AppointmentViewModel(Appointment app)
        {
            Item = app;
        }

        public AppointmentViewModel()
        {
            Item = new Appointment();
        }
    }
}
