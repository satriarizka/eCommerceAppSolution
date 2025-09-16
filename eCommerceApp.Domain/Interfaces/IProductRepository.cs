using eCommerceApp.Domain.Entities;

namespace eCommerceApp.Domain.Interfaces
{
    public interface IProductRepository : IGeneric<Product>
    {
        // kalau perlu, tambahkan method khusus product
        // Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    }
}
