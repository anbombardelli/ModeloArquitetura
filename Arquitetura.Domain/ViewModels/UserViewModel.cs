using System;
using System.ComponentModel.DataAnnotations;

namespace Arquitetura.Domain.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
    }
}
