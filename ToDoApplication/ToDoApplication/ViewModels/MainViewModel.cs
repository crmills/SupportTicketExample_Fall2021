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
using Library.ToDoApplication.Models;
using Library.ToDo.Communication;
using Library.ToDo.Models;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        internal static string PersistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ObservableCollection<ItemViewModel> ToDoList { get; set; }
        private bool isSortedAsc;

        public ObservableCollection<ItemViewModel> FilteredItemList
        {
            get
            {
                if(string.IsNullOrEmpty(Query))
                {
                    return isSortedAsc 
                        ? new ObservableCollection<ItemViewModel>(ToDoList.OrderBy(t => t.Item.Priority)) 
                        : new ObservableCollection<ItemViewModel>(ToDoList.OrderByDescending(t => t.Item.Priority));
                    //Perform the same algorithm from your "on load" read, but apply a sort order
                } else
                {
                    //get the todos that match the query
                    var response = new WebRequestHandler().Post("http://localhost:14102/ToDo/Search", new QueryDTO { QueryText = Query }).Result;
                    //get the appointments that match the query
                    //merge the lists into a central list

                    //add a filter for show incomplete

                    //sort appropriately using the isSortedAsc flag
                }


                var list = isSortedAsc
                    ? new ObservableCollection<ItemViewModel>(
                    ToDoList.Where(t => t.Item.Name.ToUpper().Contains(Query.ToUpper()) 
                    || t.Item.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t.Item is Appointment) && (t.Item as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderBy(t => t.Item.Priority))
                    : new ObservableCollection<ItemViewModel>(
                    ToDoList.Where(t => t.Item.Name.ToUpper().Contains(Query.ToUpper())
                    || t.Item.Description.ToUpper().Contains(Query.ToUpper())
                    || ((t.Item is Appointment) && (t.Item as Appointment).Attendees.Any(s => s.Contains(Query)))
                    ).OrderByDescending(t => t.Item.Priority));

                return list;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ItemViewModel SelectedItem { get; set; }
        public MainViewModel()
        {
            ToDoList = new ObservableCollection<ItemViewModel>();
        }

        public void RemoveItem()
        {
            if(SelectedItem != null)
            {
                //make a web call to delete this same item on the server
                ToDoList.Remove(SelectedItem);

            }
        }

        public string Query
        {
            get; set;
        }

        public void ToggleCompleteness()
        {
            if(SelectedItem == null || (SelectedItem.Item as ToDo) == null)
            {
                return;
            }
            (SelectedItem.Item as ToDo).IsCompleted = !(SelectedItem.Item as ToDo).IsCompleted;
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
