using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages;

public class PartChangedMessage : ValueChangedMessage<PartPayload>
{
    public PartChangedMessage(PartPayload value) : base(value)
    {
    }
}
