namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExists(string email);
    }
}
