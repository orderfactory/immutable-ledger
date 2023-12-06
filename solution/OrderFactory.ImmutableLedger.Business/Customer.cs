namespace OrderFactory.ImmutableLedger.Business;

public class Customer(Guid rootId, DateTime dateCreated, Guid recordId, bool deleted, Guid? basedOnRecordId,
    string firstName, string? middleName, string lastName
) : Entity(
    rootId, dateCreated, recordId, deleted, basedOnRecordId)
{
    public string FirstName { get; } = firstName;
    public string? MiddleName { get; } = middleName;
    public string LastName { get; } = lastName;
}