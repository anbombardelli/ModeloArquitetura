using System;
using System.Collections.Generic;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Domain.ViewModels;
using Arquitetura.Services.Validator.Notification;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Arquitetura.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController
    {
        private readonly IUserService _service;
        private readonly INotification _notification;
        private readonly IMapper _mapper;

        public UserController(
            IUserService service,
            INotification notification,
            IMapper mapper) : base(notification)
        {
            _service = service;
            _notification = notification;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Post(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = _mapper.Map<User>(userViewModel);
            _service.Post(user);

            return CustomResponse(userViewModel);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = _mapper.Map<User>(userViewModel);
            _service.Put(user);

            return CustomResponse(userViewModel);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);

            return CustomResponse();
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> Get()
        {
            try
            {
                var users = _mapper.Map<IEnumerable<UserViewModel>>(_service.Get());
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserViewModel> Get(int id)
        {
            try
            {
                var userViewModel = _mapper.Map<UserViewModel>(_service.Get(id));
                if (userViewModel == null)
                    return NotFound();

                return userViewModel;
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}