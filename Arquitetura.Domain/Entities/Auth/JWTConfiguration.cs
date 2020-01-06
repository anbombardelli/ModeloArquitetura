using System;
using System.Collections.Generic;
using System.Text;

namespace Arquitetura.Domain.Entities.Auth
{
    public class JWTConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
