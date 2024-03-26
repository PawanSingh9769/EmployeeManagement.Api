using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage =" Employee name is required field")]
        [MaxLength(60, ErrorMessage =" Max length for the name is 30 char")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Position is required field")]
        [MaxLength(60 , ErrorMessage ="Max length is 20 for position")]
        public string? Position { get; set; }


        
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        
        //navigational property
        public Company? Company { get; set; }
        
    }
}
