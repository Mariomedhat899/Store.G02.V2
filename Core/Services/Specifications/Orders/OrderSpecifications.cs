using Domain.Entites.Orders;

namespace Services.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Guid, Order>
    {
        public OrderSpecifications(Guid id, string userEmail) : base(O => O.Id == id && O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.items);

        }
        public OrderSpecifications(string userEmail) : base(O => O.UserEmail.ToLower() == userEmail.ToLower())
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.items);

            AddOrderByDescending(O => O.OrderDate);

        }
    }
}
