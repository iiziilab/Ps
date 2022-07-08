using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ClientController : ControllerBase
    {
        private readonly GeneralContext _context;
        public ClientController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Clients
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientCompany>>> GetClients()
        {
            List<ClientCompany> response = null;
            try
            {
                response = await _context.ClientCompanies.Select(x => new ClientCompany
                {
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    State = x.State,
                    Country = x.Country,
                    Pincode = x.Pincode,
                    Contact1 = x.Contact1,
                    Contact2 = x.Contact2,
                    Description = x.Description,
                    Created = x.Created
                }).OrderBy(x => x.ClientId).ToListAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Client/5
        //[Authorize(Roles = "superAdmin,admin,client,user,employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCompany>> GetClient(long id)
        {
            ClientCompany user = new ClientCompany();
            try
            {
                user = await _context.ClientCompanies.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return user;
        }

        // GET: api/Client/5
        //[Authorize(Roles = "superAdmin,admin,client,user,employee")]
        [HttpGet("list/{id}")]
        public async Task<ActionResult<ClientCompany>> GetClientList(string id)
        {
            List<ClientCompany> response = null;
            try
            {
                response = await _context.ClientCompanies.Select(x => new ClientCompany
                {
                    ClientId = x.ClientId,
                    Name = x.Name
                }).Where(p => id.Contains(p.ClientId.ToString())).OrderBy(x => x.ClientId).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<ClientCompany>> Post([FromBody] ClientCompany model)
        {
            var response = new ResponseMessage();
            try
            {
                //var clientIDExist =await _context.ClientCompanies.FirstOrDefaultAsync(x => x.Email == model.Email);
                //if (clientIDExist != null)
                //{
                //    response.message = "Username already in use, please choose another username";
                //    response.StatusCode = 1;
                //    response.data = new { };
                //    return new ObjectResult(response);
                //}

                //if (!string.IsNullOrEmpty(model.Email))
                //{
                //    var EmailExist =await _context.ClientCompanies.FirstOrDefaultAsync(x => x.Email == model.Email);
                //    if (EmailExist != null)
                //    {
                //        response.message = "Email address already in use";
                //        response.StatusCode = 2;
                //        response.data = new { };
                //        return new ObjectResult(response);
                //    }
                //}

                ClientCompany client = null;
                if (model.ClientId > 0)
                {
                    client =await  _context.ClientCompanies.FindAsync(model.ClientId);
                }
                bool isNew = false;
                if (client == null)
                {
                    isNew = true;
                    client = new ClientCompany();
                }
                client.Name = model.Name;
                client.Email = model.Email;
                client.Address = model.Address;
                client.State = model.State;
                client.Country = model.Country;
                client.Pincode = model.Pincode;
                client.Contact1 = model.Contact1;
                client.Contact2 = model.Contact2;
                client.Description = model.Description;
                client.Created = DateTime.Now;
                if (isNew)
                {
                    await _context.ClientCompanies.AddAsync(client);
                    await _context.SaveChangesAsync();
                    response.message = "Company account created successfully";
                    response.StatusCode = 200;
                    response.data = client;
                }
                else
                {
                    await _context.SaveChangesAsync();
                    response.message = "Company account updated successfully";
                    response.StatusCode = 200;
                    response.data = client;
                }
            }

            catch (Exception ex)
            {
                response.message = ex.Message;
                response.data = new { };
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }

        // DELETE: api/Clients/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var response = new ResponseMessage();
            try
            {
                var result = await _context.Projects.Where(x => x.ClientId == Id).FirstOrDefaultAsync();
                if(result == null)
                {
                    var res = await _context.ClientCompanies.FindAsync(Id);
                    if (res != null)
                    {
                        _context.ClientCompanies.Remove(res);
                        await _context.SaveChangesAsync();
                        response.message = "Recored deleted successfully";
                        response.StatusCode = 200;
                    }
                }
                else
                {
                    response.message = "Client already assigned to project";
                    response.StatusCode = 201;
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                response.message = "Something went Wrong";
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientCompany client)
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
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return new ObjectResult(response);
        }

        private bool ClientExists(int id)
        {
            return _context.ClientCompanies.Any(e => e.ClientId == id);
        }
    }
}
