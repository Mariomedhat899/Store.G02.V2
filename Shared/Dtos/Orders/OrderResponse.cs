namespace Shared.Dtos.Orders
{
    public class OrderResponse
    {

        public Guid ID { get; set; }
        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;


        public OrderAddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> items { get; set; } //Navigational Property

        public Decimal SubTotal { get; set; }
        public Decimal Total { get; set; }



    }
}
