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
    public class MarkController : ControllerBase
    {

        readonly Database _DbContext;
        public MarkController(Database database)
        {
            _DbContext = database;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] Mark request)
        {
            try
            {
                if (request == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid payload." });

                if (request.StudentID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`StudentID` is mandatory." });

                if (request.SubjectID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`SubjectID` is mandatory." });


                if (!await _DbContext.tbl_Marks.AnyAsync(x => x.StudentID == request.StudentID))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`StudentID` is invalid." });


                if (!await _DbContext.tbl_Marks.AnyAsync(x => x.SubjectID == request.SubjectID))
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`SubjectID` is invalid." });
                

                var newRecord = new tbl_Mark { StudentID = request.StudentID, SubjectID = request.SubjectID, StudentMark = request.StudentMark };
                await _DbContext.tbl_Marks.AddAsync(newRecord);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = newRecord });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] tbl_Mark request)
        {
            try
            {
                if (request == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "invalid payload." });

                if (request.ID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`ID` is mandatory." });
               
                if (request.StudentID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`StudentID` is mandatory." });

                if (request.SubjectID <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`SubjectID` is mandatory." });
                 
                var markDetail = await _DbContext.tbl_Marks.Where(x => x.ID == request.ID).FirstOrDefaultAsync();
                if (markDetail == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "`ID` is invalid." });

                markDetail.StudentID = request.StudentID;
                markDetail.SubjectID = request.SubjectID;
                markDetail.StudentMark = request.StudentMark;

                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = markDetail });
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


                var markDetail = await _DbContext.tbl_Marks.Where(x => x.ID == id).FirstOrDefaultAsync();
                if (markDetail == null)
                    return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse { message = "detail with `id` doesn't exist." });


                _DbContext.tbl_Marks.Remove(markDetail);
                await _DbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = markDetail });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse { message = "unexpected error", data = ex.Message });
            }
        }


    }
}
