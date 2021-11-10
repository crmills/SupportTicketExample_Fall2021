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
        public static ObservableCollection<ToDo> ToDos { get; set; } = new ObservableCollection<ToDo> { 
            new ToDo { Name = "First",
                       Description = "First ToDo"},
            new ToDo { Name = "Second",
                       Description = "Second ToDo"}
        };
        public static ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>
        {
            new Appointment { Name = "1st",
                       Description = "First Appointment"},
            new Appointment { Name = "2nd",
                       Description = "Second Appointment"}
        };




    }
}
