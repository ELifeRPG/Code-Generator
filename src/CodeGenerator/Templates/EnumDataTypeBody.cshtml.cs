using ELifeRPG.CodeGenerator.DataTypes;

namespace ELifeRPG.CodeGenerator.Templates;

public class EnumDataTypeBody : TemplateModelBase
{
    public EnumDataTypeBody(EnfusionEnumDataType type)
    {
        Type = type;
    }
    
    public EnfusionEnumDataType Type { get; }
}