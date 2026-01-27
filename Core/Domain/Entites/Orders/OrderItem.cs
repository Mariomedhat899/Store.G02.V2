using Domain.Entites.Products;

namespace Domain.Entites.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductInOrderItem product, decimal price, decimal quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrderItem  Product { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}