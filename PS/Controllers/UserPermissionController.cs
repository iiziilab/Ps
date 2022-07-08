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
    public class UserPermissionController : ControllerBase
    {
        private readonly GeneralContext _context;
        public UserPermissionController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        [Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermission>>> GetUserPermission()
        {
            List<UserPermission> response = null;
            try
            {
                response = await _context.UserPermissions.Where(x=>x.Role== "superAdmin").ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role/5
        [Authorize(Roles = "superAdmin,admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPermission>> GetUserPermission(int id)
        {
            var userPermission = new UserPermission();
            try
            {
                userPermission = await _context.UserPermissions.FindAsync(id);
               
                if (userPermission == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return userPermission;
        }
        [Authorize(Roles = "superAdmin,admin")]
        [HttpPost]
        public async Task<ActionResult<UserPermission>> Post([FromBody] UserPermission model)
        {
            var response = new ResponseMessage();
            try
            {
                var userPermissionExist = await _context.UserPermissions.FirstOrDefaultAsync(x => x.Email == model.Email && x.Role == model.Role);
                UserPermission userPermission = new UserPermission();
                userPermission.Email = model.Email;
                userPermission.Role = model.Role;
                await _context.UserPermissions.AddAsync(userPermission);
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

        // DELETE: api/Role/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.UserPermissions.FindAsync(Id);
                if (res != null)
                {
                    _context.UserPermissions.Remove(res);
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
        [Authorize(Roles = "superAdmin,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPermission(int id, UserPermission user)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != user.userpermissionId)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = user;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPermissionExists(id))
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

        private bool UserPermissionExists(int id)
        {
            return _context.UserPermissions.Any(e => e.userpermissionId == id);
        }
    }
}
