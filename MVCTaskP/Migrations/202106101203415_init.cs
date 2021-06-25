namespace MVCTaskP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance_Leave",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emp_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Attend_Time = c.Time(nullable: false, precision: 7),
                        Leave_Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Emp_Id, cascadeDelete: true)
                .Index(t => t.Emp_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Salary = c.Int(nullable: false),
                        Atendance = c.Time(nullable: false, precision: 7),
                        Leave = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VacencySettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Emp_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Emp_Id, cascadeDelete: true)
                .Index(t => t.Emp_Id);
            
            CreateTable(
                "dbo.VacencyTs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        VDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.privs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        age = c.Int(),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        roleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.roleId, cascadeDelete: true)
                .Index(t => t.roleId);
            
            CreateTable(
                "dbo.Roleprivs",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        privId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.privId })
                .ForeignKey("dbo.privs", t => t.privId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.privId);
            
            CreateTable(
                "dbo.settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        over = c.Double(nullable: false),
                        deduction = c.Double(nullable: false),
                        weekends = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VacencyTEmployees",
                c => new
                    {
                        Empid = c.Int(nullable: false),
                        Vacnid = c.Int(nullable: false),
                        employee_Id = c.Int(),
                        Vacencyt_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Empid, t.Vacnid })
                .ForeignKey("dbo.Employees", t => t.employee_Id)
                .ForeignKey("dbo.VacencyTs", t => t.Vacencyt_Id)
                .Index(t => t.employee_Id)
                .Index(t => t.Vacencyt_Id);
            
            CreateTable(
                "dbo.VacencyTEmployees1",
                c => new
                    {
                        VacencyT_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VacencyT_Id, t.Employee_Id })
                .ForeignKey("dbo.VacencyTs", t => t.VacencyT_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.VacencyT_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Roleprivs1",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        priv_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.priv_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.privs", t => t.priv_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.priv_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VacencyTEmployees", "Vacencyt_Id", "dbo.VacencyTs");
            DropForeignKey("dbo.VacencyTEmployees", "employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Roleprivs", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roleprivs", "privId", "dbo.privs");
            DropForeignKey("dbo.Users", "roleId", "dbo.Roles");
            DropForeignKey("dbo.Roleprivs1", "priv_Id", "dbo.privs");
            DropForeignKey("dbo.Roleprivs1", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Attendance_Leave", "Emp_Id", "dbo.Employees");
            DropForeignKey("dbo.VacencyTEmployees1", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.VacencyTEmployees1", "VacencyT_Id", "dbo.VacencyTs");
            DropForeignKey("dbo.VacencySettings", "Emp_Id", "dbo.Employees");
            DropIndex("dbo.Roleprivs1", new[] { "priv_Id" });
            DropIndex("dbo.Roleprivs1", new[] { "Role_Id" });
            DropIndex("dbo.VacencyTEmployees1", new[] { "Employee_Id" });
            DropIndex("dbo.VacencyTEmployees1", new[] { "VacencyT_Id" });
            DropIndex("dbo.VacencyTEmployees", new[] { "Vacencyt_Id" });
            DropIndex("dbo.VacencyTEmployees", new[] { "employee_Id" });
            DropIndex("dbo.Roleprivs", new[] { "privId" });
            DropIndex("dbo.Roleprivs", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "roleId" });
            DropIndex("dbo.VacencySettings", new[] { "Emp_Id" });
            DropIndex("dbo.Attendance_Leave", new[] { "Emp_Id" });
            DropTable("dbo.Roleprivs1");
            DropTable("dbo.VacencyTEmployees1");
            DropTable("dbo.VacencyTEmployees");
            DropTable("dbo.settings");
            DropTable("dbo.Roleprivs");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.privs");
            DropTable("dbo.VacencyTs");
            DropTable("dbo.VacencySettings");
            DropTable("dbo.Employees");
            DropTable("dbo.Attendance_Leave");
        }
    }
}
