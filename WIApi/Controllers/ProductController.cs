using BusinessLogic.Dto.Product;
using BusinessLogic.Enums;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WIApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(X)
        {
            X
        }

        X
        public async Task<ActionResult<IEnumerable<Product>>> GetList()
        {
            return Ok(await productService.GetListAsync());
        }

        X
        public async Task<ActionResult> Delete(X int id)
        {
            var result = await productService.DeleteAsync(id);
            return result switch
            {
                ProductDeleteStatus.Ok => X,
                ProductDeleteStatus.ProductCannotBeDeleted => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }

        X
        public async Task<ActionResult> Add(X ProductAddEditDto product)
        {
            var result = await productService.AddAsync(product);
            return Ok(result);
        }

        X
        public async Task<ActionResult> Update(X int id, X ProductAddEditDto product)
        {
            var result = await productService.UpdateAsync(id, product);
            if (result)
            {
                return X;
            }
            return X;
        }
    }
}