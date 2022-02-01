﻿using Solucao.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Contracts
{
    public class CalendarViewModel
    {
        public Guid? Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid EquipamentId { get; set; }
        public Guid ClientId { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string StartTime1 { get; set; }
        public string EndTime1 { get; set; }
        public Guid? DriverId { get; set; }
        public Guid? TechniqueId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ParentId { get; set; }
        public User User { get; set; }
        public Person Technique { get; set; }
        public Person Driver { get; set; }
        public Client Client { get; set; }
        public Equipament Equipament { get; set; }
        public IList<CalendarSpecifications> CalendarSpecifications { get; set; }
    }
}
