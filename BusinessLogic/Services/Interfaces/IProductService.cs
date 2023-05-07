
using BusinessLogic.Dto.Product;
using BusinessLogic.Enums;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetList();
        ProductDeleteStatus Delete(int id);
        int Add(ProductAddEditDto product);
        bool Update(int id, ProductAddEditDto product);
    }
}
