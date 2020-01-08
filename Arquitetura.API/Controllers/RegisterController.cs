using System;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Domain.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arquitetura.API.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public RegisterController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserViewModel userViewModel)
        {
            try
            {
                var user = _mapper.Map<User>(userViewModel);
                return Ok(_registerService.Create(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}