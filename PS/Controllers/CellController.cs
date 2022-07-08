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
    public class CellController : ControllerBase
    {
        private readonly GeneralContext _context;
        public CellController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Cell
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cell>>> GetCell()
        {
            List<Cell> response = null;
            try
            {
                response = await _context.Cells.Select(x => new Cell
                {
                    projectId = x.projectId,
                    cellId = x.cellId,
                    cellName = x.cellName
                }).OrderBy(x=>x.cellId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Cell/5
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Cell>> GetCell(long id)
        {
            Cell Cell = new Cell();
            try
            {
                Cell = await _context.Cells.FindAsync(id);
                if (Cell == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return Cell;
        }
        //GET : api/Cell/project/5
        [HttpGet("project/{id}")]
        public async Task<ActionResult<Cell>> GetCellByPID(int id)
        {
            List<Cell> response = null;
            try
            {
                response = await _context.Cells.Select(x => new Cell
                {
                    projectId = x.projectId,
                    cellId = x.cellId,
                    cellName = x.cellName
                }).Where(x => x.projectId == id).OrderBy(x => x.cellId).ToListAsync();

                if (response == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<Cell>> Post([FromBody] Cell model)
        {
            var response = new ResponseMessage();
            try
            {
                Cell Cell = null;
                if (model.cellId > 0)
                {
                    Cell = await _context.Cells.FindAsync(model.cellId);
                }
                bool isNew = false;
                if (Cell == null)
                {
                    isNew = true;
                    Cell = new Cell();
                }
                Cell.projectId = model.projectId;
                Cell.cellName = model.cellName;
     
                if (isNew)
                {
                    await _context.Cells.AddAsync(Cell);
                    response.message = "Cell added successfully";
                    response.StatusCode = 200;
                    response.data = Cell;
                }
                else
                {
                    _context.Entry(Cell).State = EntityState.Modified;
                    response.message = "Cell updated successfully";
                    response.StatusCode = 200;
                    response.data = Cell;
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

        // DELETE: api/Cell/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Cells.FindAsync(Id);
                if (res != null)
                {
                    _context.Cells.Remove(res);
                    var menu = await _context.Menus.Where(x => x.cellId == Id).ToListAsync();
                    if(menu != null)
                    {
                        foreach (var p in menu)
                        {
                            _context.Menus.Remove(p);
                        }
                    }
                    await _context.SaveChangesAsync();
                    response.message = "Recored deleted successfully";
                    response.StatusCode = 200;

                    UploadCSV cvc = await _context.UploadCSVs.FirstAsync(x => x.cellId == Id);
                    if(cvc != null)
                    {
                        _context.UploadCSVs.Remove(cvc);
                        await _context.SaveChangesAsync();
                    }
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
        public async Task<IActionResult> PutCell(int id, Cell Cell)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != Cell.cellId)
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                _context.Entry(Cell).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = Cell;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CellExists(id))
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

        private bool CellExists(int id)
        {
            return _context.Cells.Any(e => e.cellId == id);
        }
    }
}
