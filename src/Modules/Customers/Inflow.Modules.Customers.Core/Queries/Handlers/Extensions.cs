using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.DTO;

namespace Inflow.Modules.Customers.Core.Queries.Handlers
{
    internal static class Extensions
    {
        public static CustomerDTO AsDto(this Customer customer)
            => customer.Map<CustomerDTO>();

        public static CustomerDetailsDTO AsDetailsDto(this Customer customer)
        {
            var details = customer.Map<CustomerDetailsDTO>();
            details.Address = customer.Address;
            details.Notes = customer.Notes;
            details.Identity = customer.Identity is null ? null : new IdentityDTO()
            {
                Series = customer.Identity.Series,
                Type = customer.Identity.Type
            };
            return details;
        }

        private static T Map<T>(this Customer customer) where T : CustomerDTO, new()
            => new()
            {
                Id = customer.Id,
                Nationatity = customer.Nationatity,
                CreatedAt = customer.CreatedAt,
                Email = customer.Email,
                FullName = customer.FullName,
                IsActive = customer.IsActive,
                Name = customer.Name
            };
    }
}
