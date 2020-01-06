using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Entities.Auth;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Arquitetura.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public object Post(
            [FromBody]User User,
            [FromServices]SigningConfiguration Signing,
            [FromServices]JWTConfiguration Token)
        {
            try
            {
                IAuthenticateService service = new AuthenticationService(Signing, Token, _configuration);

                return Ok(service.Authenticate(User));
            }
            catch (Exception ex)
            {
                return (Unauthorized());
            }
        }
    }
}