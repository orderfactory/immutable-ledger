namespace OrderFactory.ImmutableLedger.Business;

public class Entity(Guid rootId, DateTime dateCreated, Guid recordId, bool deleted, Guid? basedOnRecordId)
{
    public Guid RootId { get; } = rootId;
    public DateTime DateCreated { get; } = dateCreated;
    public Guid RecordId { get; } = recordId;
    public bool Deleted { get; } = deleted;
    public Guid? BasedOnRecordId { get; } = basedOnRecordId;
}