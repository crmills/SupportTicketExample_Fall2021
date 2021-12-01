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

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return Database.Current.Appointments;
        }

        [HttpPost("AddOrUpdate")]
        public Item Receive([FromBody] Appointment app)
        {
            Database.Current.AddOrUpdate(app);
            return app;
        }

        [HttpGet("Delete/{id}")]
        public bool Delete(string id)
        {
            //Database.Delete("Appointment", id)
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
