using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Library.ToDoApplication.Models.REMOVE
{
    public class Item : INotifyPropertyChanged
    { 
        public int Id
        {
            get; set;
        }

        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PrimaryText");
                NotifyPropertyChanged("SecondaryText");
            }
        }

        private string description;
        public string Description { 
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PrimaryText");
                NotifyPropertyChanged("SecondaryText");
            }
        }

        public int Priority { get; set; }

        public virtual string PrimaryText { get; }
        public virtual string SecondaryText { get; }

        public virtual bool Completed { get; set; }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
