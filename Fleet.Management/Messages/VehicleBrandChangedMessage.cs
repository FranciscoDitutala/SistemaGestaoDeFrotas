using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages;

public class VehicleBrandChangedMessage : ValueChangedMessage<VehicleBrandPayload>
{
    public VehicleBrandChangedMessage(VehicleBrandPayload value) : base(value)
    {
    }
}
