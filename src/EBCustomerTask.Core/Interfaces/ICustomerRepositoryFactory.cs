namespace EBCustomerTask.Core.Interfaces
{
    public interface ICustomerRepositoryFactory
    {
        ICustomerRepository CreateSqlServerRepository();
        ICustomerRepository CreateMongoDbRepository();
    }
}
