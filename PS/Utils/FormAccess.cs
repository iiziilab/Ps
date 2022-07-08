using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PS.Utils
{
    public class FormAccess
    {
        private readonly GeneralContext _context;
        public FormAccess(GeneralContext context)
        {
            _context = context;
        }

        public async Task<UserPermission> dataaccess(int id)
        {
            UserPermission response = new UserPermission();
            try
            {
                response = await _context.UserPermissions.FirstOrDefaultAsync(x => x.userpermissionId == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return response;
        }
        
    }
}
