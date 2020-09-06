using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using StudentApplicationBackEnd.DTos;
using StudentApplicationBackEnd.Models;

namespace StudentApplicationBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly Database _DbContext;
        public StudentController(Database database)
        {
            _DbContext = database;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] StudentMarkDTO request)
        {
            try
            {
                if (request == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid payload." });

                if (string.IsNullOrWhiteSpace(request.FirstName))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`FirstName` is mandatory." });
                request.FirstName = request.FirstName.Trim();

                if (string.IsNullOrWhiteSpace(request.LastName))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`LastName` is mandatory." });
                request.LastName = request.LastName.Trim();

                if (request.Class == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`Class` is mandatory." });


                if (await _DbContext.tbl_Students.AnyAsync(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.Class == request.Class))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "Student with same details already exists." });

                var newRecord = new tbl_Student { FirstName = request.FirstName, LastName = request.LastName, Class = request.Class };

                await _DbContext.tbl_Students.AddAsync(newRecord);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = newRecord });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }

        [HttpGet, HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery] string keyword = null, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)

        {
            try
            {

                if (pageNo <= 0)
                    pageNo = 1;

                if (pageSize > 50)
                    pageSize = 50;
                else if (pageSize <= 0)
                    pageSize = 10;

                keyword = keyword?.ToLower();

                var skip = (pageNo - 1) * pageSize;
                var take = pageSize;


                var query = _DbContext.tbl_Students.Where(x => (string.IsNullOrWhiteSpace(keyword) || x.ID.ToString() == keyword || x.FirstName.Contains(keyword) || x.LastName.Contains(keyword)));
                var newquery = query.Select(x => new { x.ID, x.FirstName, x.LastName, x.Class });
                var totalFilteredResultCount = await newquery.CountAsync();


                if (skip > totalFilteredResultCount)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid request", data = "no more file found." });

                var totalFilteredResult = await newquery.Skip(skip).Take(take).ToListAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = new { total = totalFilteredResultCount, pageNo = pageNo, pageSize = pageSize, records = totalFilteredResult } });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] tbl_Student request)
        {
            try
            {
                if (request == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid payload." });

                if (request.ID <=0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid `ID` supplied." });

                if (string.IsNullOrWhiteSpace(request.FirstName))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`FirstName` is mandatory." });
                request.FirstName = request.FirstName.Trim();

                if (string.IsNullOrWhiteSpace(request.LastName))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`LastName` is mandatory." });
                request.LastName = request.LastName.Trim();

                if (request.Class == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`Class` is mandatory." });

                var studentDetail = await _DbContext.tbl_Students.Where(x => x.ID == request.ID).FirstOrDefaultAsync();
                if (studentDetail == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "Student with `id` doesn't exist." });

                studentDetail.FirstName = request.FirstName;
                studentDetail.LastName = request.LastName;
                studentDetail.Class = request.Class; 
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = studentDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (id == 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid `id`." });


                var studentDetail = await _DbContext.tbl_Students.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (studentDetail == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "Student with `id` doesn't exist." });


                _DbContext.tbl_Students.Remove(studentDetail);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = studentDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }


    }
}
