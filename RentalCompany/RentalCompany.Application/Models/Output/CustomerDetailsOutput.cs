namespace RentalCompany.Application.Models.Output
{
    public class CustomerDetailsOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public byte[] CnhImage { get; set; }

        public CustomerDetailsOutput(string id, string name, string cNPJ, DateTime birthDate, string cnhNumber, string cnhType, byte[] cnhImage)
        {
            Id = id;
            Name = name;
            CNPJ = cNPJ;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImage = cnhImage;
        }
    }
}
