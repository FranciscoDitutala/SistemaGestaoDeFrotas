using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fleet.Management.Messages;

public class VehicleVariantChangedMessage : ValueChangedMessage<VehicleVariantPayload>
{
    public VehicleVariantChangedMessage(VehicleVariantPayload value) : base(value)
    {
    }
}
