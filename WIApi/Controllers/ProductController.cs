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
        //[HttpPost]
        //[HttpPut]
        //[HttpDelete]
        //[HttpPatch]
        public ActionResult<IEnumerable<Product>> GetList()
        {
            return Ok(productService.GetList());
        }

        [X("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = productService.Delete(id);
            return result switch
            {
                ProductDeleteStatus.Ok => NoContent(),
                ProductDeleteStatus.ProductCannotBeDeleted => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }

        [X]
        public ActionResult Add([FromBody] ProductAddEditDto product)
        {
            var result = productService.Add(product);
            return Ok(result);
        }

        [X("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] ProductAddEditDto product)
        {
            var result = productService.Update(id, product);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}