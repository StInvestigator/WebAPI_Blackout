using firstMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI_Blackout.Models;
using WebAPI1.Models;
using WebAPI1.Models.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI1.Controllers
{
    [ApiController]
    [Route("/api/v1/groups")]
    public class GroupController(SiteContext _siteContext) : ControllerBase
    {

        [HttpGet]
        [Route("list")]
        public async Task<ApiResponse<IEnumerable<Group>>> List()
        {
            try
            {
                return ApiResponse<IEnumerable<Group>>.SuccessResponse(await _siteContext.Groups.Include(x=>x.Schedule).ToListAsync());
            }
            catch(Exception ex)
            {
                Response.StatusCode = 404;
                return ApiResponse<IEnumerable<Group>>.ErrorResponse(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ApiResponse<Group>> Create([FromBody] Group group)
        {
            try
            {
                _siteContext.Add(group);
                await _siteContext.SaveChangesAsync();

                return ApiResponse<Group>.SuccessResponse(group);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return ApiResponse<Group>.ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ApiResponse<Group>> Edit(int id, [FromBody] Group group)
        {
            try
            {
                var model = (await _siteContext.Groups.Include(x => x.Schedule).ToListAsync()).Find(x=>x.Id==id);
                if (model == null) throw new Exception("Group was not found");
                model.Name = group.Name;
                if(group.Schedule.Count() != 0)
                {
                    foreach (var item in model.Schedule)
                    {
                        _siteContext.Remove(item);
                    }
                    model.Schedule = group.Schedule;
                }
                await _siteContext.SaveChangesAsync();

                return ApiResponse<Group>.SuccessResponse(model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return ApiResponse<Group>.ErrorResponse(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ApiResponse<string>> Delete(int id)
        {
            try
            {
                var model = (await _siteContext.Groups.Include(x=>x.Schedule).ToListAsync()).Find(x => x.Id == id);
                if (model == null) throw new Exception("Group was not found");
                foreach (var item in model.Schedule)
                {
                    _siteContext.Remove(item);
                }
                _siteContext.Groups.Remove(model);
                await _siteContext.SaveChangesAsync();

                return ApiResponse<string>.SuccessResponse("Ok");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return ApiResponse<string>.ErrorResponse(ex.Message);
            }
        }
    }
}
