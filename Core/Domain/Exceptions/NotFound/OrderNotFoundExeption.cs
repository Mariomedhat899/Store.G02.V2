namespace Domain.Exceptions.NotFound
{
    public class OrderNotFoundExeption(Guid id) : NotFoundException($"Order With Id : {id} Was Not Found !!")
    {
    }
}
