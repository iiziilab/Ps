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
    public class MenuController : ControllerBase
    {
        private readonly GeneralContext _context;
        public MenuController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
            List<Menu> response = null;
            try
            {
                response = await _context.Menus.Select(x => new Menu
                {
                    ID = x.ID,
                    Consideration = x.Consideration,
                    //Item = x.Item,
                    CategoryIndex = x.CategoryIndex,
                    Include = x.Include,
                    Category = x.Category,
                    ShortDescription = x.ShortDescription,
                    projectId = x.projectId,
                    cellId = x.cellId,
                    isExpanded = x.isExpanded
                }).OrderBy(x => x.ID).ToListAsync();
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
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            Menu role = new Menu();
            try
            {
                role = await _context.Menus.FindAsync(id);

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
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet("cid/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenuByCID(int id)
        {
            List<Menu> response = null;
            try
            {
                response = await _context.Menus.Select(x => new Menu
                {
                    ID = x.ID,
                    Consideration = x.Consideration,
                    //Item = x.Item,
                    CategoryIndex = x.CategoryIndex,
                    Include = x.Include,
                    Category = x.Category,
                    ShortDescription = x.ShortDescription,
                    projectId = x.projectId,
                    cellId = x.cellId,
                    isExpanded = x.isExpanded
                }).Where(x => x.cellId == id).OrderBy(x => x.ID).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        [HttpGet("excelcid/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenuByExcelCID(int id)
        {
            List<Menu> response = null;
            var response1 = new ResponseMessage();
            try
            {
                //response = await _context.Menus.Select(x => new Menu
                //{
                //    ID = x.ID,
                //    Consolidation = x.Consolidation,
                //    CategoryIndex = x.CategoryIndex,
                //    Include = x.Include,
                //    Category = x.Category,
                //    Description = x.Description,
                //    cellId = x.cellId
                //}).Where(x => x.cellId == id).OrderBy(x => x.ID).ToListAsync();

                var res = await _context.Menus.Where(x => x.cellId == id)
                .Select(n => new
                {
                    n.ID,
                    n.Consideration,
                    n.CategoryIndex,
                    n.Include,
                    n.Category,
                    n.ShortDescription
                }).OrderBy(x => x.ID).ToListAsync();


                //var result = (from a in _context.Menus
                //              where a.cellId == id
                //              select new {
                //                  a.ID,
                //                  a.Consolidation,
                //                  a.CategoryIndex,
                //                  a.Include,
                //                  a.Category,
                //                  a.Description
                //              }).ToList();
                response1.message = "";
                response1.StatusCode = 200;
                response1.data = res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response1);
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet("pid/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenuByPID(int id)
        {
            List<Menu> response = null;
            try
            {
                response = await _context.Menus.Select(x => new Menu
                {
                    ID = x.ID,
                    Consideration = x.Consideration,
                    //Item = x.Item,
                    CategoryIndex = x.CategoryIndex,
                    Include = x.Include,
                    Category = x.Category,
                    ShortDescription = x.ShortDescription,
                    projectId = x.projectId,
                    cellId = x.cellId,
                    isExpanded = x.isExpanded
                }).Where(x=>x.projectId == id).OrderBy(x => x.ID).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        [HttpGet("excelpid/{id}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenuByExcelPID(int id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Menus.Where(x => x.projectId == id).OrderBy(x => x.cellId)
                .Select(n => new
                {
                    n.ID,
                    n.Consideration,
                    n.CategoryIndex,
                    n.Include,
                    n.Category,
                    n.ShortDescription
                }).ToListAsync();

                response.message = "";
                response.StatusCode = 200;
                response.data = res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        //[Authorize(Roles = "superAdmin,admin")]
        [HttpPost("{id}")]
        public async Task<ActionResult<Menu>> Post(string id,[FromBody] Menu []model)
        {
            var response = new ResponseMessage();
            try
            {
                foreach(var m in model)
                {
                    Menu menu = null;
                    bool isNew = false;
                    if (menu == null)
                    {
                        isNew = true;
                        menu = new Menu();
                    }
                    menu.Consideration = m.Consideration;
                    menu.CategoryIndex = m.CategoryIndex;
                    menu.Include = m.Include;
                    menu.Category = m.Category;
                    menu.ShortDescription = m.ShortDescription;
                    menu.LongDescription = m.LongDescription;
                    menu.projectId = Convert.ToInt32(id.Split('&')[0]);
                    menu.cellId = Convert.ToInt32(id.Split('&')[1]);
                    if (isNew)
                    {
                        await _context.Menus.AddAsync(menu);
                        response.message = "Menu added successfully";
                        response.StatusCode = 200;
                        response.data = menu;
                    }
                    else
                    {
                        response.message = "Menu updated successfully";
                        response.StatusCode = 200;
                        response.data = menu;
                    }
                    await _context.SaveChangesAsync();
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

        // DELETE: api/Role/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Menus.FindAsync(Id);
                if (res != null)
                {
                    _context.Menus.Remove(res);
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
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != menu.ID)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(menu).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = menu;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(id))
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

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.ID == id);
        }
    }
}
