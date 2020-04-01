using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SampleShop.Interfaces;
using SampleShop.Model;

namespace SampleShop.Controllers
{
    // TODO
    // The action methods that use the new orders service methods have to be implemented. 
    // The desired URIs are specificed over each of the action methods. */
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService service;

        public OrdersController(IOrdersService service)
        {
            this.service = service;
        }

        // GET api/orders
        public ActionResult<List<Order>> Get()
        {
            var items = service.All();
            return Ok(items);
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public ActionResult<List<Order>> Get(int id)
        {
            var items = service.GetById(id);
            return Ok(items);
        }

        // GET api/orders/dates/?start=2018-01-03&end=2018-02-03
        [HttpGet("dates")]
        public ActionResult<List<Order>> Get(DateTime start, DateTime end)
        {
            var orders = service.GetByDates(start, end);

            return Ok(orders);
        }

        // GET api/orders/items/?day=2018-01-15
        [HttpGet("items")]
        public ActionResult<List<Order>> Get(DateTime day)
        {
            var items = service.GetItemsSoldByDay(day);

            return Ok(items);
        }

        // POST api/items
        [HttpPost]
        public ActionResult Add([FromBody] Order value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = service.Add(value);
            if (order != null)
            {
                return CreatedAtAction("Get", new { id = order.Id }, order);
            }

            return BadRequest("Failed Validation");
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            service.Delete(id);

            return Ok();
        }
    }
}