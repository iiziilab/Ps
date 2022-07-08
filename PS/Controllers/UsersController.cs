using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using PS.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuth jwtAuth;
        private readonly GeneralContext _context;
        //private UserPermissionController _userpermission;
        public static IWebHostEnvironment _environment;

        public UsersController(GeneralContext context, IAuth auth,IWebHostEnvironment environment)
        {
            _context = context;
            jwtAuth = auth;
            //_userpermission = userpermission;
            _environment = environment;
        }

        // GET: api/Users
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<User> response = null;
            try
            {
                response = await _context.Users.Select(x => new User
                {
                    ClientCompany = x.ClientCompany,
                    email = x.email,
                    password = Encryption.Decrypt(x.password),
                    Role = x.Role,
                    Created = x.Created
                }).OrderBy(x => x.id).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
            //return await _context.Users.ToListAsync();
        }

        [HttpGet("ByDate")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByDate()
        {
            List<User> response = null;
            try
            {
                response = await _context.Users.Select(x => new User
                {
                    ClientCompany = x.ClientCompany,
                    email = x.email,
                    password = Encryption.Decrypt(x.password),
                    Role = x.Role,
                    Created = x.Created
                }).Where(x=> DateTime.Compare(x.Created.Date, DateTime.Now.Date) == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
            //return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            user.password = Encryption.Decrypt(user.password);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPost("Records")]
        public async Task<ActionResult<User>> UserRecord(Record model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.email == model.email && x.roleid == model.role && x.id==model.id);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                user.password = Encryption.Decrypt(user.password);
            }
            
            return user;
        }

        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPost("UserRecords")]
        public async Task<ActionResult<UserInfo>> UserInfoRecord(Record model)
        {
            var userinfo = await _context.UserInfos.SingleOrDefaultAsync(x => x.Email == model.email && x.userid == model.id);
            if (userinfo != null)
            {
                userinfo.Password = Encryption.Decrypt(userinfo.Password);
            }
            if (userinfo == null)
            {
                return NotFound();
            }
            return userinfo;
        }

        // GET: api/Users/getclient/5
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("getclient/{clientid}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserbyId(long clientId)
        {
            User response = null;
            try
            {
                response = await _context.Users.SingleOrDefaultAsync(x => x.ClientId == clientId);
                if (response != null)
                {
                    response.password = Encryption.Decrypt(response.password);
                }
            }
            catch(Exception ex)
            {

            }
            return new ObjectResult(response);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != user.id)
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                user.roleid = user.Role.roleId;
                user.password = Encryption.Encrypt(user.password);
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
                    if (!UserExists(id))
                    {
                        response.message = "Not Found";
                        response.StatusCode = 404;
                        response.data = new { };
                        return NotFound();
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
                //return NoContent();
        }

       // [Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutUserInfo(int id, UserInfo user)
        {
            var response = new ResponseMessage();
            try
            {
                if (id != user.userid)
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                //user.roleId = user.Role.roleId;
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
                    if (!UserExists(id))
                    {
                        response.message = "Not Found";
                        response.StatusCode = 404;
                        response.data = new { };
                        return NotFound();
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
            //return NoContent();
        }

        // PUT: api/Users/updateclient
       // [Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPut("updateclient/{clientid}")]
        public async Task<IActionResult> PutUserbyId(int clientid, User user)
        {
            var response = new ResponseMessage();
            try
            {
                var usr = _context.Users.FirstOrDefault(x => x.ClientId == clientid);
                user.id = usr.id;
                if (clientid != user.ClientId)
                {
                    response.message = "BadRequest";
                    response.StatusCode = 400;
                    response.data = new { };
                    return new ObjectResult(response);
                    //return BadRequest();
                }
                user.roleid = usr.roleid;
                user.password = Encryption.Encrypt(user.password);
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    response.message = "Record updated successfully!";
                    response.StatusCode = 200;
                    response.data = user;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!UserExists(clientid))
                    {
                        response.message = "Not Found";
                        response.StatusCode = 404;
                        response.data = new { };
                        return NotFound();
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ObjectResult(response);
            //return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "superAdmin,admin,user")]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody]User user)
        {
            var response = new ResponseMessage();
            try
            {
                var clientIDExist = await _context.Users.FirstOrDefaultAsync(x => x.email == user.email);
                if (clientIDExist != null)
                {
                    response.message = "Username already in use, please choose another username";
                    response.StatusCode = 1;
                    response.data = new { };
                    return new ObjectResult(response);
                }
                if (!string.IsNullOrEmpty(user.email))
                {
                    var EmailExist = await _context.Users.FirstOrDefaultAsync(x => x.email == user.email);
                    if (EmailExist != null)
                    {
                        response.message = "Email address already in use";
                        response.StatusCode = 2;
                        response.data = new { };
                        return new ObjectResult(response);
                    }
                }

                User client = null;
                if (user.id > 0)
                {
                    client = await _context.Users.FindAsync(user.id);
                }
                bool isNew = false;
                if (client == null)
                {
                    isNew = true;
                    client = new User();
                }
                client.email = user.email;
                client.password = Encryption.Encrypt(user.password);
                client.ClientId = user.ClientId;
                client.roleid = user.Role.roleId;
                client.Created = DateTime.Now;
                if (isNew)
                {
                    await _context.Users.AddAsync(client);
                    response.message = "Company account created successfully";
                    response.StatusCode = 200;
                    response.data = client;
                }
                else
                {
                    response.message = "Company account updated successfully";
                    response.StatusCode = 200;
                    response.data = client;
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

        // DELETE: api/Users/5
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize(Roles = "superAdmin")]
        [HttpDelete("deleteclient/{clientid}")]
        public async Task<ActionResult<User>> DeleteUserbyId(int clientid)
        {
            var response = new ResponseMessage();
            try
            {

                var client = await _context.Users.FirstOrDefaultAsync(x => x.ClientId == clientid);
                if (client == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(client);
                await _context.SaveChangesAsync();
                response.message = "Company account deleted successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return new ObjectResult(response);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication(UserCredential model)
        {
            var response = new ResponseMessage();
            try
            {
                var pass = Encryption.Encrypt(model.Password);
                var user = await _context.Users.SingleOrDefaultAsync(x => x.email == model.UserName && x.password == Encryption.Encrypt(model.Password));
                if (user == null)
                {
                    var employee = _context.Employees.SingleOrDefault(x => x.Email == model.UserName && x.Password == Encryption.Encrypt(model.Password));
                    if (user == null && employee == null)
                    {
                        var userinfo = _context.UserInfos.SingleOrDefault(x => x.Email == model.UserName && x.Password == Encryption.Encrypt(model.Password));
                        if (user == null && employee == null && userinfo == null)
                        {
                            response.message = "Invalid Email/Password, Please enter correct email/password";
                            response.StatusCode = 401;
                            response.data = user;
                            response.token = "";
                        }
                        else
                        {
                            var userinforole = _context.Roles.SingleOrDefault(x => x.roleId == userinfo.roleId[0]);
                            var token = jwtAuth.Authentication(model.UserName, model.Password, userinforole.roleName);
                            if (token == null)
                            {
                                response.message = "Invalid Email/Password, Please enter your correct email/password";
                                response.StatusCode = 401;
                                response.data = user;
                                response.token = "";
                            }
                            if (userinfo.statusId == 1) {
                                response.message = "User login successfully";
                            }
                            else{
                                response.message = "Inactive User, Please contact admin";
                            }
                            response.StatusCode = 200;
                            response.data = new {email=model.UserName,password=model.Password,Role=userinforole,id= userinfo.userid,statusId = userinfo.statusId };
                            response.token = token;
                            response.rolePermission = await _context.RolePermissions.FirstOrDefaultAsync(x => x.roleId == userinfo.roleId[0]);
                        }
                    }
                    else
                    {
                        var emprole = _context.Roles.SingleOrDefault(x => x.roleId == employee.roleid);
                        var token = jwtAuth.Authentication(model.UserName, model.Password, emprole.roleName);
                        if (token == null)
                        {
                            response.message = "Invalid Email/Password, Please enter your correct email/password";
                            response.StatusCode = 401;
                            response.data = user;
                            response.token = "";
                            //return Unauthorized();
                        }
                        response.message = "User login successfully";
                        response.StatusCode = 200;
                        response.data = employee;
                        response.token = token;
                        response.rolePermission = await _context.RolePermissions.FirstOrDefaultAsync(x => x.roleId == employee.roleid);
                    }
                    //return Unauthorized();
                }
                else
                {
                    var role = _context.Roles.SingleOrDefault(x => x.roleId == user.roleid);
                    var token = jwtAuth.Authentication(model.UserName, model.Password, role.roleName);
                    if (token == null)
                    {
                        response.message = "User is not authorized to access this application";
                        response.StatusCode = 401;
                        response.data = user;
                        response.token = "";
                        //return Unauthorized();
                    }
                    response.message = "User login successfully";
                    response.StatusCode = 200;
                    response.data = user;
                    response.token = token;
                    response.rolePermission = await _context.RolePermissions.FirstOrDefaultAsync(x => x.roleId == user.roleid);
                    //return Ok(token);
                }

            }

            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 404;
                response.data = new { };
            }
            return new ObjectResult(response);

        }
        //[Authorize(Roles = "superAdmin,admin,client")]
        [HttpPost("UploadFile/{id}")]
        public async Task<IActionResult> UploadFile(int id,[FromForm] IFormFile file)
        {
            var response = new ResponseMessage();
            try
            {
                if (file != null)
                {
                    if (file.ContentType.Contains("image"))
                    {
                        //string extension = Path.GetExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName).ToLower();
                        if (extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif")
                        {
                            var filename = String.Concat(DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss"), extension);
                            var basePath = Path.Combine(_environment.WebRootPath, "images", "tmp");
                            if (Directory.Exists(basePath) == false)
                            {
                                Directory.CreateDirectory(basePath);
                            }
                            using (var inputStream = new FileStream(Path.Combine(basePath, filename), FileMode.Create))
                            {
                                try
                                {
                                    await file.CopyToAsync(inputStream);
                                    //return ResponseInfo.Success(filename);
                                }
                                catch (Exception ex)
                                {
                                    //ex.Log();
                                    //return ResponseInfo.Error("Failed to save image");
                                }
                            }
                        }
                        else
                        {
                            response.message = "File type not accepted. Only PNG, JPG/JPEG, and gif are allowed";
                            response.StatusCode = 200;
                            response.data = new { };
                        }
                        string fName = file.FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmssfff");//DateTime.Now.ToFileTime()
                        string path = Path.Combine(_environment.WebRootPath, "image/" + fName + extension);//"image/" +
                        //using (var stream = new FileStream(path, FileMode.Create))
                        //{
                        //    await file.CopyToAsync(stream);
                        //}
                        var image = new UserImage();
                        var img = await _context.UserImages.FirstOrDefaultAsync(x => x.UserId == id);
                        if (img != null)
                        {
                            var userimg = await _context.UserImages.FindAsync(img.Id);
                            if (userimg != null)
                            {
                                _context.UserImages.Remove(userimg);
                                await _context.SaveChangesAsync();
                            }
                        }
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            image.Image = ms.ToArray();
                        }
                        image.Path = path;
                        image.FileName = fName + extension;
                        image.UserId = id;
                        await _context.UserImages.AddAsync(image);
                        response.message = "image uploaded successfully";
                        response.StatusCode = 200;
                        response.data = image;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        response.message = "Invalid file";
                        response.StatusCode = 200;
                        response.data = new { };
                    }
                    
                }
                else
                {
                    response.message = "image not uploaded";
                    response.StatusCode = 400;
                    response.data = new { };
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 404;
                response.data = new { };
            }
            return new ObjectResult(response);
        }

        //GET :  api/Users/getpic/5
        //[Authorize(Roles = "superAdmin,admin,client")]
        [HttpGet("getpic/{id}")]
        public async Task<IActionResult> GetUserPicbyId(int id)
        {
            var response = new ResponseMessage();
            var client = new UserImage();
            try
            {
                client = await _context.UserImages.FirstOrDefaultAsync(x => x.UserId == id);
                if (client == null)
                {
                    response.message = "image not found";
                    response.StatusCode = 404;
                    response.data = new { };
                    //return NotFound();
                    return new ObjectResult(response);
                }
                response.message = "image found";
                response.StatusCode = 200;
                response.data = client;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 500;
                response.data = ex.Data;
            }
            return new ObjectResult(response);
        }

        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpPost("UploadUserFile/{id}")]
        public async Task<IActionResult> UploadUserInfoFile(int id, [FromForm] IFormFile file)
        {
            var response = new ResponseMessage();
            var image = new UserInfoImage();
            try
            {
                if (file != null)
                {
                    if (file.ContentType.Contains("image"))
                    {
                        var pic = await _context.UserInfoImages.FirstOrDefaultAsync(x => x.userid == id);
                        if (pic != null)
                        {
                            _context.UserInfoImages.Remove(pic);
                            await _context.SaveChangesAsync();
                        }
                        string extension = Path.GetExtension(file.FileName).ToLower();
                        string fName = file.FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmssfff");//DateTime.Now.ToFileTime()
                        //string path = Path.Combine(_environment.WebRootPath, "image/" + fName + extension);//"image\\" 
                        string path = Path.Combine("C:\\inetpub", "assets/" + fName + extension);//"image\\" 

                        var img = await _context.UserImages.FirstOrDefaultAsync(x => x.UserId == id);
                        if (img != null)
                        {
                            var userimg = await _context.UserImages.FindAsync(img.Id);
                            if (userimg != null)
                            {
                                _context.UserImages.Remove(userimg);
                                await _context.SaveChangesAsync();
                            }
                        }

                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            image.Image = ms.ToArray();
                        }
                        image.Path = path;
                        image.FileName = fName + extension;
                        image.userid = id;
                        await _context.UserInfoImages.AddAsync(image);
                        response.message = "image uploaded successfully";
                        response.StatusCode = 200;
                        response.data = image;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        response.message = "Invalid file";
                        response.StatusCode = 200;
                        response.data = new { };
                    }

                }
                else
                {
                    response.message = "image not uploaded";
                    response.StatusCode = 400;
                    response.data = new { };
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 404;
                response.data = image;
            }
            return new ObjectResult(response);
        }

        //GET :  api/Users/getpic/5
        //[Authorize(Roles = "superAdmin,admin,client,user")]
        [HttpGet("getuserpic/{id}")]
        public async Task<IActionResult> GetUserInfoPicbyId(int id)
        {
            var response = new ResponseMessage();
            var client = new UserInfoImage();
            try
            {
                client = await _context.UserInfoImages.FirstOrDefaultAsync(x => x.userid == id);
                if (client == null)
                {
                    response.message = "image not found";
                    response.StatusCode = 404;
                    response.data = new { };
                    //return NotFound();
                    return new ObjectResult(response);
                }
                
                response.message = "image found";
                response.StatusCode = 200;
                response.data = client;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 500;
                response.data = ex.Data;
            }
            return new ObjectResult(response);
        }
        //[HttpPost("UploadDemo1")]
        //public async Task<IActionResult> UploadDemoFile()
        //{
        //    var response = new ResponseMessage();
        //    try
        //    {
        //        var files = Request.Form.Files;
        //        if (files.Any(f => f.Length == 0))
        //        {
        //            return BadRequest();
        //        }
        //        if (files != null)
        //        {
        //            foreach (var file in files)
        //            {
        //                string extension = Path.GetExtension(file.FileName).ToLower();
        //                string fName = file.FileName;
        //                string path = Path.Combine(_environment.WebRootPath, "csv/" + fName + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        //                using (var stream = new FileStream(path, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //                }
        //            }
        //            int max_cols = 201;
        //            int num_rows = 0;

        //            int seg_index = 1;

        //            clsMatrix data = new clsMatrix(0, 0, 0);
        //            clsMatrix vol = new clsMatrix(0, 0, 0);
        //            clsMatrix seg_matrix = new clsMatrix(0, 0, 0);
        //            clsMatrix weight_matrix = new clsMatrix(0, 0, 0);
        //            List<clsAlt> alts = new List<clsAlt>();
        //            int count_alts = 0;
        //            int irow = 0;
        //            string Directory_data = Path.Combine(_environment.WebRootPath, "csv/");

        //            ArrayList alt_list = new ArrayList();
        //            //if (file.FileName == "alts_1.csv")
        //            //{
        //            alt_list = read_csv_to_array_list(path_file: Path.Combine(Directory_data, "alts_1.csv"), max_cols: 11, start_col: 1);
        //            foreach (object list in alt_list)
        //            {

        //                //var bow = list as clsAlt;
        //                //ArrayList nestedArrayList = list as ArrayList;
        //                IList result = list as IList;
        //                //foreach (object nestedListItem in nestedArrayList)
        //                //{
        //                clsAlt a = new clsAlt();
        //                // ID
        //                a.Id = Convert.ToInt32(result[0]);
        //                // Include
        //                a.Include = result[1].ToString() == "1" ? true : false;
        //                // ShortDescription
        //                a.ShortDescription = result[2].ToString();
        //                // LongDescription
        //                a.LongDescription = result[3].ToString();
        //                // CategoryIndex
        //                a.CategoryIndex = Convert.ToInt32(result[4]);
        //                // Category
        //                a.Category = result[5].ToString();
        //                // ID_MODEL
        //                a.ID_Model = Convert.ToInt32(result[6]);
        //                // PARM
        //                a.Parm = result[7].ToString();
        //                // Count
        //                // a.count = alt_list(i - 1)(8)
        //                // PARM_VOL
        //                a.Parm = result[9].ToString();
        //                // Consideration
        //                a.Consideration_Set = result[10].ToString() == "1" ? true : false;
        //                a.Default_Consideration_Set = a.Consideration_Set;
        //                a.Forced_In = false;

        //                if (a.Include)
        //                {
        //                    count_alts += 1;
        //                    alts.Add(a);
        //                }
        //            }
        //            //List<clsAlt> alts = new List<clsAlt>();
        //            //int count_alts = 0;

        //            //else if (file.FileName == "data_1.csv")
        //            //{
        //            data = read_csv_to_matrix(path_file: Path.Combine(Directory_data, "data_1.csv"), max_cols: count_alts + 1, start_col: 2);
        //            //    break;
        //            //}
        //            //else if (file.FileName == "seg_1.csv")
        //            //{
        //            seg_matrix = read_csv_to_matrix(path_file: Path.Combine(Directory_data, "seg_1.csv"), max_cols: 52, start_col: 2);
        //            //    break;
        //            //}
        //            //else if (file.FileName == "weights_1.csv")
        //            //{
        //            weight_matrix = read_csv_to_matrix(path_file: Path.Combine(Directory_data, "weights_1.csv"), max_cols: 4, start_col: 2);
        //            //}

        //            clsVector seg = new clsVector(data.RowCount, 0);
        //            clsVector weight = new clsVector(data.RowCount, 0);

        //            seg.Vector = seg_matrix.Columns;
        //            weight.Vector = weight_matrix.Columns;

        //            clsCalculate c = new clsCalculate(alts: alts, data: data, vol: data, weight: weight, seg: seg, seg_index: seg_index, number_of_saved_combos: 500, threshold: 130);

        //            ArrayList results = new ArrayList();
        //            results = c.CalculateParallel(num_chosen: 5, allow_fixed_items: true);

        //            response.message = "image uploaded successfully";
        //            response.StatusCode = 200;
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            response.message = "image not uploaded";
        //            response.StatusCode = 400;
        //            response.data = new { };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.message = ex.Message;
        //        response.StatusCode = 404;
        //        //response.data = image;
        //    }
        //    return new ObjectResult(response);
        //}

        [HttpPost("UploadDemo")]
        public async Task<IActionResult> UploadFile(CSV_Data demo)
        {
            var response = new ResponseMessage();
            var uploadfile = new UploadCSV();
            //StringBuilder sb = new StringBuilder();
            try
            {
                //var files = Request.Form.Files;
                //if (files.Any(f => f.Length == 0))
                //{
                //    return BadRequest();
                //}
                //if (files != null)
                //{
                //    foreach (var file in files)
                //    {
                //        string extension = Path.GetExtension(file.FileName).ToLower();
                //        string fName = file.FileName.Split('.')[0];
                //        string path = Path.Combine(_environment.WebRootPath, "csv\\" + fName + DateTime.Now.ToString("yyyyMMddHHmmssfff")+extension);
                //        sb.Append(path + " ");
                //        using (var stream = new FileStream(path, FileMode.Create))
                //        {
                //            await file.CopyToAsync(stream);
                //        }
                //    }
                //    var up = await _context.UploadCSVs.FirstOrDefaultAsync(x=>x.projectId == demo.projectId && x.cellId == demo.cellId);
                //    if(up != null)
                //    {
                //        _context.UploadCSVs.Remove(up);
                //    }
                uploadfile.projectId = demo.projectId;
                uploadfile.cellId = demo.cellId;
                uploadfile.data_1 = demo.data_1;
                uploadfile.seg_1 = demo.seg_1;
                uploadfile.weights_1 = demo.weights_1;

                //uploadfile.FileURL = sb.ToString();
                //uploadfile.result = demo.result;
                await _context.UploadCSVs.AddAsync(uploadfile);
                response.message = "Data saved successfully";
                response.StatusCode = 200;
                response.data = uploadfile;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 404;
                //response.data = image;
            }
            return new ObjectResult(response);
        }

        [HttpGet("getReport/{id}")]
        public async Task<ActionResult<UploadCSV>> GetItemReport(int? id)
        {
            var item = await _context.UploadCSVs.Where(x=>x.cellId == id).FirstOrDefaultAsync();
            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        
        //private clsMatrix read_csv_to_matrix(string path_file, int max_cols, int start_col)
        //{
        //    int num_rows;
        //    int irow = 0;
        //    ArrayList list_row_data = new ArrayList();

        //    list_row_data = read_csv_to_array_list(path_file, max_cols, start_col);
        //    num_rows = list_row_data.Count;

        //    clsMatrix data = new clsMatrix(num_rows, max_cols - start_col + 1, 0);

        //    int icol = 0;
        //    for (var i = 1; i <= num_rows; i++)
        //    {
        //        icol = 0;
        //        IList list = list_row_data[i - 1] as IList;
        //        for (var j = start_col; j <= max_cols; j++)
        //        {
        //            icol += 1;
        //            data[i - 1, icol - 1] = Convert.ToDouble(list[j - 1]);
        //        }
        //    }

        //    return data;
        //}


        private ArrayList read_csv_to_array_list(string path_file, int max_cols, int start_col)
        {
            int num_rows;
            int irow = 0;
            ArrayList list_row_data = new ArrayList();

            TextFieldParser tfp = new TextFieldParser(path_file);
            tfp.Delimiters = new string[] { "," };
            tfp.TextFieldType = FieldType.Delimited;

            tfp.ReadLine(); // skip header
            while (tfp.EndOfData == false)
            {
                var fields = tfp.ReadFields();
                object[] row_data = new object[max_cols - 1 + 1];
                irow += 1;
                for (var i = 1; i <= max_cols; i++)
                    row_data[i - 1] = fields[i - 1];
                list_row_data.Add(row_data);
            }

            num_rows = list_row_data.Count;

            return list_row_data;
        }

        //[Authorize(Roles = "superAdmin,admin,client,employee")]
        [HttpPost("UploadEmpFile/{id}")]
        public async Task<IActionResult> UploadEmpFile(long id, [FromForm] IFormFile file)
        {
            var response = new ResponseMessage();
            try
            {
                if (file != null)
                {
                    if (file.ContentType.Contains("image"))
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string fName = file.FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        string path = Path.Combine(_environment.WebRootPath, "image/" + fName + extension);//"image/" +
                        //using (var stream = new FileStream(path, FileMode.Create))
                        //{
                        //    await file.CopyToAsync(stream);
                        //}
                        var image = new EmployeeImage();
                        var img = await _context.EmployeeImages.FirstOrDefaultAsync(x => x.EmployeeId == id);
                        if (img != null)
                        {
                            var userimg = await _context.EmployeeImages.FindAsync(img.Id);
                            if (userimg != null)
                            {
                                _context.EmployeeImages.Remove(userimg);
                                await _context.SaveChangesAsync();
                            }
                        }
                        using (var ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            image.Image = ms.ToArray();
                        }
                        image.Path = path;
                        image.FileName = fName + extension;
                        image.EmployeeId = id;
                        await _context.EmployeeImages.AddAsync(image);
                        response.message = "image uploaded successfully";
                        response.StatusCode = 200;
                        response.data = image;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        response.message = "Invalid file";
                        response.StatusCode = 200;
                        response.data = new { };
                    }
                }
                else
                {
                    response.message = "image not uploaded";
                    response.StatusCode = 400;
                    response.data = new { };
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 404;
                response.data = new { };
            }
            return new ObjectResult(response);
        }
        //GET :  api/Users/getpic/5
        //[Authorize(Roles = "superAdmin,admin,client,employee")]
        [HttpGet("getemppic/{id}")]
        public async Task<IActionResult> GetEmpPicbyId(long id)
        {
            var response = new ResponseMessage();
            var client = new EmployeeImage();
            try
            {
                client = await _context.EmployeeImages.FirstOrDefaultAsync(x => x.EmployeeId == id);
                if (client == null)
                {
                    response.message = "image not found";
                    response.StatusCode = 404;
                    response.data = new { };
                    //return NotFound();
                    return new ObjectResult(response);
                }
                response.message = "image found";
                response.StatusCode = 200;
                response.data = client;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.StatusCode = 500;
                response.data = ex.Data;
            }
            return new ObjectResult(response);
        }
    }
    public class demo
    {
        public IFormFile[] File { get; set; }
        public int projectId { get; set; }
        public long cellId { get; set; }
        public string[] result { get; set; }
    }

    public class CSV_Data
    {
        public int projectId { get; set; }
        public long cellId { get; set; }
        public string data_1 { get; set; }
        public string seg_1 { get; set; }
        public string weights_1 { get; set; }
    }
}