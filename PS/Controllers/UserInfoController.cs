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
    public class UserInfoController : ControllerBase
    {
        private readonly GeneralContext _context;
        //private readonly UserPermissionController _userpermission;
        public UserInfoController(GeneralContext context)
        {
            _context = context;
        }
        // GET: api/Role
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfo()
        {
            List<UserInfo> response = null;
            try
            {
                response = await _context.UserInfos.Select(x => new UserInfo
                {
                    userid = x.userid,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Designation = x.Designation,
                    Status = x.Status,
                    roleId = x.roleId,
                    Password = Encryption.Decrypt(x.Password),
                    Created = x.Created
                }).Where(x => x.userid != 1).OrderBy(x => x.userid).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        [HttpGet("ByDate")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfoByDate()
        {
            List<UserInfo> response = null;
            try
            {
                response = await _context.UserInfos.Select(x => new UserInfo
                {
                    userid = x.userid,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Designation = x.Designation,
                    Status = x.Status,
                    roleId = x.roleId,
                    Password = Encryption.Decrypt(x.Password),
                    Created = x.Created
                }).Where(x => x.userid != 1 && DateTime.Compare(x.Created.Date, DateTime.Now.Date) == 0).OrderBy(x => x.userid).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
        }

        // GET: api/Role/5
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet("{userid}")]
        public async Task<ActionResult<UserInfo>> GetUserInfo(int userid)
        {
            var user = new UserInfo();
            try
            {
                user = await _context.UserInfos.FindAsync(userid);
                user.Password = Encryption.Decrypt(user.Password);
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
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<UserInfo>> Post([FromBody] UserInfo model)
        {
            var response = new ResponseMessage();
            try
            {
                var userinfoExist = await _context.UserInfos.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (userinfoExist != null)
                {
                    response.message = "Email address already taken";
                    response.StatusCode = 201;
                    response.data = new { };
                    return new ObjectResult(response);
                }

                UserInfo user = null;
                if (model.userid > 0)
                {
                    user = await _context.UserInfos.FindAsync(model.userid);
                }
                bool isNew = false;
                if (user == null)
                {
                    isNew = true;
                    user = new UserInfo();
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Designation = model.Designation;
                user.roleId = model.roleId;
                user.statusId = model.statusId;
                user.Password = Encryption.Encrypt(model.Password);
                user.Created = DateTime.Now;
                if (isNew)
                {
                    await _context.UserInfos.AddAsync(user);
                    response.message = "User Info added successfully";
                    response.StatusCode = 200;
                    response.data = user;
                }
                else
                {
                    response.message = "User updated successfully";
                    response.StatusCode = 200;
                    response.data = user;
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
        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(int userid)
        {
            var response = new ResponseMessage();
            try
            {
                var res = await _context.UserInfos.FindAsync(userid);
                if (res != null)
                {
                    _context.UserInfos.Remove(res);
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
        [HttpPut("{userid}")]
        public async Task<IActionResult> PutUserInfo(int userid, UserInfo user)
        {
            var response = new ResponseMessage();
            try
            {
                if (userid != user.userid)//changed clientid to employeeid
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                user.Password = Encryption.Encrypt(user.Password);
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
                    if (!UserInfoExists(userid))
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

        private bool UserInfoExists(int id)
        {
            return _context.UserInfos.Any(e => e.userid == id);
        }
    }
}
