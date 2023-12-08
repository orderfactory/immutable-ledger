namespace OrderFactory.ImmutableLedger.Business;

public class Phone(
    Guid rootId,
    DateTime dateCreated,
    Guid recordId,
    bool deleted,
    Guid? basedOnRecordId,
    string phoneNumber
) : Entity(
    rootId, dateCreated, recordId, deleted, basedOnRecordId)
{
    public string PhoneNumber { get; } = phoneNumber;
}