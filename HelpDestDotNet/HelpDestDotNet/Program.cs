using Library.HelpDesk.Models;
using Library.HelpDesk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HelpDestDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ItemBase> ticketList = null;
            var path = Environment.SpecialFolder.LocalApplicationData;
            if (File.Exists($"{path}\\SaveData.json"))
            {
                //deserialize the list
                ticketList = JsonConvert.DeserializeObject<List<ItemBase>>(File.ReadAllText("SaveData.json"));
            } else
            {
                ticketList = new List<ItemBase>();
            }
            var ticketNavigator = new ListNavigator<ItemBase>(ticketList, 2);
            Console.WriteLine("Welcome to the help desk!");
            bool cont = true;

            while(cont)
            {
                Console.WriteLine("Please choose from the menu:");
                Console.WriteLine("1. Add a ticket");
                Console.WriteLine("2. Edit a ticket");
                Console.WriteLine("3. Delete a ticket");
                Console.WriteLine("4. List tickets");
                Console.WriteLine("5. Exit\n");

                if(int.TryParse(Console.ReadLine(), out int choice)) {

                    switch(choice)
                    {
                        case 1:
                            //add
                            CreateOrEditSupportTicket(ticketList);
                            break;
                        case 2:
                            //edit
                            Console.WriteLine("Which would you like to edit?");
                            PrintTicketList(ticketNavigator);
                            if (int.TryParse(Console.ReadLine(), out int editChoice))
                            {
                                var ticketToEdit = ticketList.FirstOrDefault(t => t.Id == editChoice);
                                CreateOrEditSupportTicket(ticketList, ticketToEdit);
                            } else
                            {
                                Console.WriteLine("Invalid selection!");
                            }
                            break;
                        case 3:
                            //delete
                            Console.WriteLine("Which would you like to delete?");
                            PrintTicketList(ticketNavigator);
                            if(int.TryParse(Console.ReadLine(), out int deleteChoice))
                            {
                                var ticketToDelete = ticketList.FirstOrDefault(t => t.Id == deleteChoice);
                                ticketList.Remove(ticketToDelete);
                            } else
                            {
                                Console.WriteLine("Invalid choice!");
                            }
                            break;
                        case 4:
                            //list
                            PrintTicketList(ticketNavigator);
                            break;
                        case 5:
                            //exit
                            cont = false;
                            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All  };
                            File.WriteAllText("SaveData.json", JsonConvert.SerializeObject(ticketList, settings));
                            break;
                        default:
                            Console.WriteLine("Sorry, try again");
                            break;
                    }
                }else
                {
                    //the choice isn't an integer
                    Console.WriteLine("Sorry, try again!");
                }

            }
        }
        public static void CreateOrEditSupportTicket(List<ItemBase> ticketList, ItemBase ticket = null)
        {
            bool isNewTicket = false;
            if(ticket == null)
            {
                ticket = new SupportTicket();
                isNewTicket = true;
            }

            Console.WriteLine("What is the title?");
            ticket.Name = Console.ReadLine();

            Console.WriteLine("What is the description?");
            ticket.Description = Console.ReadLine();

            Console.WriteLine("What is the priority?");
            if(int.TryParse(Console.ReadLine(), out int priority))
            {
                ticket.Priority = priority;
            } else
            {
                Console.WriteLine("Invalid choice, defaulting to 3");
                ticket.Priority = 3;
            }

            if(ticket is SupportTicket) { 
                Console.WriteLine("What is the deadline?");
                if(DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
                {
                    (ticket as SupportTicket).Deadline = deadline;
                } else
                {
                    Console.WriteLine("Invalid choice, defaulting to today");
                    (ticket as SupportTicket).Deadline = DateTime.Today;
                }
            }

            ticket.DateAdded = DateTime.Now;

            if(isNewTicket)
            {
                ticketList.Add(ticket);
            }
        }

        public static void PrintTicketList(ListNavigator<ItemBase> itemNavigator)
        {
            //foreach (var ticket in ticketList)
            //{
            //    Console.WriteLine(ticket.ToString());
            //}
            bool isNavigating = true;
            while(isNavigating)
            {
                var page = itemNavigator.GetCurrentPage();
                foreach (var item in page)
                {
                    Console.WriteLine($"{item.Value}");
                }

                if (itemNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous");
                }

                if (itemNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next");
                }

                var selection = Console.ReadLine();
                if(selection.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                {
                    itemNavigator.GoBackward();
                } else if (selection.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                {
                    itemNavigator.GoForward();
                } else
                {
                    isNavigating = false;
                }

            }

            Console.WriteLine();
        }
    }
}
