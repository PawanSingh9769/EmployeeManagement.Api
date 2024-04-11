using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid Id, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid Id, bool trackChanges);

       Task< EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges);

        void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges);

        void UpdateEmployeeForCompany(Guid companyId, Guid id , EmployeeForUpdateDto employeeForUpdate,bool compTrackChange, bool  empTrackChanges);
    }
}
