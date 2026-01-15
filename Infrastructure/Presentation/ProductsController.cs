using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation
{
    [ApiController]
    [Route("api/[Controller]")]

    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
       
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductQueryParameters parameters)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);


            if (result is null) return BadRequest();

            return Ok(result);

        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductById(int? Id)
        {
            if(Id is null or 0) return BadRequest();
            var result = await _serviceManager.ProductService.GetProductByIdAsync(Id.Value);

            return Ok(result);

        }


        [HttpGet("Types")]
        public async Task<IActionResult> GetAllProductTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();


            if (result is null) return BadRequest();

            return Ok(result);

        }

        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllProductBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();


            if (result is null) return BadRequest();

            return Ok(result);

        }



    }
}
