using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly IConfiguration _configiratuion;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configiratuion = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from Employee";
            DataTable table = new DataTable();
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
        public JsonResult Post(Employee emp)
        {
            string query = @"insert into Employee(employeeName,Department,DateOfJoining,PhotoFileName) 
values ('" + emp.EmployeeName + "', '" + emp.Department + "', '" + emp.DateOfJoining + " ','" + emp.PhotoFileName + "');";

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
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update employee set EmployeeName = '" + emp.EmployeeName + "', Department = '" + emp.Department + "',  DateOfJoining = '" + emp.DateOfJoining + "',  PhotoFileName = '" + emp.PhotoFileName + "'  where EmployeeID = " + emp.EmployeeID + ";";

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


        [HttpDelete("{ID}")]
        public JsonResult Delete(int ID)
        {
            string query = @"delete from employee where EmployeeID = " + ID + ";";

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



        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }



        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configiratuion.GetConnectionString("default");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
