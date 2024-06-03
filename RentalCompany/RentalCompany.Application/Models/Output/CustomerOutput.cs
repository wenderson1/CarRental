using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Models.Output
{
    public class CustomerOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }

        public CustomerOutput(string id, string name, string cNPJ, DateTime birthDate, string cnhNumber, string cnhType)
        {
            Id = id;
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
        }
    }
}
