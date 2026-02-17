using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Services;
using Shared.Dtos.Orders;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IServiceManager _serviceManager) : ControllerBase
    {

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderRequest request)
        {

            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.OrderService.CreateOrderAsync(request, userEmailClaim.Value);

            return Ok(result);
        }

        //Get All Delivery Methods

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetAllDeliveyMethodsAsync();

            return Ok(deliveryMethods);
        }

        //Get All Orders For User


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdersForSpecificUser()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);

            var orders = await _serviceManager.OrderService.GetOrdesForSpecificUserAsync(userEmailClaim.Value);

            return Ok(orders);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByIdForSpecificUser(Guid id)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);

            var orders = await _serviceManager.OrderService.GetOrderByIdForSpecificUserAsync(id, userEmailClaim.Value);

            return Ok(orders);
        }
    }
}
