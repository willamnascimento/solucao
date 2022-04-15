using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Data.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string  Number { get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public bool IsPhysicalPerson { get; set; }
        public bool IsAnnualContract { get; set; }
        public bool IsReceipt { get; set; }
        public string NameForReceipt { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool Has220V { get; set; }
        public bool HasStairs { get; set; }
        public bool TakeTransformer { get; set; }
        public bool HasTechnique { get; set; }
        public string TechniqueOption1 { get; set; }
        public string TechniqueOption2 { get; set; }
        public string LandMark { get; set; }
        public string Responsible { get; set; }
        public string Specialty { get; set; }
        public string ClinicName { get; set; }
        public string ClinicCellPhone { get; set; }
        public string ZipCode { get; set; }
        public string Secretary { get; set; }
        public string CpfCnpj { get; set; }
        public string RgIe { get; set; }
        public City City { get; set; }
        public State State { get; set; }


    }
}
