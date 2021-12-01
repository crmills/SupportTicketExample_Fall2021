using Api.ToDoApplication.Persistence;
using Library.ToDo.Models;
using Library.ToDoApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.ToDoApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;


        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return Database.Current.ToDos;
        }

        [HttpGet("GetItem")]
        public Item GetTestItem()
        {
            return new ToDo();
        }

        [HttpPost("AddOrUpdate")]
        public Item Receive([FromBody] ToDo todo)
        {
            Database.Current.AddOrUpdate(todo);
            return todo;
        }

        [HttpPost("Search")]
        public IList<ToDo> Search(QueryDTO query)
        {
            return Database.Current.ToDos
                .Where(t => t.Name.ToUpper().Contains(query.QueryText) || t.Description.ToUpper().Contains(query.QueryText)).ToList();
        }
    }
}
