namespace RentalCompany.Application.Models.Input
{
    public class CustomerInput
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
    }
}
