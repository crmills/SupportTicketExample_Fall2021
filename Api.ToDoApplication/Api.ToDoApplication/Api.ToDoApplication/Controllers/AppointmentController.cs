using Api.ToDoApplication.Persistence;
using Library.ToDoApplication.Models;
using Library.ToDoApplication.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.ToDoApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private object _lock;
        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new Appointment();
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return Database.Appointments;
        }

        [HttpPost("AddOrUpdate")]
        public Appointment AddOrUpdate([FromBody] Appointment appointment)
        {
            if(appointment.Id <= 0)
            {
                lock (_lock)
                {
                    var lastUsedId = Database.Appointments.Select(a => a.Id).Max();
                    appointment.Id = lastUsedId + 1;
                    Database.Appointments.Add(appointment);
                }
            } else
            {
                var item = Database.Appointments.FirstOrDefault(t => t.Id == appointment.Id);
                var index = Database.Appointments.IndexOf(item);
                Database.Appointments.RemoveAt(index);
                Database.Appointments.Insert(index, appointment);
            }

            return appointment;
        }

        [HttpGet("Delete/{id}")]
        public bool Delete(int id)
        {
            try
            {
                var appToRemove = Database.Appointments.FirstOrDefault(a => a.Id == appointment.Id);
                Database.Appointments.Remove(appToRemove);

            } catch(Exception)
            {
                return false;
            }


            return true;
        }
    }
}
