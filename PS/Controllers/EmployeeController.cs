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
    public class EmployeeController : ControllerBase
    {
        private readonly GeneralContext _context;
        
        public EmployeeController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Employee
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            List<Employee> response = null;
            try
            {
                response = await _context.Employees.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryption.Decrypt(x.Password),
                    ContactNo = x.ContactNo,
                    ClientCompany = x.ClientCompany,
                    Role = x.Role,
                    ChangePassword = x.ChangePassword,
                    EditProject = x.EditProject,
                    ViewProject = x.ViewProject,
                    Created = x.Created
                }).OrderBy(x => x.EmployeeId).ToListAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        [HttpGet("ByDate")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByDate()
        {
            List<Employee> response = null;
            try
            {
                response = await _context.Employees.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryption.Decrypt(x.Password),
                    ContactNo = x.ContactNo,
                    ClientCompany = x.ClientCompany,
                    Role = x.Role,
                    ChangePassword = x.ChangePassword,
                    EditProject = x.EditProject,
                    ViewProject = x.ViewProject,
                    Created = x.Created
                }).Where(x=> DateTime.Compare(x.Created.Date, DateTime.Now.Date) == 0).OrderBy(x => x.EmployeeId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Employee/5
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            Employee employee =new Employee();
            try
            {
                employee = await _context.Employees.FindAsync(id);
                
                if (employee == null)
                {
                    return NotFound();
                }
                employee.Password = Encryption.Decrypt(employee.Password);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return employee;
        }

        // GET: api/Employee/getemployee/5
        //[Authorize(Roles = "superAdmin,admin,client,user,employee")]
        [HttpGet("getemployee/{clientid}")]
        public async Task<ActionResult<Employee>> GetEmployeebyId(long clientid)
        {
            List<Employee> response = null;
            try
            {
                response = await _context.Employees.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryption.Decrypt(x.Password),
                    ContactNo = x.ContactNo,
                    ClientCompany = x.ClientCompany,
                    Role = x.Role,
                    ChangePassword = x.ChangePassword,
                    EditProject = x.EditProject,
                    ViewProject = x.ViewProject,
                    Created = x.Created
                }).Where(p=>p.ClientId == clientid).OrderBy(x => x.EmployeeId).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        // GET: api/Employee/getemployee/5
        //[Authorize(Roles = "superAdmin,admin,client,user,employee")]
        [HttpGet("getemployeelist/{clientid}")]
        public async Task<ActionResult<Employee>> GetEmployeeListbyId(int clientid)
        {
            List<Employee> response = null;
            try
            {
                response = await _context.Employees.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryption.Decrypt(x.Password),
                    ContactNo = x.ContactNo,
                    ClientCompany = x.ClientCompany,
                    Role = x.Role,
                    ChangePassword = x.ChangePassword,
                    EditProject = x.EditProject,
                    ViewProject = x.ViewProject,
                    Created = x.Created
                }).Where(p => p.ClientId == clientid).OrderBy(x => x.EmployeeId).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Employee/getemployeebyid/5
        //[Authorize(Roles = "superAdmin,admin,client,user,employee")]
        [HttpGet("getemployeelistbyeid/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeListbyEId(string id)
        {
            List<Employee> response = null;
            try
            {
                response = await _context.Employees.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    Password = Encryption.Decrypt(x.Password),
                    ContactNo = x.ContactNo,
                    ClientCompany = x.ClientCompany,
                    Role = x.Role,
                    ChangePassword = x.ChangePassword,
                    EditProject = x.EditProject,
                    ViewProject = x.ViewProject,
                    Created = x.Created
                }).Where(p => id.Contains(p.EmployeeId.ToString())).OrderBy(x => x.EmployeeId).Distinct().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }
        [HttpPost("UpdatePermission")]
        public async Task<IActionResult> UpdateEmployeePermission([FromBody] UpdatePermission model)
        {
            var response = new ResponseMessage();
            try
            {
                var emp = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == model.id);
                if(emp != null)
                {
                    emp.ChangePassword = model.ChangePassword;
                    emp.EditProject = model.EditProject;
                    emp.ViewProject = model.ViewProject;
                    _context.Entry(emp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = emp;
                }
            }
            catch(Exception ex)
            {

            }
            return new ObjectResult(response);
        }


        [HttpPost("UpdateEmp")]
        public async Task<IActionResult> UpdateEmp([FromBody] UpdateEmp model)
        {
            var response = new ResponseMessage();
            try
            {
                var emp = await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == model.id);
                if (emp != null)
                {
                    emp.Name = model.Name;
                    emp.Email = model.Email;
                    emp.Password = Encryption.Encrypt(model.Password);
                    emp.ContactNo = model.ContactNo;
                    _context.Entry(emp).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = emp;
                }
            }
            catch (Exception ex)
            {

            }
            return new ObjectResult(response);
        }

        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPost]
        public async Task<ActionResult<Employee>> Post([FromBody] Employee model)
        {
            var response = new ResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(model.Email))
                {
                    var empidExist = await _context.Employees.FirstOrDefaultAsync(x => x.Email == model.Email);
                    if (empidExist != null)
                    {
                        response.message = "This email "+ model.Email +" already taken, please choose another";
                        response.StatusCode = 201;
                        response.data = new { };
                        return new ObjectResult(response);
                    }
                }

                Employee employee = null;
                if (model.EmployeeId > 0)
                {
                    employee = await  _context.Employees.FindAsync(model.EmployeeId);
                }
                bool isNew = false;
                if (employee == null)
                {
                    isNew = true;
                    employee = new Employee();
                }
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Password = Encryption.Encrypt(model.Password);
                employee.ContactNo = model.ContactNo;
                employee.ClientId = model.ClientId;
                employee.EditProject = model.EditProject;
                employee.ViewProject = model.ViewProject;
                employee.ChangePassword = model.ChangePassword;
                employee.Created = DateTime.Now;
                employee.roleid = 5;
                if (isNew)
                {
                    await _context.Employees.AddAsync(employee);
                    response.message = "Employee added successfully";
                    response.StatusCode = 200;
                    response.data = employee;
                }
                else
                {
                    response.message = "Employee updated successfully";
                    response.StatusCode = 200;
                    response.data = employee;
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
        //Delete : api/Employee/delete/5
        [HttpPost("delete/{cid}")]
        public async Task<IActionResult> DeleteClient(long cid)
        {
            var response = new ResponseMessage();
            try
            {
                IEnumerable<Employee> emp = await _context.Employees.Where(x => x.ClientId == cid).ToListAsync();
                foreach (var e in emp)
                {
                    _context.Employees.Remove(e);
                }
                await _context.SaveChangesAsync();
                response.message = "Recored deleted successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.data = new { };
                response.StatusCode = 500;
            }
            return new ObjectResult(response);
        }
        // DELETE: api/Employee/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.Employees.FindAsync(Id);
                if (res != null)
                {
                    _context.Employees.Remove(res);
                    await _context.SaveChangesAsync();
                    response.message = "Recored deleted successfully";
                    response.StatusCode = 200;
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
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != employee.EmployeeId)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                }
                employee.roleid = 5;
                employee.Password = Encryption.Encrypt(employee.Password);
                _context.Entry(employee).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = employee;
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
            return _context.Employees.Any(e => e.ClientId == id);
        }
    }
}
