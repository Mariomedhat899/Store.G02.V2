using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
          
           var Result = await _serviceManager.BasketService.GetBasketAsync(id);

              return Ok(Result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
          
           var Result = await _serviceManager.BasketService.CreateBasketAsync(dto,TimeSpan.FromDays(1));

              return Ok(Result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
          
           var Result = await _serviceManager.BasketService.DeleteBasketAsync(id);

            return NoContent();
        }

    }
}
