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
    public class RolePermissionController : ControllerBase
    {
        private readonly GeneralContext _context;
        public RolePermissionController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePermission>>> GetRolePermission()
        {
            List<RolePermission> response = null;
            try
            { 
                response = await _context.RolePermissions.OrderBy(x => x.rolepermissionId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RolePermission>> GetRolePermission(int id)
        {
            var RolePermission = new RolePermission();
            try
            {
                RolePermission = await _context.RolePermissions.FindAsync(id);
               
                if (RolePermission == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return RolePermission;
        }
        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("role/{id}")]
        public async Task<ActionResult<RolePermission>> GetRolePermissionByRole(int id)
        {
            var RolePermission = new RolePermission();
            try
            {
                RolePermission = await _context.RolePermissions.SingleOrDefaultAsync(x => x.roleId == id);

                if (RolePermission == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return RolePermission;
        }
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<RolePermission>> Post([FromBody] RolePermission model)
        {
            var response = new ResponseMessage();
            try
            {
                RolePermission role = null;
                if (model.rolepermissionId > 0)
                {
                    role = await _context.RolePermissions.SingleOrDefaultAsync(x=>x.roleId == model.roleId);
                }
                bool isNew = false;
                if (role == null)
                {
                    isNew = true;
                    role = new RolePermission();
                }
                role.ClientDelete = model.ClientDelete;
                role.ClientDetails = model.ClientDetails;
                role.ClientInsert = model.ClientInsert;
                role.ClientList = model.ClientList;
                role.ClientUpdate = model.ClientUpdate;
                role.ProjectInsert = model.ProjectInsert;
                role.ProjectUpdate = model.ProjectUpdate;
                role.ProjectDelete = model.ProjectDelete;
                role.ProjectDetails = model.ProjectDetails;
                role.ProjectList = model.ProjectList;
                role.UserInsert = model.UserInsert;
                role.UserUpdate = model.UserUpdate;
                role.UserDelete = model.UserDelete;
                role.UserDetails = model.UserDetails;
                role.UserList = model.UserList;
                role.RoleInsert = model.RoleInsert;
                role.RoleUpdate = model.RoleUpdate;
                role.RoleDelete = model.RoleDelete;
                role.RoleDetails = model.RoleDetails;
                role.RoleList = model.RoleList;
                role.PermissionInsert = model.PermissionInsert;
                role.PermissionUpdate = model.PermissionUpdate;
                role.PermissionDelete = model.PermissionDelete;
                role.PermissionDetails = model.PermissionDetails;
                role.PermissionList = model.PermissionList;
                role.CellList = model.CellList;
                role.Menu = model.Menu;
                role.Upload = model.Upload;

                role.roleId = model.roleId;
                if (isNew)
                {
                    await _context.RolePermissions.AddAsync(role);
                    response.message = "Role Permission added successfully";
                    response.StatusCode = 200;
                    response.data = role;
                }
                else
                {
                    _context.Entry(role).State = EntityState.Modified;
                    response.message = "Role Permission updated successfully";
                    response.StatusCode = 200;
                    response.data = role;
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

        // DELETE: api/Role/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.RolePermissions.FindAsync(Id);
                if (res != null)
                {
                    _context.RolePermissions.Remove(res);
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
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolePermission(int id, RolePermission user)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != user.rolepermissionId)//changed clientid to employeeid
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
                    if (!RolePermissionExists(id))
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

        private bool RolePermissionExists(int id)
        {
            return _context.RolePermissions.Any(e => e.rolepermissionId == id);
        }
    }
}
