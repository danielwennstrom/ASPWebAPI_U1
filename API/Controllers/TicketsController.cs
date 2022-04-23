using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/Tickets/5
        [HttpGet]
        public async Task<ActionResult<List<Ticket>>> Get([FromQuery] string customerId, string created, string status)
        {
            List<Ticket> query = new();

            if (!String.IsNullOrEmpty(customerId))
            {
                query = await _context.Tickets.Where(t => t.CustomerId.ToString() == customerId).ToListAsync();
                return query;
            }
            else if (!String.IsNullOrEmpty(created))
            {
                query = await _context.Tickets.Where(t => t.Created.ToShortDateString() == created).ToListAsync();
                return query;
            }
            else if (!String.IsNullOrEmpty(status))
            {
                query = await _context.Tickets.Where(t => t.Status == status).ToListAsync();
                return query;
            }
            else
            {
                return await _context.Tickets.ToListAsync();
            }
        }
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateTicket([FromBody]Ticket ticket)
        {
            ticket.CustomerId = Guid.NewGuid();
            ticket.Created = DateTime.Now;
            ticket.LastUpdated = DateTime.Now;

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
        [HttpPut]
        public async Task<ActionResult<Ticket>> UpdateTicket([FromBody] Ticket ticket)
        {
            ticket.LastUpdated = DateTime.Now;
            var query = await _context.Tickets.FindAsync(ticket.Id);

            _context.Entry(query).CurrentValues.SetValues(ticket);
            await _context.SaveChangesAsync();

            return Ok(query);
        }
    }
}
