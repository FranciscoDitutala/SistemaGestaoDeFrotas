namespace Fleet.Parts;

public partial class StockOutPayload
{
    public DateTime RegisteredDateValue => RegisteredDate.ToDateTime();
    public DateTime LastUpdatedValue => LastUpdated.ToDateTime();

    public DateTime? ApprovedDateValue => ApprovedDate?.ToDateTime();
    public DateTime? DeliveredDateValue => DeliveredDate?.ToDateTime();

    public DateTime? CancelledDateValue => CancelledDate?.ToDateTime();

    public bool Approved => ApprovedDateValue.HasValue;
    public bool Delivered => DeliveredDateValue.HasValue;
    public bool Cancelled => CancelledDateValue.HasValue;
}
