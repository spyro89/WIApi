
using BusinessLogic.Dto.Product;
using BusinessLogic.Enums;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetListAsync();
        Task<ProductDeleteStatus> DeleteAsync(int id);
        Task<int> AddAsync(ProductAddEditDto product);
        Task<bool> UpdateAsync(int id, ProductAddEditDto product);
    }
}
