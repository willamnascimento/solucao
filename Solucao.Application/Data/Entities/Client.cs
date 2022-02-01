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
        public string Complement { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public City City { get; set; }
        public State State { get; set; }


    }
}
