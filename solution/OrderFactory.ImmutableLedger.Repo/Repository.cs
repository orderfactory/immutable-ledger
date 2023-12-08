using System.Data;
using Dapper;
using OrderFactory.ImmutableLedger.Business;

namespace OrderFactory.ImmutableLedger.Repo;

public interface IRepository
{
    Task<int> SaveCustomer(Customer customer);
    Task<int> SavePhone(Phone phone);
    Task<int> SaveCustomerPhone(CustomerPhone customerPhone);
}

public class Repository(ISqlConnectionFactory connectionFactory) : IRepository
{
    public Task<int> SaveCustomer(Customer customer)
    {
        const string sql =
            """

            if not exists (select 1 from [dbo].[CustomerRoot] where [Id] = @RootId)
            begin
            	INSERT INTO [dbo].[CustomerRoot]
            			   ([Id]
            			   ,[DateCreated])
            		 VALUES
            			   (@RootId
            			   ,@DateCreated)
            end;

            INSERT INTO [dbo].[CustomerData]
                       ([RootId]
                       ,[DateCreated]
                       ,[RecordId]
                       ,[Deleted]
                       ,[BasedOnRecordId]
                       ,[FirstName]
                       ,[MiddleName]
                       ,[LastName]
                       ,[DefaultPhoneId])
                 VALUES
                       (@RootId
                       ,@DateCreated
                       ,@RecordId
                       ,@Deleted
                       ,@BasedOnRecordId
                       ,@FirstName
                       ,@MiddleName
                       ,@LastName
                       ,@DefaultPhoneId);

            select @@ROWCOUNT;

            """;
        return Connection().ExecuteScalarAsync<int>(sql, customer);
    }

    public Task<int> SavePhone(Phone phone)
    {
        const string sql =
            """

            if not exists (select 1 from [dbo].[PhoneRoot] where [Id] = @RootId)
            begin
            	INSERT INTO [dbo].[PhoneRoot]
            			   ([Id]
            			   ,[DateCreated])
            		 VALUES
            			   (@RootId
            			   ,@DateCreated)
            end;

            INSERT INTO [dbo].[PhoneData]
                       ([RootId]
                       ,[DateCreated]
                       ,[RecordId]
                       ,[Deleted]
                       ,[BasedOnRecordId]
                       ,[PhoneNumber])
                 VALUES
                       (@RootId
                       ,@DateCreated
                       ,@RecordId
                       ,@Deleted
                       ,@BasedOnRecordId
                       ,@PhoneNumber);

            select @@ROWCOUNT;

            """;
        return Connection().ExecuteScalarAsync<int>(sql, phone);
    }

    public Task<int> SaveCustomerPhone(CustomerPhone customerPhone)
    {
        const string sql =
            """

            if not exists (select 1 from [dbo].[CustomerPhoneRoot] where [Id] = @RootId)
            begin
            	INSERT INTO [dbo].[CustomerPhoneRoot]
            			   ([Id]
            			   ,[DateCreated])
            		 VALUES
            			   (@RootId
            			   ,@DateCreated)
            end;

            INSERT INTO [dbo].[CustomerPhoneData]
                       ([RootId]
                       ,[DateCreated]
                       ,[RecordId]
                       ,[Deleted]
                       ,[BasedOnRecordId]
                       ,[CustomerId]
                       ,[PhoneId])
                 VALUES
                       (@RootId
                       ,@DateCreated
                       ,@RecordId
                       ,@Deleted
                       ,@BasedOnRecordId
                       ,@CustomerId
                       ,@PhoneId);

            select @@ROWCOUNT;

            """;
        return Connection().ExecuteScalarAsync<int>(sql, customerPhone);
    }

    private IDbConnection Connection()
    {
        return connectionFactory.CreateConnection();
    }
}