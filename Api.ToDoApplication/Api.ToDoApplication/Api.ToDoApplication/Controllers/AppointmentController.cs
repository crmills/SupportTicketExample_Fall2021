using Api.ToDoApplication.Persistence;
using Library.ToDoApplication.Models;
using Library.ToDoApplication.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.ToDoApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private object _lock = new object();
        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new Appointment();
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return Database.Current.Appointments;
        }

        [HttpPost("AddOrUpdate")]
        public Appointment AddOrUpdate([FromBody] Appointment appointment)
        {
            //if(appointment.Id <= 0)
            //{
            //    lock (_lock)
            //    {
            //        var lastUsedId = Database.Current.Appointments.Select(a => a.Id).Max();
            //        appointment.Id = lastUsedId + 1;
            //        Database.Current.Appointments.Add(appointment);
            //    }
            //} else
            //{
            //    var item = Database.Current.Appointments.FirstOrDefault(t => t.Id == appointment.Id);
            //    var index = Database.Current.Appointments.IndexOf(item);
            //    Database.Current.Appointments.RemoveAt(index);
            //    Database.Current.Appointments.Insert(index, appointment);
            //}

            return appointment;
        }

        [HttpGet("Delete/{id}")]
        public bool Delete(string id)
        {
            try
            {
                var appToRemove = Database.Current.Appointments.FirstOrDefault(a => a._id == id);
                Database.Current.Appointments.Remove(appToRemove);

            } catch(Exception)
            {
                return false;
            }


            return true;
        }
    }
}
