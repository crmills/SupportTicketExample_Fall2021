
using Library.HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {

        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ItemBase> Get()
        {
            return DataRepository.Items;
        }

        [HttpGet("GetById/{id}")]
        public ItemBase GetById(int id)
        {
            return new SupportTicket { };
        }

        [HttpPost("AddOrUpdate")]
        public ItemBase AddOrUpdate([FromBody] ItemBase item)
        {
            if(item.Id <= 0)
            {
                DataRepository.AddItem(item);
            } else
            {
                var itemToEdit = DataRepository.Items.FirstOrDefault(i => i.Id == item.Id);
                var index = DataRepository.Items.IndexOf(itemToEdit);
                DataRepository.Items.RemoveAt(index);
                DataRepository.Items.Insert(index, item);
            }

            return item;
        }
    }
}
