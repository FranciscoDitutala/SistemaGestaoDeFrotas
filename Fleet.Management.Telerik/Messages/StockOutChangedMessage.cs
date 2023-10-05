using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages
{
    public class StockOutChangedMessage : ValueChangedMessage<StockOutPayload>
    {
        public StockOutChangedMessage(StockOutPayload value) : base(value)
        {
        }
    }
}
