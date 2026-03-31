namespace Domain.Entites.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
        }

        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> Items, decimal subTotal, string? PaymentIntenetId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            items = Items;
            SubTotal = subTotal;
            PaymentIntentId = PaymentIntenetId;
        }



        public string UserEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.pending;

        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }  //Navigational Property
        public int DeliveryMethodId { get; set; } //FK
        public ICollection<OrderItem> items { get; set; } //Navigational Property

        public Decimal SubTotal { get; set; } // = Price * Quantity


        //public Decimal Total { get; set; } // = SubTotal + Delivary Method Cost

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;

        public string? PaymentIntentId { get; set; }
    }
}
