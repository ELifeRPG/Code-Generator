using ELifeRPG.CodeGenerator.DataTypes;

namespace ELifeRPG.CodeGenerator.Templates;

public class ClassDataTypeBody : TemplateModelBase
{
    public ClassDataTypeBody(EnfusionClassDataType type)
    {
        Type = type;
    }
    
    public EnfusionClassDataType Type { get; }
}