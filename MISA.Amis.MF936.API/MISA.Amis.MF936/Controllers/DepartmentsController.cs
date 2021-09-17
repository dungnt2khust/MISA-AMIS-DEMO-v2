using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
        public IActionResult testPostgresql()
        {
            string Host = "localhost";
            string User = "postgres";
            string DBname = "MISAAmis_MF936";
            string Password = "Thao0502";
            string Port = "5432";

            string connString = String.Format("Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer", Host, User, DBname, Port, Password);
            DataTable dt = new DataTable();
            using (var conn = new NpgsqlConnection(connString))

            {
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM department", conn))
                {
                    var reader = command.ExecuteReader();
                    NpgsqlDataAdapter nda = new NpgsqlDataAdapter(command);
                    nda.Fill(dt);
                    command.Dispose();
                    reader.Close();
                }
                conn.Close();

            }
            return StatusCode(200, dt);
        } 
    }
}
