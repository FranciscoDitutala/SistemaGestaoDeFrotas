namespace Fleet.Parts;

public partial class StockEntryPayload
{
    public DateTime RegisteredDateValue => RegisteredDate.ToDateTime();
    public DateTime LastUpdatedValue => LastUpdated.ToDateTime();
}
