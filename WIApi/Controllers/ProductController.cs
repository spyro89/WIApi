using BusinessLogic.Dto.Product;
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
            await productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductAddEditDto product)
        {
            var result = await productService.AddAsync(product);
            return Ok(result);
        }

        [HttpPost("{id}")]
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