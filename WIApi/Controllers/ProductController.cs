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

        /// <summary>
        /// Returns list of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Product>> GetList()
        {
            return Ok(productService.GetList());
        }

        /// <summary>
        /// Deletes product of given id
        /// </summary>
        /// <param name="id">Id of product</param>
        /// <returns></returns>
        /// <response code="204">When successfully deleted</response>
        /// <response code="405">When failed to delete OR is already ordered</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = productService.Delete(id);
            return result switch
            {
                ProductDeleteStatus.Ok => NoContent(),
                ProductDeleteStatus.ProductCannotBeDeleted => StatusCode(StatusCodes.Status405MethodNotAllowed)
            };
        }

        [HttpPost]
        public ActionResult Add([FromBody] ProductAddEditDto product)
        {
            var result = productService.Add(product);
            return Ok(result);
        }

        [HttpPut("{id}")]
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