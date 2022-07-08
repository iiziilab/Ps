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
    public class ModuleController : ControllerBase
    {
        private readonly GeneralContext _context;
        public ModuleController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModulePermission>>> GetModule()
        
        {
            List<ModulePermission> response = null;
            try
            {
                response = await _context.ModulePermissions.Select(x => new ModulePermission
                {
                    modulepermissionId = x.modulepermissionId,
                    ModuleName = x.ModuleName,
                    Status = x.Status
                }).OrderBy(x => x.modulepermissionId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ModulePermission>> GetModule(int id)
        {
            var module = new ModulePermission();
            try
            {
                module = await _context.ModulePermissions.FindAsync(id);
               
                if (module == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return module;
        }
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpPost]
        public async Task<ActionResult<ModulePermission>> Post([FromBody] ModulePermission model)
        {
            var response = new ResponseMessage();
            try
            {
                var Exist = await _context.ModulePermissions.FirstOrDefaultAsync(x => x.ModuleName == model.ModuleName);
                if (Exist != null)
                {
                    response.message = "Module already exist, please choose another one";
                    response.StatusCode = 200;
                    response.data = new { };
                    return new ObjectResult(response);
                }

                ModulePermission module = null;
                if (model.modulepermissionId > 0)
                {
                    module = await _context.ModulePermissions.FindAsync(model.modulepermissionId);
                }
                bool isNew = false;
                if (module == null)
                {
                    isNew = true;
                    module = new ModulePermission();
                }
                module.ModuleName = model.ModuleName;
                module.statusId = model.statusId;
                if (isNew)
                {
                    await _context.ModulePermissions.AddAsync(module);
                    response.message = "Module added successfully";
                    response.StatusCode = 200;
                    response.data = module;
                }
                else
                {
                    response.message = "Module updated successfully";
                    response.StatusCode = 200;
                    response.data = module;
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
                var res = await _context.ModulePermissions.FindAsync(Id);
                if (res != null)
                {
                    _context.ModulePermissions.Remove(res);
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
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModulePermission module)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != module.modulepermissionId)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(module).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = module;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(id))
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

        private bool ModuleExists(int id)
        {
            return _context.ModulePermissions.Any(e => e.modulepermissionId == id);
        }
    }
}
