using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions.Services;
using Shared;
using Shared.Dtos.Products;
using Shared.ErrorModels;
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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache(180)]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters parameters)
        {
            var result = await _serviceManager.ProductService.GetAllProductsAsync(parameters);


            if (result is null) return BadRequest();

            return Ok(result);

        }


        [HttpGet("{Id}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProductById(int? Id)
        {
            if(Id is null or 0) return BadRequest();
            var result = await _serviceManager.ProductService.GetProductByIdAsync(Id.Value);

            return Ok(result);

        }


        [HttpGet("Types")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllProductTypes()
        {
            var result = await _serviceManager.ProductService.GetAllTypesAsync();


            if (result is null) return BadRequest();

            return Ok(result);

        }

        [HttpGet("Brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllProductBrands()
        {
            var result = await _serviceManager.ProductService.GetAllBrandsAsync();


            if (result is null) return BadRequest();

            return Ok(result);

        }



    }
}
