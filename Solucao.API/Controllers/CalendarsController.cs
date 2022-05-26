using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solucao.Application.Contracts;
using Solucao.Application.Contracts.Requests;
using Solucao.Application.Data.Entities;
using Solucao.Application.Service.Interfaces;
using Solucao.Application.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Solucao.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarService calendarService;
        private readonly IUserService userService;
        public CalendarsController(ICalendarService _calendarService, IUserService _userService)
        {
            calendarService = _calendarService;
            userService = _userService;
        }

        [HttpGet("calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<CalendarViewModel>> GetAllAsync([FromQuery] CalendarRequest model)
        {
            return await calendarService.GetAll(model.Date);
        }

        [HttpGet("calendar/availability")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<CalendarViewModel>> AvailabilityAsync([FromQuery] CalendarRequest model)
        {
            return await calendarService.Availability(model.StartDate, model.EndDate, model.ClientId, model.EquipamentId);
        }

        [HttpPost("calendar")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PostAsync([FromBody] CalendarViewModel model)
        {
            ValidationResult result;
            result = await calendarService.ValidateLease(model.Date, model.ClientId, model.EquipamentId, model.CalendarSpecifications, model.StartTime1);

            if (result != null)
            {
                if (!result.ErrorMessage.Contains("minutos"))
                    return NotFound(result);
                else
                    model.Note = result.ErrorMessage;
            }

            var user = userService.GetByName(User.Identity.Name).Result;

            result = await calendarService.Add(model, user.Id);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("calendar/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Calendar))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PutAsync([FromBody] CalendarViewModel model)
        {
            ValidationResult result;
            result = await calendarService.ValidateLease(model.Date, model.ClientId, model.EquipamentId, model.CalendarSpecifications, model.StartTime1);

            if (result != null)
            {
                if (!result.ErrorMessage.Contains("minutos"))
                    return NotFound(result);
                else
                    model.Note = result.ErrorMessage;
            }

            var user = userService.GetByName(User.Identity.Name).Result;

            result = await calendarService.Update(model, user.Id);

            if (result != null)
                return NotFound(result);

            return Ok(result);
        }
    }
}
