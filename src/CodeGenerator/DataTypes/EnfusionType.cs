﻿using Microsoft.OpenApi.Models;

namespace ELifeRPG.CodeGenerator.DataTypes;

public class EnfusionType
{
    private readonly OpenApiSchema _schema;

    public EnfusionType(OpenApiSchema schema)
    {
        _schema = schema;
        Name = GetName();
        BaseName = GetBaseName();
    }

    public string Name { get; }
    
    public string? BaseName { get; }

    private string GetName()
    {
        if (!string.IsNullOrEmpty(_schema.Type))
        {
            return _schema.Type switch
            {
                "integer" => GetIntegerTypeName(),
                "object" => "class",
                "array" => GetArrayTypeName(),
                _ => _schema.Type
            };
        }
        
        if (!string.IsNullOrEmpty(_schema.Reference?.Id))
        {
            return _schema.Reference.Id;
        }

        return string.Empty;
    }
    
    private string? GetBaseName()
    {
        if (_schema.Type == "integer")
        {
            return null;
        }
        
        return _schema.Type != "object" ? _schema.Type : "JsonApiStruct";
    }

    private string GetArrayTypeName()
    {
        return $"array<{_schema.Items.Reference.Id}>";
    }

    private string GetIntegerTypeName()
    {
        return _schema.Enum is { Count: 0 } ? "integer" : "enum";
    }
}