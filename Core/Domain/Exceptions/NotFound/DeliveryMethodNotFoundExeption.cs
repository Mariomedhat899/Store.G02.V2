namespace Domain.Exceptions.NotFound
{
    public class DeliveryMethodNotFoundExeption(int id) : NotFoundException($"Delivery Method With Id : {id} Was Not Found !!")
    {
    }
}
