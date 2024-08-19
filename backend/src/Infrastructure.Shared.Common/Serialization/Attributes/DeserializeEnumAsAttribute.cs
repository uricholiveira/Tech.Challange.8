using Infrastructure.Shared.Common.Serialization.Enums;

namespace Infrastructure.Shared.Common.Serialization.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class DeserializeEnumAsAttribute : Attribute
{
    public DeserializeEnumAsAttribute(CasingOption casingOption)
    {
        CasingOption = casingOption;
    }

    public CasingOption CasingOption { get; }
}