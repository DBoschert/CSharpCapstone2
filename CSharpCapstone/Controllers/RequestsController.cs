using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharpCapstone.Data;
using CSharpCapstone.Models;

namespace CSharpCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly CSharpCapstoneContext _context;

        public RequestsController(CSharpCapstoneContext context)
        {
            _context = context;
        }

        //GET: /api/requests/reviews/{userId}
        
        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviewStatus(int userId)
        {
            var req = await _context.Requests.Where(x => x.Status == "Review" && x.Id != userId).ToListAsync();
            if (req == null)
            {
                return NotFound();
            }
            return req;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            return await _context.Requests.Include(x => x.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            var request = await _context.Requests.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //PUT: /api/requests/review/5
        [HttpPut("review/{id}")]
        public async Task<IActionResult> SetRequestStatusGreater(int id, Request request)
        {
            if(request.Total <= 50)
            {
                request.Status = "APPROVED";
                return await PutRequest(id, request);
            }

            request.Status = "REVIEW";
            return await PutRequest(id, request);

        }

        //PUT: /api/requests/approve/5
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> SetRequestStatusApproved(int id, Request request)
        {
            request.Status = "APPROVED";
            return await PutRequest(id, request);
        }

        //PUT: /api/requests/reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> SetRequestStatusRejected(int id, Request request)
        {
            request.Status = "REJECTED";
            return await PutRequest(id, request);
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
          if (_context.Requests == null)
          {
              return Problem("Entity set 'CSharpCapstoneContext.Request'  is null.");
          }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
