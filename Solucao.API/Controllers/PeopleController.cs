﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solucao.Application.Utils;
using Solucao.Application.Contracts;
using Solucao.Application.Data.Entities;
using Solucao.Application.Service.Implementations;
using Solucao.Application.Service.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Solucao.Application.Contracts.Requests;

namespace Solucao.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService personService;

        public PeopleController(IPersonService _personService)
        {
            personService = _personService;
        }

        [HttpGet("people")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Person))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<PersonViewModel>> GetAllAsync([FromQuery] PersonRequest personRequest)
        {
            return await personService.GetAll(personRequest.Ativo, personRequest.TipoPessoa);
        }

        [HttpGet("people/get-by-name")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Person))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<PersonViewModel>> GetByNameAsync([FromQuery] PersonRequest personRequest)
        {
            return await personService.GetAll(personRequest.Ativo, personRequest.TipoPessoa);
        }

        [HttpPost("people")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PostAsync([FromBody] PersonViewModel model)
        {
            var result = await personService.Add(model);

            if (result != null)
                return NotFound(result);
            return Ok(result);
        }


        [HttpPut("people/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] PersonViewModel model)
        {
            var result = await personService.Update(model);

            if (result != null)
                return NotFound(result);
            return Ok(result);
        }
    }
}
