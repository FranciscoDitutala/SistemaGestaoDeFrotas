using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages;

internal class StockEntryChangedMessage : ValueChangedMessage<StockEntryPayload>
{
    public StockEntryChangedMessage(StockEntryPayload value) : base(value)
    {
    }
}
