using System;
using System.Linq;
using System.Net;
using AutoMapper;
using MacsASPNETCore.Models;
using MacsASPNETCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MacsASPNETCore.Controllers.Api
{
    [Route("api/activities")]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _repository;

        public ActivityController(IActivityRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            var results = _repository.GetAllActivities();
            return Json(results);
        }

        [HttpGet("month")]
        public JsonResult Get([FromQuery]DateTime starting, [FromQuery]DateTime ending)
        {
            var results = _repository.GetAllActivitiesByMonth(starting, ending)
                .OrderBy(a => a.StartTime)
                .ToList();
            return Json(results);
        }

        [HttpPost("")]
        private JsonResult Post([FromBody]ActivityViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Use Automapper to check incoming data
                    var newActivity = Mapper.Map<Activity>(vm);

                    // Save to Database

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(true);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}