
using BusinessLogic.Dto.Product;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetListAsync();
        Task<bool> DeleteAsync(int id);
        Task<int> AddAsync(ProductAddEditDto product);
        Task<bool> UpdateAsync(int id, ProductAddEditDto product);
    }
}
