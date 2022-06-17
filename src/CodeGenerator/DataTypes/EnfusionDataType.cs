namespace ELifeRPG.CodeGenerator.DataTypes;

public abstract class EnfusionDataType
{
    protected EnfusionDataType(string key, EnfusionType type)
    {
        TypeName = key;
        DataTypeName = type.Name;
        BaseDataType = type.BaseName;
    }

    public string DataTypeName { get; }
    
    public string? BaseDataType { get; }

    public string TypeName { get; }
}
