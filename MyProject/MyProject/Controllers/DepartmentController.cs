using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId,
                                DepartmentName from dbo.Department";
            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection sqlCon = new SqlConnection(sqlDataSource))
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
        public JsonResult Add(Department dep)
        {
            string query = @"Insert into Department values('"+dep.DepartmentName+@"')";
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
        public JsonResult Update(Department dep)
        {
            string query = @"Update Department set DepartmentName = '" + dep.DepartmentName + "' where DepartmentId = '" + dep.DepartmentId + @"' ";
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
        public JsonResult Delete(int depId)
        {
            string query = @"Delete from Department where DepartmentId = '" + depId + @"'";
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

        
    }
}
