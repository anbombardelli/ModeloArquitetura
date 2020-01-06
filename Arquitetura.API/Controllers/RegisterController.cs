using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Arquitetura.API.Controllers
{
    public class RegisterController : Controller
    {
        private IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                IRegisterService service = new RegisterService(_configuration);            

                return Ok(service.Create(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}