namespace EBCustomerTask.Core.Interfaces
{
    public interface ICustomerRepositoryContext
    {
        Task<ICustomerRepository> GetRepositoryAsync();
    }
}
