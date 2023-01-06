namespace Inflow.Modules.Customers.Core.DTO
{
    internal class CustomerDetailsDTO : CustomerDTO
    {
        public string Address { get; set; }
        public IdentityDTO Identity { get; set; }
        public string Notes { get; set; }
    }
}
