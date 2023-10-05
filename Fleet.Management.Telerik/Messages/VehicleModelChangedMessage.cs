using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages;

internal class VehicleModelChangedMessage : ValueChangedMessage<VehicleModelPayload>
{
    public VehicleModelChangedMessage(VehicleModelPayload value) : base(value)
    {
    }
}
