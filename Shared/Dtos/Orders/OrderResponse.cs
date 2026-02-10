using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Orders
{
    public class OrderResponse
    {

        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }  //Delivery Method Name
        public ICollection<OrderItemDto> items { get; set; } 
        public Decimal SubTotal { get; set; } // = Price * Quantity
        public Decimal Total { get; set; } // = SubTotal + Delivary Method Cost

    }
}
