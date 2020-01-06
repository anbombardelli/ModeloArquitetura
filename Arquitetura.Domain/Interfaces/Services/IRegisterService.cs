using Arquitetura.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitetura.Domain.Interfaces.Services
{
    public interface IRegisterService
    {
        public User Create(User user);
    }
}
