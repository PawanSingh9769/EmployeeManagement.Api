using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Company name is a required field")]
        [MaxLength(60, ErrorMessage ="Maximum length for the name is 60 char")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 char")]
        public string ? Address { get; set; }
        
        public string? Country { get; set; }

        //navigational property
        //this properties serve the purpose of defining the relationship between our model
        public ICollection<Employee>? Employees { get; set; }
    }
}
