using System;

namespace Solucao.Application.Contracts.Requests
{
    public class CalendarRequest
    {
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? EquipamentId { get; set; }
    }
}
