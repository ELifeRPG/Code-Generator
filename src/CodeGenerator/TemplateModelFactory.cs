using System;
using System.Collections.Generic;
using ELifeRPG.CodeGenerator.DataTypes;
using ELifeRPG.CodeGenerator.Templates;
using Microsoft.OpenApi.Models;

namespace ELifeRPG.CodeGenerator;

public static class TemplateModelFactory
{
    public static DataTypeContainer GetContainer(KeyValuePair<string, OpenApiSchema> path)
    {
        var enfusionDataType = new EnfusionType(path.Value);
        return enfusionDataType.Name switch
        {
            "class" => new DataTypeContainer(new EnfusionClassDataType(path.Key, path.Value, enfusionDataType)),
            "enum" => new DataTypeContainer(new EnfusionEnumDataType(path.Key, path.Value, enfusionDataType)),
            _ => throw new Exception(),
        };
    }
}