using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ToDoApplication.ViewModels
{
    public class DialogViewModel: INotifyPropertyChanged
    {
        private IList<Item> _itemList;
        public ToDo BoundToDo { get; set; }
        public Appointment BoundAppointment { get; set; }

        private bool isTodo;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsTodo { 
            get {
                return isTodo;
            } 

            set
            {
                isTodo = value;

                if((BoundToDo == null || BoundToDo.Id <=0) && (BoundAppointment == null || BoundAppointment.Id <=0))
                {
                    if (isTodo)
                    {
                        BoundToDo = new ToDo();
                        BoundAppointment = null;
                    }
                    else
                    {
                        BoundAppointment = new Appointment();
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
        
        public DialogViewModel(Item item)
        {
            if(item is Appointment)
            {
                BoundAppointment = item as Appointment;
                BoundToDo = null;
                IsTodo = false;

                NotifyPropertyChanged("BoundAppointment");
            } else if (item is ToDo)
            {
                BoundToDo = item as ToDo;
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
