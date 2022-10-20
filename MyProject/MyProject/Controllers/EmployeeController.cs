using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Data.SqlClient;
using System.Data;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeId,
                                EmployeeName,
                                Department,
                                DateOfJoining,
                                PhotoFileName
                                from dbo.Employee";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection sqlCon = new SqlConnection(sqlDataSource))
            {
                sqlCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    sqlCon.Close();
                }
            }
            return new JsonResult(dt);

        }

        [HttpPost]
        public JsonResult Add(Employee em)
        {
            string query = @"Insert into Employee(EmployeeName,Department,DateOfJoining,PhotoFileName) values('" + em.EmployeeName + @"',
                            '" + em.Department + @"',
                            '" + em.DateOfJoining + @"',
                            '" + em.PhotoFileName + @"')";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection sqlCon = new SqlConnection(sqlDataSource))
            {
                sqlCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    sqlCon.Close();
                }
            }
            return new JsonResult("Added succesfully");

        }

        [HttpPut]
        public JsonResult Update(Employee em)
        {
            string query = @"Update Employee set EmployeeName = '" + em.EmployeeName + "' where EmployeeId = '" + em.EmployeeId + @"' ";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection sqlCon = new SqlConnection(sqlDataSource))
            {
                sqlCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    sqlCon.Close();
                }
            }
            return new JsonResult("Updated succesfully");

        }

        [HttpDelete]
        public JsonResult Delete(int empId)
        {
            string query = @"Delete from Employee where EmployeeId = ('" + empId + @"')";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection sqlCon = new SqlConnection(sqlDataSource))
            {
                sqlCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    sqlCon.Close();
                }
            }
            return new JsonResult("Deleted succesfully");

        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
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
    }
}
