using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject_DTO_
{
    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password name is requred ")]
        public string Password { get; set; }
    }
}
