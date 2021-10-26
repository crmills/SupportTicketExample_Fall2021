using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication
{
    public class ToDo : INotifyPropertyChanged
    {
        public int Id {
            get; set;
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Priority { get; set; }

        private bool isCompleted;
        public bool IsCompleted { 
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{IsCompleted} [{Priority}] {Name} - {Description}";
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetId()
        {
            if(Id > 0)
            {
                return;
            }
            FakeDatabase.LastTodoId++;
            Id = FakeDatabase.LastTodoId;
        }

    }
}
