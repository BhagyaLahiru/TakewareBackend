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
    public class GemController : ControllerBase
    {
        readonly string _connectionString = "Data Source = BHAGYA; Initial Catalog=gemsdb; User id=sa; Password=123;";


        [HttpGet("Select/{id}")]
        public async Task<ActionResult> Select(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                    para.Add("@id", id);

                    var result = await connection.QueryAsync<Gem>("SelectGem", para, commandType: CommandType.StoredProcedure);

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



        [HttpGet("Selet")]
        public async Task<ActionResult> Selet()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();

                  

                    var result = await connection.QueryAsync<Gem>("SeleGems", para, commandType: CommandType.StoredProcedure);

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



        [HttpPost("insert")]
        public async Task<ActionResult> insert(Gem data)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    DynamicParameters para = new DynamicParameters();
              
                    para.Add("@gemName", data.gemName);
                    para.Add("@gemDescription", data.gemDescription);
                    para.Add("@price", data.price);
                    //para.Add("@password", data.password);

                    var result = await connection.QueryAsync("[dbo].[InsertGem]", para, commandType: CommandType.StoredProcedure);

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


    }
    public class Gem
    {
        public int gemID { get; set; }
        public string gemName { get; set; }
        public string gemDescription { get; set; }
        public int price  { get; set; }
        public string gemImage { get; set; }


    }

}

