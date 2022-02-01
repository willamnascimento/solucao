using Solucao.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Interfaces
{
    public interface ICalendarService
    {
        Task<ValidationResult> Add(CalendarViewModel calendar, Guid user);
        Task<ValidationResult> Update(CalendarViewModel calendar, Guid user);
        Task<IEnumerable<CalendarViewModel>> GetAll(DateTime date);
        Task<CalendarViewModel> GetById(Guid id);
        Task<ValidationResult> ValidateLease(DateTime date, Guid clientId, Guid equipamentId, string startTime);


    }
}
