using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject_DTO_
{
    public record EmployeeDto(Guid Id, string Name, int age, string Position);//input Dto
    public record EmployeeForCreationDto( string Name, int age, string Position);//output Dto
}
