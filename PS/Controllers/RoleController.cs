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
    public class RoleController : ControllerBase
    {
        private readonly GeneralContext _context;
        public RoleController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            List<Role> response = null;
            try
            {
                response = await _context.Roles.Select(x => new Role
                {
                    roleId = x.roleId,
                    roleName = x.roleName,
                    Status = x.Status
                }).Where(x=>x.roleName != "superAdmin" && x.roleId != 3 && x.roleId != 5).OrderBy(x=>x.roleId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAlRole()
        {
            List<Role> response = null;
            try
            {
                response = await _context.Roles.Select(x => new Role
                {
                    roleId = x.roleId,
                    roleName = x.roleName,
                    Status = x.Status
                }).OrderBy(x => x.roleId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("Status")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoleByStatus()
        {
            List<Role> response = null;
            try
            {
                response = await _context.Roles.Select(x => new Role
                {
                    roleId = x.roleId,
                    roleName = x.roleName,
                    Status = x.Status
                }).Where(x => x.roleName != "superAdmin" && x.roleId != 3 && x.roleId != 5 && x.Status.statusId == 1).OrderBy(x => x.roleId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("list/{id}")]
        public async Task<ActionResult<Role>> GetRole(string id)
        {
            List<Role> role = new List<Role>();
            try
            {
                List<int> numbers = new List<int>(Array.ConvertAll(id.Split(','), int.Parse));
                role = await _context.Roles.Where(u => numbers.Contains(u.roleId)).ToListAsync();

                if (role == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(role);
        }

        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            Role role = new Role();
            try
            {
                role = await _context.Roles.FindAsync(id);

                if (role == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return role;
        }
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<Role>> Post([FromBody] Role model)
        {
            var response = new ResponseMessage();
            try
            {
                var roleidExist = await _context.Roles.FirstOrDefaultAsync(x => x.roleName == model.roleName);
                if (roleidExist != null)
                {
                    response.message = "Role already exist, please choose another role";
                    response.StatusCode = 200;
                    response.data = new { };
                    return new ObjectResult(response);
                }

                Role role = null;
                if (model.roleId > 0)
                {
                    role = await _context.Roles.FindAsync(model.roleId);
                }
                bool isNew = false;
                if (role == null)
                {
                    isNew = true;
                    role = new Role();
                }
                role.roleName = model.roleName;
                role.statusId = model.statusId;
                if (isNew)
                {
                    await _context.Roles.AddAsync(role);
                    response.message = "Role added successfully";
                    response.StatusCode = 200;
                    response.data = role;
                }
                else
                {
                    response.message = "Role updated successfully";
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
                var res = await _context.Roles.FindAsync(Id);
                if (res != null)
                {
                    _context.Roles.Remove(res);
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
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != role.roleId)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(role).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = role;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(id))
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

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.roleId == id);
        }
    }
}
