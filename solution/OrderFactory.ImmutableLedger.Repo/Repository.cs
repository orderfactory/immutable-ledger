using System.Data;
using Dapper;
using OrderFactory.ImmutableLedger.Business;

namespace OrderFactory.ImmutableLedger.Repo;

public interface IRepository
{
    Task<int> SaveCustomer(Customer customer);
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
                       ,[LastName])
                 VALUES
                       (@RootId
                       ,@DateCreated
                       ,@RecordId
                       ,@Deleted
                       ,@BasedOnRecordId
                       ,@FirstName
                       ,@MiddleName
                       ,@LastName);

            select @@ROWCOUNT;

            """;
        return Connection().ExecuteScalarAsync<int>(sql, customer);
    }

    private IDbConnection Connection()
    {
        return connectionFactory.CreateConnection();
    }
}