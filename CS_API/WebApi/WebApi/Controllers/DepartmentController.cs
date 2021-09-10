using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WebApi.Models;

namespace WebApi.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configiratuion;

        public DepartmentController(IConfiguration configuration)
        {
            this._configiratuion = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from  Department";
            System.Data.DataTable table = new System.Data.DataTable();
            string sqlDataSource = _configiratuion.GetConnectionString("default");
            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(table);
        }



        [HttpPost]
        public JsonResult Post(Department Dep)
        {
            string query = @"Insert into Department(DepartmentName) values ( '"+Dep.DepartmentName+"')";

            System.Data.DataTable table = new System.Data.DataTable();

            string sqlDataSource = _configiratuion.GetConnectionString("default");


            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult("Added Succefully.");

        }

        [HttpDelete("{ID}")]
        public JsonResult Delete(int ID)
        {
            string query = @"delete from Department where DepartmentID = " + ID + ";";

            System.Data.DataTable table = new System.Data.DataTable();

            string sqlDataSource = _configiratuion.GetConnectionString("default");
            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    reader = command.ExecuteReader();

                    table.Load(reader);
                    reader.Close();
                    sqlConnection.Close();

                }
            }
            return new JsonResult("Deleted Succefully.");

        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update department set DepartmentName = '" + dep.DepartmentName + "'  where DepartmentID = " + dep.DepartmentID + ";";

            System.Data.DataTable table = new System.Data.DataTable();

            string sqlDataSource = _configiratuion.GetConnectionString("default");


            SqlDataReader reader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    reader = command.ExecuteReader();

                    table.Load(reader);
                    reader.Close();
                    sqlConnection.Close();

                }
            }
            return new JsonResult("Updated Succefully.");

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
