using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role { roleId = 1, roleName="superAdmin", Status=null,statusId=1 },
                    new Role { roleId = 2, roleName = "admin", statusId = 1, Status = null },
                    new Role { roleId = 3, roleName = "client", statusId = 1, Status = null },
                    new Role { roleId = 4, roleName = "user", statusId = 1, Status = null },
                    new Role { roleId = 5, roleName = "employee", statusId = 1, Status = null }
                );
            modelBuilder.Entity<Status>().HasData(
                    new Status { statusName = "Active", statusId = 1 },
                    new Status { statusName = "Inactive", statusId = 2}
                );
            modelBuilder.Entity<UserInfo>().HasData(
                    new UserInfo { FirstName = "admin", LastName = "", Designation = new string[]{"superAdmin"},
                        Email="admin@gmail.com",roleId = new int[]{ 1 }, userid=1,
                        Password= "IBoKtjHCmJ0DICX0jCj0vg==", statusId = 1,Created=DateTime.Now }
                );
            modelBuilder.Entity<RolePermission>().HasData(
                   new RolePermission
                   {
                       rolepermissionId = 1,
                       roleId = 1,
                       ClientInsert =true,
                       ClientUpdate = true,
                       ClientDelete = true,
                       ClientList = true,
                       ClientDetails = true,
                       ProjectInsert = true,
                       ProjectUpdate = true,
                       ProjectDelete = true,
                       ProjectList = true,
                       ProjectDetails = true,
                       UserInsert = true,
                       UserUpdate = true,
                       UserDelete = true,
                       UserList = true,
                       UserDetails = true,
                       RoleInsert = true,
                       RoleUpdate = true,
                       RoleDelete = true,
                       RoleList = true,
                       RoleDetails = true,
                       PermissionInsert = true,
                       PermissionUpdate = true,
                       PermissionDelete = true,
                       PermissionList = true,
                       PermissionDetails = true,
                       CellList = true,
                       Upload = true,
                       Menu = true
                   }
               );
            modelBuilder.Entity<ModulePermission>().HasData(
                    new ModulePermission
                    {
                        modulepermissionId = 1,
                        ModuleName = "Client",
                        statusId = 1,
                        Status = null
                    },
                    new ModulePermission
                    {
                        modulepermissionId = 2,
                        ModuleName = "Project",
                        statusId = 1,
                        Status = null
                    },
                    new ModulePermission
                    {
                        modulepermissionId = 3,
                        ModuleName = "User",
                        statusId = 1,
                        Status = null
                    },
                    new ModulePermission
                    {
                        modulepermissionId = 4,
                        ModuleName = "Role",
                        statusId = 1,
                        Status = null
                    },
                    new ModulePermission
                    {
                        modulepermissionId = 5,
                        ModuleName = "Permission",
                        statusId = 1,
                        Status = null
                    }
                );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ClientCompany> ClientCompanies { get; set; }
        public DbSet<ClientCredential> ClientCredentials { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<UserInfoImage> UserInfoImages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<UploadCSV> UploadCSVs { get; set; }
    }
}
