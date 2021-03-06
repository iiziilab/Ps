// <auto-generated />
using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    [DbContext(typeof(GeneralContext))]
    partial class GeneralContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DAL.Models.Cell", b =>
                {
                    b.Property<long>("cellId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("cellName")
                        .HasColumnType("text");

                    b.Property<int>("projectId")
                        .HasColumnType("integer");

                    b.HasKey("cellId");

                    b.HasIndex("projectId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("DAL.Models.ClientCompany", b =>
                {
                    b.Property<long>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Contact1")
                        .HasColumnType("text");

                    b.Property<string>("Contact2")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Pincode")
                        .HasColumnType("varchar(7)");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.HasKey("ClientId");

                    b.ToTable("ClientCompanies");
                });

            modelBuilder.Entity("DAL.Models.ClientCredential", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientCredentials");
                });

            modelBuilder.Entity("DAL.Models.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("ChangePassword")
                        .HasColumnType("boolean");

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("ContactNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("EditProject")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<bool>("ViewProject")
                        .HasColumnType("boolean");

                    b.Property<int>("roleid")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ClientId");

                    b.HasIndex("roleid");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DAL.Models.EmployeeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeImages");
                });

            modelBuilder.Entity("DAL.Models.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<int>("CategoryIndex")
                        .HasColumnType("integer");

                    b.Property<int>("Consideration")
                        .HasColumnType("integer");

                    b.Property<int>("ForcedIn")
                        .HasColumnType("integer");

                    b.Property<int>("Include")
                        .HasColumnType("integer");

                    b.Property<string>("LongDescription")
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.Property<int?>("cellId")
                        .HasColumnType("integer");

                    b.Property<bool>("isExpanded")
                        .HasColumnType("boolean");

                    b.Property<int>("projectId")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("projectId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("DAL.Models.ModulePermission", b =>
                {
                    b.Property<int>("modulepermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ModuleName")
                        .HasColumnType("text");

                    b.Property<int>("statusId")
                        .HasColumnType("integer");

                    b.HasKey("modulepermissionId");

                    b.HasIndex("statusId");

                    b.ToTable("ModulePermissions");

                    b.HasData(
                        new
                        {
                            modulepermissionId = 1,
                            ModuleName = "Client",
                            statusId = 1
                        },
                        new
                        {
                            modulepermissionId = 2,
                            ModuleName = "Project",
                            statusId = 1
                        },
                        new
                        {
                            modulepermissionId = 3,
                            ModuleName = "User",
                            statusId = 1
                        },
                        new
                        {
                            modulepermissionId = 4,
                            ModuleName = "Role",
                            statusId = 1
                        },
                        new
                        {
                            modulepermissionId = 5,
                            ModuleName = "Permission",
                            statusId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DataType")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long[]>("EmployeeId")
                        .HasColumnType("bigint[]");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ProjectName")
                        .HasColumnType("text");

                    b.Property<string>("ProjectNo")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("statusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("statusId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DAL.Models.Role", b =>
                {
                    b.Property<int>("roleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("roleName")
                        .HasColumnType("text");

                    b.Property<int>("statusId")
                        .HasColumnType("integer");

                    b.HasKey("roleId");

                    b.HasIndex("statusId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            roleId = 1,
                            roleName = "superAdmin",
                            statusId = 1
                        },
                        new
                        {
                            roleId = 2,
                            roleName = "admin",
                            statusId = 1
                        },
                        new
                        {
                            roleId = 3,
                            roleName = "client",
                            statusId = 1
                        },
                        new
                        {
                            roleId = 4,
                            roleName = "user",
                            statusId = 1
                        },
                        new
                        {
                            roleId = 5,
                            roleName = "employee",
                            statusId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.RolePermission", b =>
                {
                    b.Property<int>("rolepermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("CellList")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientList")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool>("Menu")
                        .HasColumnType("boolean");

                    b.Property<bool>("PermissionDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("PermissionDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("PermissionInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("PermissionList")
                        .HasColumnType("boolean");

                    b.Property<bool>("PermissionUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectList")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool>("RoleDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("RoleDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("RoleInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("RoleList")
                        .HasColumnType("boolean");

                    b.Property<bool>("RoleUpdate")
                        .HasColumnType("boolean");

                    b.Property<bool>("Upload")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserList")
                        .HasColumnType("boolean");

                    b.Property<bool>("UserUpdate")
                        .HasColumnType("boolean");

                    b.Property<int>("roleId")
                        .HasColumnType("integer");

                    b.HasKey("rolepermissionId");

                    b.HasIndex("roleId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            rolepermissionId = 1,
                            CellList = true,
                            ClientDelete = true,
                            ClientDetails = true,
                            ClientInsert = true,
                            ClientList = true,
                            ClientUpdate = true,
                            Menu = true,
                            PermissionDelete = true,
                            PermissionDetails = true,
                            PermissionInsert = true,
                            PermissionList = true,
                            PermissionUpdate = true,
                            ProjectDelete = true,
                            ProjectDetails = true,
                            ProjectInsert = true,
                            ProjectList = true,
                            ProjectUpdate = true,
                            RoleDelete = true,
                            RoleDetails = true,
                            RoleInsert = true,
                            RoleList = true,
                            RoleUpdate = true,
                            Upload = true,
                            UserDelete = true,
                            UserDetails = true,
                            UserInsert = true,
                            UserList = true,
                            UserUpdate = true,
                            roleId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.Status", b =>
                {
                    b.Property<int>("statusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("statusName")
                        .HasColumnType("text");

                    b.HasKey("statusId");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            statusId = 1,
                            statusName = "Active"
                        },
                        new
                        {
                            statusId = 2,
                            statusName = "Inactive"
                        });
                });

            modelBuilder.Entity("DAL.Models.UploadCSV", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileURL")
                        .HasColumnType("text");

                    b.Property<long>("cellId")
                        .HasColumnType("bigint");

                    b.Property<string>("data_1")
                        .HasColumnType("text");

                    b.Property<int>("projectId")
                        .HasColumnType("integer");

                    b.Property<string[]>("result")
                        .HasColumnType("text[]");

                    b.Property<string>("seg_1")
                        .HasColumnType("text");

                    b.Property<string>("weights_1")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("cellId");

                    b.HasIndex("projectId");

                    b.ToTable("UploadCSVs");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("email")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<int>("roleid")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("ClientId");

                    b.HasIndex("roleid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("DAL.Models.UserInfo", b =>
                {
                    b.Property<int>("userid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string[]>("Designation")
                        .HasColumnType("text[]");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int[]>("roleId")
                        .HasColumnType("integer[]");

                    b.Property<int>("statusId")
                        .HasColumnType("integer");

                    b.HasKey("userid");

                    b.HasIndex("statusId");

                    b.ToTable("UserInfos");

                    b.HasData(
                        new
                        {
                            userid = 1,
                            Created = new DateTime(2022, 7, 7, 11, 50, 13, 42, DateTimeKind.Local).AddTicks(1312),
                            Designation = new[] { "superAdmin" },
                            Email = "admin@gmail.com",
                            FirstName = "admin",
                            LastName = "",
                            Password = "IBoKtjHCmJ0DICX0jCj0vg==",
                            roleId = new[] { 1 },
                            statusId = 1
                        });
                });

            modelBuilder.Entity("DAL.Models.UserInfoImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<int>("userid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("userid");

                    b.ToTable("UserInfoImages");
                });

            modelBuilder.Entity("DAL.Models.UserPermission", b =>
                {
                    b.Property<int>("userpermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("ClientDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientList")
                        .HasColumnType("boolean");

                    b.Property<bool>("ClientUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("ProjectDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectDetails")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectInsert")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectList")
                        .HasColumnType("boolean");

                    b.Property<bool>("ProjectUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("userpermissionId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("DAL.Models.Cell", b =>
                {
                    b.HasOne("DAL.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DAL.Models.ClientCredential", b =>
                {
                    b.HasOne("DAL.Models.ClientCompany", "ClientCompany")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCompany");
                });

            modelBuilder.Entity("DAL.Models.Employee", b =>
                {
                    b.HasOne("DAL.Models.ClientCompany", "ClientCompany")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCompany");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Models.EmployeeImage", b =>
                {
                    b.HasOne("DAL.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DAL.Models.Menu", b =>
                {
                    b.HasOne("DAL.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DAL.Models.ModulePermission", b =>
                {
                    b.HasOne("DAL.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DAL.Models.Project", b =>
                {
                    b.HasOne("DAL.Models.ClientCompany", "ClientCompany")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCompany");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DAL.Models.Role", b =>
                {
                    b.HasOne("DAL.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DAL.Models.RolePermission", b =>
                {
                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("roleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Models.UploadCSV", b =>
                {
                    b.HasOne("DAL.Models.Cell", "Cell")
                        .WithMany()
                        .HasForeignKey("cellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cell");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.HasOne("DAL.Models.ClientCompany", "ClientCompany")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCompany");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Models.UserImage", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.UserInfo", b =>
                {
                    b.HasOne("DAL.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DAL.Models.UserInfoImage", b =>
                {
                    b.HasOne("DAL.Models.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
