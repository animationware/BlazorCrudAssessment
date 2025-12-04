using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderRegistry.Domain.Entities;
using OrderRegistry.Infrastructure.Data;

namespace OrderRegistry.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderRegistryDbContext _context;

        public OrderController(OrderRegistryDbContext context)
        {
            _context = context;
        }

        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Order updatedOrder)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Name = updatedOrder.Name;
            order.Date = updatedOrder.Date;
            order.Price = updatedOrder.Price;
            order.Description = updatedOrder.Description;
            order.State = updatedOrder.State;

            await _context.SaveChangesAsync();
            return Ok(order);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
