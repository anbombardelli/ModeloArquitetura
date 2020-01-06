using Arquitetura.Domain.Entities;
using System;

namespace Arquitetura.Domain.Interfaces.Services
{
    public interface IAuthenticateService
    {
        public Object Authenticate(User user);
    }
}
