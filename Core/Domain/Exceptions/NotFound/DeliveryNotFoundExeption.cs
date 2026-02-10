namespace Domain.Exceptions.NotFound
{
    public class DeliveryNotFoundExeption(int id) : NotFoundException($"The Delivery With Id {id} Was Not Found")
    {
    }
}
