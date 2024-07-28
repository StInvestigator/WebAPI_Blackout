using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI_Blackout.Models;
using WebAPI1.Models;
using WebAPI1.Models.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("/api/v1/blackout")]
    public class BlackoutController : ControllerBase
    {

        [HttpPost]
        [Route("info")]
        public ApiResponse<IEnumerable<PowerStatus>> Info([FromBody] BlackoutFilter? filter)
        {
            try
            {
                IEnumerable<PowerStatus> data = new List<PowerStatus>
            {
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 18,
                    State = PowerState.Off
                },
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 19,
                    State = PowerState.Off
                },
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 20,
                    State = PowerState.Off
                },
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 21,
                    State = PowerState.Maybe
                },
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 22,
                    State = PowerState.Maybe
                },
                new PowerStatus
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Hour = 23,
                    State = PowerState.Maybe
                }
            };

                if (filter != null)
                    data = data.Where(x => (filter.FromHour == null || x.Hour >= filter.FromHour) && (filter.Date == null || x.Date == filter.Date));

                return ApiResponse<IEnumerable<PowerStatus>>.SuccessResponse(data);
            }
            catch(Exception ex)
            {
                Response.StatusCode = 404;
                return ApiResponse<IEnumerable<PowerStatus>>.ErrorResponse(ex.Message);
            }
        }
    }
}
