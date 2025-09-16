using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
