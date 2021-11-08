using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Library.ToDoApplication.Models;

namespace ToDoApplication.ViewModels
{
    public class DialogViewModel: INotifyPropertyChanged
    {
        private IList<Item> _itemList;
        public ToDoViewModel BoundToDo { get; set; }
        public AppointmentViewModel BoundAppointment { get; set; }

        private bool isTodo;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsTodo { 
            get {
                return isTodo;
            } 

            set
            {
                isTodo = value;

                if((BoundToDo == null || BoundToDo.Item.Id <=0) && (BoundAppointment == null || BoundAppointment.Item.Id <=0))
                {
                    if (isTodo)
                    {
                        BoundToDo = new ToDoViewModel();
                        BoundAppointment = null;
                    }
                    else
                    {
                        BoundAppointment = new AppointmentViewModel();
                        BoundToDo = null;
                    }
                }

                NotifyPropertyChanged("IsToDoVisible");
                NotifyPropertyChanged("IsAppointmentVisible");
                NotifyPropertyChanged("BoundAppointment");
                NotifyPropertyChanged("BoundToDo");
            }
        }

        public Visibility IsToDoVisible
        {
            get
            {
                return IsTodo ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility IsAppointmentVisible
        {
            get
            {
                return IsTodo ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        
        public DialogViewModel(ItemViewModel itemViewModel)
        {
            if(itemViewModel is AppointmentViewModel)
            {
                BoundAppointment = itemViewModel as AppointmentViewModel;
                BoundToDo = null;
                IsTodo = false;

                NotifyPropertyChanged("BoundAppointment");
            } else if (itemViewModel is ToDoViewModel)
            {
                BoundToDo = itemViewModel as ToDoViewModel;
                BoundAppointment = null;
                IsTodo = true;
                NotifyPropertyChanged("BoundToDo");
            } else
            {
                IsTodo = true;
            }
        }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
