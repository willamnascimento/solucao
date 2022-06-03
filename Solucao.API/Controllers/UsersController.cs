﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly TokenService tokenService;
        private readonly ILogger<UsersController> logger;

        
        public UsersController(IUserService _userService, TokenService _tokenService, ILogger<UsersController> _logger)
        {
            userService = _userService;
            tokenService = _tokenService;
            logger = _logger;
        }


        [HttpGet("user")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            logger.LogInformation("Action GET");
            return await userService.GetAll();
        }

        [HttpPost("user")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PostAsync([FromBody] User model)
        {
            var result = await userService.Add(model);

            if (result != null)
                return NotFound(result);
            return Ok(result);
        }


        [HttpPut("user/{id}")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] User model)
        {
            var result = await userService.Update(model, id);

            if (result != null)
                return NotFound(result);
            return Ok(result);
            
        }

        [HttpPost("user/change-user-password")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ValidationResult))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.Conflict, Type = typeof(ApplicationError))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ApplicationError))]
        public async Task<IActionResult> ChangeUserPassworAsync([FromBody] ChangeUserPasswordRequest model)
        {
            // Recupera o usuário
            var user = await userService.GetByEmail(model.Email);

            if (user == null)
                return NotFound(new ApplicationError { Code = "404", Message = "Usuário e senha não conferem." });

            return Ok(await userService.ChangeUserPassword(user,model.Password));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] User model)
        {
            // Recupera o usuário
            var user = await userService.Authenticate(model.Email, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new ApplicationError { Code = "404", Message = "Senha ou usuário inválido."});

            // Gera o Token
            var token = tokenService.GenerateToken(user);

            // Retorna os dados
            return Ok(new
            {
                user = user,
                token = token
            });
        }
    }
}
