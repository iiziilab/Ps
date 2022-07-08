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
using PS.Model;

namespace PS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly GeneralContext _context;
        //private readonly FormAccess _access;
        public ProjectController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Project
        //[Authorize(Roles = "superAdmin,admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDetails>>> GetProjects()
        {
            List<Project> data = null;
            List<ProjectDetails> response = new List<ProjectDetails>();
            try
            {
               //if(_access.dataaccess())
               //var result = (from p in _context.Projects
               //             join c in _context.Cells on p.Id equals c.projectId
               //             orderby c.projectId
               //             select new ProjectDetails
               //             {
               //                 Id = p.Id,
               //                 ProjectName = p.ProjectName,
               //                 ProjectNo = p.ProjectNo,
               //                 ClientId = p.ClientId,
               //                 EmployeeId = p.EmployeeId,
               //                 Description = p.Description,
               //                 StartDate = p.StartDate,
               //                 EndDate = p.EndDate,
               //                 DataType = p.DataType,
               //                 Status = p.Status,
               //                 statusId = p.statusId,
               //                 cellId = c.cellId,
               //             }).Distinct();
               // foreach(var x in result)
               // {
               // }


                data = await _context.Projects.Select(x => new Project
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectNo = x.ProjectNo,
                    ClientId = x.ClientId,
                    EmployeeId = x.EmployeeId,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    DataType = x.DataType,
                    Status = x.Status,
                    statusId = x.statusId,
                    Created = x.Created
                }).OrderBy(x => x.Id).ToListAsync();
                foreach(var x in data)
                {
                    long cellid = 0;
                    var cell = _context.Cells.Where(a => a.projectId == x.Id && a.cellName != "Notes").FirstOrDefault();
                    if(cell != null)
                    {
                        cellid = cell.cellId;
                    }
                    var model = new ProjectDetails 
                    {
                        Id = x.Id,
                        ProjectName = x.ProjectName,
                        ProjectNo = x.ProjectNo,
                        ClientId = x.ClientId,
                        EmployeeId = x.EmployeeId,
                        Description = x.Description,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        DataType = x.DataType,
                        Status = x.Status,
                        statusId = x.statusId,
                        //Created = x.Created
                        cellId = cellid,
                    };
                    response.Add(model);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        [HttpGet("ByDate")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByDate()
        {
            List<Project> response = null;
            try
            {
                response = await _context.Projects.Select(x => new Project
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectNo = x.ProjectNo,
                    ClientId = x.ClientId,
                    EmployeeId = x.EmployeeId,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    DataType = x.DataType,
                    Status = x.Status,
                    statusId = x.statusId,
                    Created = x.Created
                }).Where(x=> DateTime.Compare(x.Created.Date, DateTime.Now.Date) == 0).OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        //Get: api/Project/id
        // [Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = new Project();
            try
            {
                project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return project;
        }
        //Get: api/Project/id
        //[Authorize(Roles = "superAdmin,admin,client")]
        [HttpGet("client/{id}")]
        public async Task<ActionResult<Project>> GetProjectByCid(long id)
        {
            List<Project> response = null;
            try
            {
                //long[] nums = Array.ConvertAll(id.Split(','), long.Parse);
                response = await _context.Projects.Select(x => new Project
                {
                    Id = x.Id,
                    ClientId = x.ClientId,
                    ProjectName = x.ProjectName,
                    ProjectNo = x.ProjectNo,
                    EmployeeId = x.EmployeeId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    DataType = x.DataType,
                    Status = x.Status,
                    statusId = x.statusId,
                    Created = x.Created
                }).Where(p => p.ClientId == id).OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        //Get: api/Project/id
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<Project>> GetProjectByEid(string id)
        {
            List<Project> response = null;
            try
            {
                long[] nums = Array.ConvertAll(id.Split(','), long.Parse);
                response = await _context.Projects.Select(x => new Project
                {
                    Id = x.Id,
                    ClientId = x.ClientId,
                    ProjectName = x.ProjectName,
                    ProjectNo = x.ProjectNo,
                    EmployeeId = x.EmployeeId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.Description,
                    DataType = x.DataType,
                    Status = x.Status,
                    statusId = x.statusId,
                    Created = x.Created
                }).Where(p => p.EmployeeId.Contains(nums[0])).OrderBy(x=>x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        // Post: api/projects 
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<Project>> Post([FromBody] Project project)
        {
            var response = new ResponseMessage();
            try
            {
                Project project1 = null;
                if (project.Id > 0)
                {
                    project1 = await _context.Projects.FindAsync(project.Id);
                }
                bool isNew = false;
                if (project1 == null)
                {
                    isNew = true;
                    project1 = new Project();
                }
                project1.ProjectName = project.ProjectName;
                project1.ProjectNo = project.ProjectNo;
                project1.ClientId = project.ClientId;
                project1.EmployeeId = project.EmployeeId;
                project1.Description = project.Description;
                project1.StartDate = project.StartDate;
                project1.EndDate = project.EndDate;
                project1.DataType = project.DataType;
                project1.statusId = project.statusId;
                project1.Created = DateTime.Now;
                if (isNew)
                {
                    await _context.Projects.AddAsync(project1);
                    response.message = "Project added successfully";
                    response.StatusCode = 200;
                    response.data = project1;
                }
                else
                {
                    _context.Entry(project1).State = EntityState.Modified;
                    response.message = "Project updated successfully";
                    response.StatusCode = 200;
                    response.data = project1;
                }
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                response.message = ex.Message;
                response.data = new { };
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }
        // DELETE: api/Project/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Projects.FindAsync(Id);
                if (res != null)
                {
                    _context.Projects.Remove(res);
                    await _context.SaveChangesAsync();
                    response.message = "Project deleted successfully";
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
    }
}
