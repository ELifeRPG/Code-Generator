using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace ELifeRPG.CodeGenerator.DataTypes;

public class EnfusionEnumDataType : EnfusionDataType
{
    public EnfusionEnumDataType(string key, OpenApiSchema schema, EnfusionType type)
        : base(key, type)
    {
        Values = BuildValues(schema).ToList();
    }
    
    public ICollection<KeyValuePair<string, int>> Values { get; }

    private static IEnumerable<KeyValuePair<string, int>> BuildValues(OpenApiSchema schema)
    {
        var enumNames = GetEnumNames(schema).ToList();

        for (var i = 0; i < schema.Enum.Count; i++)
        {
            var schemaEnum = schema.Enum[i];
            if (schemaEnum is not OpenApiInteger integerValue)
            {
                continue;
            }

            yield return new KeyValuePair<string, int>(enumNames[i], integerValue.Value);
        }
    }

    private static IEnumerable<string> GetEnumNames(OpenApiSchema schema)
    {
        var enumNames = (OpenApiArray)schema.Extensions["x-enumNames"];
        foreach (var item in enumNames)
        {
            if (item is not OpenApiString enumName)
            {
                continue;
            }

            yield return enumName.Value;
        }
    }
}