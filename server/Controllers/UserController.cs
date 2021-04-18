using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       readonly string _connectionString = "Data Source = BHAGYA; Initial Catalog=gemDB; User id=sa; Password=123;";


        [HttpGet("Select/{id}")]
        public async Task<ActionResult> Select(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@id", id);

                    var result = await connection.QueryAsync<User>("SelectBuyer", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> login(User data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    String passHash = test.CreateMD5(data.password);
                    para.Add("@email", data.email);
                    para.Add("@password",passHash);

                    var result = await connection.QueryAsync("[dbo].[BuyerLogin]", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }


        [HttpPost("insert")]
        public async Task<ActionResult> insert(User data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    String passHash = test.CreateMD5(data.password);
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@firstName", data.firstName);
                    para.Add("@lastName", data.lastName);
                    para.Add("@email", data.email);
                    para.Add("@password", passHash);
                    para.Add("@address", data.address);
                    para.Add("@phoneNumber", data.phoneNumber);
                    

                    var result = await connection.QueryAsync("[dbo].[InsertBuyer]", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = true, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }


        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, User data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@id", id);
                    para.Add("@firstName", data.firstName);
                    para.Add("@lastName", data.lastName);
                    para.Add("@email", data.email);
                    para.Add("@password", data.password);
                    para.Add("@phoneNumber", data.phoneNumber);

                    var result = await connection.QueryAsync("Sp name", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }


        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@id", id);

                    var result = await connection.QueryAsync("Sp name", para, commandType: CommandType.StoredProcedure);

                    return Ok(new BaseResponse() { success = true, message = "Success", errorType = "NA", data = result });
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = ex.Message, errorType = "VAL", data = ex, exceptionNumber = ex.Number });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new BaseResponse() { success = false, message = "Action will be canceled!", errorType = "EX" });
            }
        }

    }

    public class BaseResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string errorType { get; set; }
        public object data { get; set; }
        public int exceptionNumber { get; set; }
    }


    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        
    }


}


