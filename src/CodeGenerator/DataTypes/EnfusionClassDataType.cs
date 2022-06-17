using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace ELifeRPG.CodeGenerator.DataTypes;

public class EnfusionClassDataType : EnfusionDataType
{
    public EnfusionClassDataType(string key, OpenApiSchema schema, EnfusionType type)
        : base(key, type)
    {
        Properties = schema.Properties
            .Select(x =>
            {
                var mappedType = new EnfusionType(x.Value);
                return new KeyValuePair<string, string>(x.Key, mappedType.Name);
            })
            .ToList();
    }
    
    public List<KeyValuePair<string, string>> Properties { get; }
}