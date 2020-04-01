using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _service;

        public ItemsController(IItemsService service)
        {
            _service = service;
        }

        // GET api/items
        public ActionResult<List<Item>> Get()
        {
            var items = _service.All();
            return Ok(items);
        }
    }
}