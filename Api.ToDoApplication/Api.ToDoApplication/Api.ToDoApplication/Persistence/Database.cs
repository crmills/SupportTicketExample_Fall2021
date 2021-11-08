using Library.ToDoApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public static class Database
    {
        public static ObservableCollection<ToDo> ToDos { get; set; } = new ObservableCollection<ToDo>();
        public static ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();




    }
}
