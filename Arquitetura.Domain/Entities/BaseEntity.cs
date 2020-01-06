using System;

namespace Arquitetura.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
    }
}
