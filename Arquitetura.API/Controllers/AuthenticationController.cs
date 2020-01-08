using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Entities.Auth;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Domain.ViewModels;
using Arquitetura.Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Arquitetura.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public object Post(
            [FromBody]UserViewModel userViewModel,
            [FromServices]SigningConfiguration Signing,
            [FromServices]JWTConfiguration Token)
        {
            try
            {
                var user = _mapper.Map<User>(userViewModel);
                IAuthenticateService service = new AuthenticationService(Signing, Token, _configuration);

                return Ok(service.Authenticate(user));
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}