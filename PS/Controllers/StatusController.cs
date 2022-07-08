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
    public class StatusController : ControllerBase
    {
        private readonly GeneralContext _context;
        public StatusController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Status
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            List<Status> response = null;
            try
            {
                response = await _context.Statuses.Select(x => new Status
                {
                    statusId = x.statusId,
                    statusName = x.statusName
                }).OrderBy(x=>x.statusId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Status/5
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            Status Status = new Status();
            try
            {
                Status = await _context.Statuses.FindAsync(id);
               
                if (Status == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return Status;
        }
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpPost]
        public async Task<ActionResult<Status>> Post([FromBody] Status model)
        {
            var response = new ResponseMessage();
            try
            {
                var StatusidExist = await _context.Statuses.FirstOrDefaultAsync(x => x.statusName == model.statusName);
                if (StatusidExist != null)
                {
                    response.message = "Status already exist, please choose another Status";
                    response.StatusCode = 200;
                    response.data = new { };
                    return new ObjectResult(response);
                }

                Status Status = null;
                if (model.statusId > 0)
                {
                    Status = await _context.Statuses.FindAsync(model.statusId);
                }
                bool isNew = false;
                if (Status == null)
                {
                    isNew = true;
                    Status = new Status();
                }
                Status.statusName = model.statusName;
     
                if (isNew)
                {
                    await _context.Statuses.AddAsync(Status);
                    response.message = "Status added successfully";
                    response.StatusCode = 200;
                    response.data = Status;
                }
                else
                {
                    response.message = "Status updated successfully";
                    response.StatusCode = 200;
                    response.data = Status;
                }
                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                response.message = ex.Message;
                response.data = new { };
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }

        // DELETE: api/Status/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Statuses.FindAsync(Id);
                if (res != null)
                {
                    _context.Statuses.Remove(res);
                    await _context.SaveChangesAsync();
                    response.message = "Recored deleted successfully";
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                response.message = "Something went Wrong";
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }
        //[Authorize(Roles = "superAdmin,admin,client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status Status)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != Status.statusId)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(Status).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = Status;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(id))
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

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.statusId == id);
        }
    }
}
