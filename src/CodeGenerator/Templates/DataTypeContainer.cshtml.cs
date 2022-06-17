using ELifeRPG.CodeGenerator.DataTypes;

namespace ELifeRPG.CodeGenerator.Templates;

public class DataTypeContainer : TemplateModelBase
{
    public DataTypeContainer(EnfusionClassDataType dataType)
    {
        ClassDataType = dataType;
        DataType = dataType;
    }
    
    public DataTypeContainer(EnfusionEnumDataType dataType)
    {
        EnumDataType = dataType;
        DataType = dataType;
    }

    public EnfusionDataType DataType { get; }

    public EnfusionClassDataType? ClassDataType { get; }

    public EnfusionEnumDataType? EnumDataType { get; }
}