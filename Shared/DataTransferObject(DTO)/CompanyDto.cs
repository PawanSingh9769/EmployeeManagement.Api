using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject_DTO_
{
    //input DTO
    public record CompanyDto(Guid Id, string Name, string FullAddress);

    //Output DTO
    public record  CompanyForCreationDto(string Name, string Address, string Country);

    
}
