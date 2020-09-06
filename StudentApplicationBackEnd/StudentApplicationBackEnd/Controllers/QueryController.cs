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
    public class QueryController : ControllerBase
    {
        readonly Database _DbContext;
        public QueryController(Database database)
        {
            _DbContext = database;
        }


        [HttpPost, HttpGet]
        [Route("ping")]
        public IActionResult ping()
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse { success = true, message = "success", data = "pong" });
        }

        [HttpGet, HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery] string keyword = null, [FromQuery] FilterType? filterType = FilterType.Student, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)

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


                var query = _DbContext.tbl_Marks.Where(x => (filterType == FilterType.Student && (string.IsNullOrWhiteSpace(keyword) || x.StudentID.ToString() == keyword || x.tbl_Student.FirstName.Contains(keyword) || x.tbl_Student.LastName.Contains(keyword))) || (filterType == FilterType.Class && x.tbl_Student.Class.ToString() == keyword) || (filterType == FilterType.Subject && x.tbl_Subject.Name.ToString() == keyword));
                var newquery = query.Select(x => new { x.ID, x.StudentID, x.StudentMark, x.tbl_Student.Class, x.tbl_Student.FirstName, x.tbl_Student.LastName, x.tbl_Subject.Name });
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

    }
}
