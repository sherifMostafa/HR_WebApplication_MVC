using System;
using System.Data.Entity;
using System.Linq;

namespace MVCTaskP.Models
{
    public class Piocontext : DbContext
    {
        // Your context has been configured to use a 'Piocontext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVCTaskP.Models.Piocontext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Piocontext' 
        // connection string in the application configuration file.
        public Piocontext()
            : base("name=Piocontext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<priv> Privs { get; set; }
        public virtual DbSet<Roleprivs> Roleprivs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Attendance_Leave> Attendance_Leaves { get; set; }
        public virtual DbSet<VacencyT> VacencyTs { get; set; }
        public virtual DbSet<VacencySetting> VacencySettings { get; set; }
        public virtual DbSet<VacencyTEmployees> VacancyTEmployeess { get; set; }
        public virtual DbSet<settings> settings { get; set; }
        //public virtual DbSet<Emp_OffVacency> Emp_OffVacencies { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}