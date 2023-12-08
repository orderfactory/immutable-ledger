namespace OrderFactory.ImmutableLedger.Business;

public class CustomerPhone(
    Guid rootId,
    DateTime dateCreated,
    Guid recordId,
    bool deleted,
    Guid? basedOnRecordId,
    Guid customerId,
    Guid phoneId
) : Entity(
    rootId, dateCreated, recordId, deleted, basedOnRecordId)
{
    public Guid CustomerId { get; } = customerId;
    public Guid PhoneId { get; } = phoneId;
}