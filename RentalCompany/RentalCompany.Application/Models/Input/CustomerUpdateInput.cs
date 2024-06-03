namespace RentalCompany.Application.Models.Input
{
    public class CustomerUpdateInput
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhType { get; set; }
    }
}
