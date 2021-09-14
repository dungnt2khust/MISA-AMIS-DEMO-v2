using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.MF936.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        #region Constructors
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {

        }
        #endregion

        [HttpGet("1")]
        public void testPostgresql()
        {
            string Host = "mydemoserver.postgres.database.azure.com";
            string User = "mylogin@mydemoserver";
            string DBname = "mypgsqldb";
            string Password = "<server_admin_password>";
            string Port = "5432";

            string connString = String.Format("Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer", Host, User, DBname, Port, Password);

            using (var conn = new NpgsqlConnection(connString))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM Department", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            string.Format(
                                "Reading from table=({0}, {1}, {2})",
                                reader.GetInt32(0).ToString(),
                                reader.GetString(1),
                                reader.GetInt32(2).ToString()
                                )
                            );
                    }
                    reader.Close();
                }
                conn.Close();
            }
        } 
    }
}
