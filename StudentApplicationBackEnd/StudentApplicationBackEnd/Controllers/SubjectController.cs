using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StudentApplicationBackEnd.DTos;
using StudentApplicationBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentApplicationBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {


        readonly Database _DbContext;
        public SubjectController(Database database)
        {
            _DbContext = database;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] Subject request)
        {
            try
            {
                if (request == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid payload." });

                if (string.IsNullOrWhiteSpace(request.Name))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`Name` is mandatory." });
                request.Name = request.Name.Trim();
                 
                
                if (await _DbContext.tbl_Subjects.AnyAsync(x => x.Name == request.Name))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "Subject with same name already exists." });

                var newRecord = new tbl_Subject { Name = request.Name};
                await _DbContext.tbl_Subjects.AddAsync(newRecord);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = newRecord });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            try
            { 
                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = await _DbContext.tbl_Subjects.Select(x=> new { x.Name, x.ID}).ToArrayAsync() });
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


                var subjectDetail = await _DbContext.tbl_Subjects.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (subjectDetail == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "Subject with `id` doesn't exist." });


                _DbContext.tbl_Subjects.Remove(subjectDetail);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = subjectDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }


    }
}
