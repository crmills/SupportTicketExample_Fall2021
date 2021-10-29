using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        internal static string PersistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ObservableCollection<Item> ToDoList { get; set; }
        private bool isSortedAsc;

        public ObservableCollection<Item> FilteredItemList
        {
            get
            {
                if(string.IsNullOrEmpty(Query))
                {
                    return isSortedAsc 
                        ? new ObservableCollection<Item>(ToDoList.OrderBy(t => t.Priority)) 
                        : new ObservableCollection<Item>(ToDoList.OrderByDescending(t => t.Priority));
                }

                return isSortedAsc
                    ? new ObservableCollection<Item>(
                    ToDoList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper()) 
                    || t.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderBy(t => t.Priority))
                    : new ObservableCollection<Item>(
                    ToDoList.Where(t => t.Name.ToUpper().Contains(Query.ToUpper())
                    || t.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderByDescending(t => t.Priority));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Item SelectedItem { get; set; }
        public MainViewModel()
        {
            ToDoList = new ObservableCollection<Item>();
        }

        public void RemoveItem()
        {
            if(SelectedItem != null)
            {
                ToDoList.Remove(SelectedItem);
            }
        }

        public string Query
        {
            get; set;
        }

        public void ToggleCompleteness()
        {
            if(SelectedItem == null || (SelectedItem as ToDo) == null)
            {
                return;
            }
            (SelectedItem as ToDo).IsCompleted = !(SelectedItem as ToDo).IsCompleted;
        }

        public void SaveState()
        {
            File.WriteAllText(PersistencePath, JsonConvert.SerializeObject(this, Settings));
        }

        public void RefreshList()
        {
            NotifyPropertyChanged("FilteredItemList");
        }

        public void Sort()
        {
            isSortedAsc = !isSortedAsc;
            RefreshList();
        }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
