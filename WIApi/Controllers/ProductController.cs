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

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetList()
        {
            return Ok(await productService.GetListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await productService.DeleteAsync(id);
            return result switch
            {
                ProductDeleteStatus.Ok => NoContent(),
                ProductDeleteStatus.ProductCannotBeDeleted => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductAddEditDto product)
        {
            var result = await productService.AddAsync(product);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProductAddEditDto product)
        {
            var result = await productService.UpdateAsync(id, product);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}