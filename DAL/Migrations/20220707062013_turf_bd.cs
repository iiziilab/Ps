using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class turf_bd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientCompanies",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Pincode = table.Column<string>(type: "varchar(7)", nullable: true),
                    Contact1 = table.Column<string>(type: "text", nullable: true),
                    Contact2 = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCompanies", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    statusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    statusName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    userpermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    ClientInsert = table.Column<bool>(type: "boolean", nullable: false),
                    ClientUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    ClientDelete = table.Column<bool>(type: "boolean", nullable: false),
                    ClientList = table.Column<bool>(type: "boolean", nullable: false),
                    ClientDetails = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectInsert = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectDelete = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectList = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectDetails = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.userpermissionId);
                });

            migrationBuilder.CreateTable(
                name: "ClientCredentials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCredentials_ClientCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientCompanies",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulePermissions",
                columns: table => new
                {
                    modulepermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleName = table.Column<string>(type: "text", nullable: true),
                    statusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePermissions", x => x.modulepermissionId);
                    table.ForeignKey(
                        name: "FK_ModulePermissions_Statuses_statusId",
                        column: x => x.statusId,
                        principalTable: "Statuses",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectName = table.Column<string>(type: "text", nullable: true),
                    ProjectNo = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long[]>(type: "bigint[]", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataType = table.Column<string>(type: "text", nullable: true),
                    statusId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_ClientCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientCompanies",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Statuses_statusId",
                        column: x => x.statusId,
                        principalTable: "Statuses",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleName = table.Column<string>(type: "text", nullable: true),
                    statusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleId);
                    table.ForeignKey(
                        name: "FK_Roles_Statuses_statusId",
                        column: x => x.statusId,
                        principalTable: "Statuses",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string[]>(type: "text[]", nullable: true),
                    roleId = table.Column<int[]>(type: "integer[]", nullable: true),
                    statusId = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.userid);
                    table.ForeignKey(
                        name: "FK_UserInfos_Statuses_statusId",
                        column: x => x.statusId,
                        principalTable: "Statuses",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    cellId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projectId = table.Column<int>(type: "integer", nullable: false),
                    cellName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.cellId);
                    table.ForeignKey(
                        name: "FK_Cells_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Consideration = table.Column<int>(type: "integer", nullable: false),
                    Include = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    CategoryIndex = table.Column<int>(type: "integer", nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: true),
                    LongDescription = table.Column<string>(type: "text", nullable: true),
                    projectId = table.Column<int>(type: "integer", nullable: false),
                    cellId = table.Column<int>(type: "integer", nullable: true),
                    isExpanded = table.Column<bool>(type: "boolean", nullable: false),
                    ForcedIn = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Menus_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    ContactNo = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    ChangePassword = table.Column<bool>(type: "boolean", nullable: false),
                    EditProject = table.Column<bool>(type: "boolean", nullable: false),
                    ViewProject = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_ClientCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientCompanies",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_roleid",
                        column: x => x.roleid,
                        principalTable: "Roles",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    rolepermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleId = table.Column<int>(type: "integer", nullable: false),
                    ClientInsert = table.Column<bool>(type: "boolean", nullable: false),
                    ClientUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    ClientDelete = table.Column<bool>(type: "boolean", nullable: false),
                    ClientList = table.Column<bool>(type: "boolean", nullable: false),
                    ClientDetails = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectInsert = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectDelete = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectList = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectDetails = table.Column<bool>(type: "boolean", nullable: false),
                    UserInsert = table.Column<bool>(type: "boolean", nullable: false),
                    UserUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    UserDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UserList = table.Column<bool>(type: "boolean", nullable: false),
                    UserDetails = table.Column<bool>(type: "boolean", nullable: false),
                    RoleInsert = table.Column<bool>(type: "boolean", nullable: false),
                    RoleUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    RoleDelete = table.Column<bool>(type: "boolean", nullable: false),
                    RoleList = table.Column<bool>(type: "boolean", nullable: false),
                    RoleDetails = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionInsert = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionUpdate = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionDelete = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionList = table.Column<bool>(type: "boolean", nullable: false),
                    PermissionDetails = table.Column<bool>(type: "boolean", nullable: false),
                    CellList = table.Column<bool>(type: "boolean", nullable: false),
                    Upload = table.Column<bool>(type: "boolean", nullable: false),
                    Menu = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.rolepermissionId);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_ClientCompanies_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientCompanies",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleid",
                        column: x => x.roleid,
                        principalTable: "Roles",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfoImages_UserInfos_userid",
                        column: x => x.userid,
                        principalTable: "UserInfos",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadCSVs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projectId = table.Column<int>(type: "integer", nullable: false),
                    cellId = table.Column<long>(type: "bigint", nullable: false),
                    FileURL = table.Column<string>(type: "text", nullable: true),
                    result = table.Column<string[]>(type: "text[]", nullable: true),
                    data_1 = table.Column<string>(type: "text", nullable: true),
                    seg_1 = table.Column<string>(type: "text", nullable: true),
                    weights_1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadCSVs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadCSVs_Cells_cellId",
                        column: x => x.cellId,
                        principalTable: "Cells",
                        principalColumn: "cellId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadCSVs_Projects_projectId",
                        column: x => x.projectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeImages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "statusId", "statusName" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Inactive" }
                });

            migrationBuilder.InsertData(
                table: "ModulePermissions",
                columns: new[] { "modulepermissionId", "ModuleName", "statusId" },
                values: new object[,]
                {
                    { 1, "Client", 1 },
                    { 2, "Project", 1 },
                    { 3, "User", 1 },
                    { 4, "Role", 1 },
                    { 5, "Permission", 1 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "roleId", "roleName", "statusId" },
                values: new object[,]
                {
                    { 1, "superAdmin", 1 },
                    { 2, "admin", 1 },
                    { 3, "client", 1 },
                    { 4, "user", 1 },
                    { 5, "employee", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "userid", "Created", "Designation", "Email", "FirstName", "LastName", "Password", "roleId", "statusId" },
                values: new object[] { 1, new DateTime(2022, 7, 7, 11, 50, 13, 42, DateTimeKind.Local).AddTicks(1312), new[] { "superAdmin" }, "admin@gmail.com", "admin", "", "IBoKtjHCmJ0DICX0jCj0vg==", new[] { 1 }, 1 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "rolepermissionId", "CellList", "ClientDelete", "ClientDetails", "ClientInsert", "ClientList", "ClientUpdate", "Menu", "PermissionDelete", "PermissionDetails", "PermissionInsert", "PermissionList", "PermissionUpdate", "ProjectDelete", "ProjectDetails", "ProjectInsert", "ProjectList", "ProjectUpdate", "RoleDelete", "RoleDetails", "RoleInsert", "RoleList", "RoleUpdate", "Upload", "UserDelete", "UserDetails", "UserInsert", "UserList", "UserUpdate", "roleId" },
                values: new object[] { 1, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_projectId",
                table: "Cells",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCredentials_ClientId",
                table: "ClientCredentials",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeImages_EmployeeId",
                table: "EmployeeImages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientId",
                table: "Employees",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_roleid",
                table: "Employees",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_projectId",
                table: "Menus",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulePermissions_statusId",
                table: "ModulePermissions",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_statusId",
                table: "Projects",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_roleId",
                table: "RolePermissions",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_statusId",
                table: "Roles",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadCSVs_cellId",
                table: "UploadCSVs",
                column: "cellId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadCSVs_projectId",
                table: "UploadCSVs",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoImages_userid",
                table: "UserInfoImages",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_statusId",
                table: "UserInfos",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientId",
                table: "Users",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleid",
                table: "Users",
                column: "roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCredentials");

            migrationBuilder.DropTable(
                name: "EmployeeImages");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "ModulePermissions");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UploadCSVs");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "UserInfoImages");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ClientCompanies");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
