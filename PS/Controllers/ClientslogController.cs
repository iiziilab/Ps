using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using PS.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientslogController : ControllerBase
    {
        private readonly GeneralContext _context;
        public ClientslogController(GeneralContext context)
        {
            _context = context;
        }
        // Get api/Clientlog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientCredential>>> GetClients()
        {
            var response = await _context.ClientCredentials.Select(x => new ClientCredential
            {
                ClientId = x.ClientId,
                Id = x.Id,
                Email = x.Email,
                Password = x.Password
            }).ToListAsync();
            return new ObjectResult(response);
        }
        // GET: api/Clientlog/5
        [HttpGet("{id}")]
        private async Task<ActionResult<ClientCredential>> GetbyId(int? id)
        {
            var client = await _context.ClientCredentials.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return new ObjectResult(client);
        }
        // POST: api/Clientslog
        [HttpPost]
        public async Task<ActionResult<ClientCredential>> Post([FromBody] ClientCredential model)
        {
            var response = new ResponseMessage();
            try
            {

                var clientIDExist = await _context.ClientCredentials.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (clientIDExist != null)
                {
                    response.message = "Username already in use, please choose another username";
                    // return response;
                }
                if (!string.IsNullOrEmpty(model.Email))
                {
                    var EmailExist = await _context.ClientCompanies.FirstOrDefaultAsync(x => x.Email == model.Email);
                    if (EmailExist != null)
                    {
                        response.message = "Email address already in use";
                        //return response;
                    }
                }

                ClientCredential client = null;
                if (model.ClientId > 0)
                {
                    client = await _context.ClientCredentials.FindAsync(model.ClientId);
                }
                bool isNew = false;
                if (client == null)
                {
                    isNew = true;
                    client = new ClientCredential();
                }
                client.Email = model.Email;
                client.Password = model.Password;
                if (isNew)
                {
                    await _context.ClientCredentials.AddAsync(client);
                    response.message = "Company account created successfully";
                    response.StatusCode = 200;
                }
                else
                {
                    response.message = "Company account updated successfully";
                    response.StatusCode = 200;
                }
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return new ObjectResult(response);
        }

        // DELETE: api/Clientslog/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            var res = await _context.ClientCredentials.FindAsync(Id);
            if (res != null)
            {
                _context.ClientCredentials.Remove(res);
                await _context.SaveChangesAsync();
                response.message = "Recored deleted successfully";
                response.StatusCode = 200;
            }
            return new ObjectResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientCredential client)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != client.ClientId)
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }

                _context.Entry(client).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = client;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(id))
                    {
                        response.message = "Not Found";
                        response.StatusCode = 404;
                        response.data = new { };
                        return new ObjectResult(response);
                        //return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return new ObjectResult(response);
        }

        private bool ClientExists(int id)
        {
            return _context.ClientCredentials.Any(e => e.Id == id);
        }
    }
}
