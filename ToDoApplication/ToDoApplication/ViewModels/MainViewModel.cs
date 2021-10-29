using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        internal static string PersistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ObservableCollection<Item> ToDoList { get; set; }

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
    }
}
