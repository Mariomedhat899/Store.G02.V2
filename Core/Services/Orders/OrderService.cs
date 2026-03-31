using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Services.Abstractions.Orders;
using Services.Specifications.Orders;
using Shared.Dtos.Orders;

namespace Services.Orders
{
    public class OrderService(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1.get order Address
            var orderAddress = _mapper.Map<OrderAddress>(request.ShipToAddress);

            //2.get Delivery Method by id
            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);

            if (deliveryMethod == null) throw new DeliveryMethodNotFoundExeption(request.DeliveryMethodId);

            //3. Get Orderitems
            //3.1 Get Basket By Id
            var basket = await _basketRepository.GetBasketAsync(request.BasketId);

            if (basket == null) throw new BasketNotFoundExeption(request.BasketId);

            //3.2 Convert BasketItems To OrderItems
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                //Check Price Of Each Item In Database
                //Get Product By Id From Database
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);
                if (product == null) throw new ProductNotFountException(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;

                var ProductInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);

                var orderItem = new OrderItem(ProductInOrderItem, item.Price, item.Quantity);

                orderItems.Add(orderItem);
            }

            //4. Calculate SubTotal 
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            //Create Order Object
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);
            //Add order In Database
            await _unitOfWork.GetRepository<Guid, Order>().AddAsync(order);

            var count = await _unitOfWork.SaveChangesAsync();

            if (count <= 0) throw new CreateOrderBadRequestException();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveyMethodResponse>> GetAllDeliveyMethodsAsync()
        {
            var deliveryMehtods = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAllAsync();

            return _mapper.Map<IEnumerable<DeliveyMethodResponse>>(deliveryMehtods);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecifications(id, userEmail);
            var order = await _unitOfWork.GetRepository<Guid, Order>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundExeption(id);

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdesForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecifications(userEmail);
            var orders = await _unitOfWork.GetRepository<Guid, Order>().GetAllAsync(spec);

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
