
using Library.HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [HttpPost("AddOrUpdate")]
        public async Task<ItemBase> AddOrUpdate([FromBody] ItemBase item)
        {
            DataRepository.Items.Add(item);
            return item;
        }
    }
}
