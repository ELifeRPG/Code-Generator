using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ELifeRPG.CodeGenerator.DataTypes;
using Xunit;

namespace UnitTests;

public class EnfusionTypeTests
{
    [Fact]
    public void DataTypeName_ReturnsFloat_WhenOfTypeNumber()
    {
        var schema = new OpenApiSchema { Type = "number" };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("float", enfusionType.Name);
    }
    
    [Fact]
    public void DataTypeName_ReturnsInt_WhenOfTypeInteger()
    {
        var schema = new OpenApiSchema { Type = "integer" };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("int", enfusionType.Name);
    }

    [Fact]
    public void DataTypeName_ReturnsEnum_WhenOfTypeIntegerWithEnums()
    {
        var schema = new OpenApiSchema { Type = "integer", Enum = new List<IOpenApiAny>{ new OpenApiInteger(1) } };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("enum", enfusionType.Name);
    }

    [Fact]
    public void DataTypeName_ReturnsClass_WhenOfTypeObject()
    {
        var schema = new OpenApiSchema { Type = "object" };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("class", enfusionType.Name);
    }
    
    [Fact]
    public void DataTypeName_ReturnsArrayOfT_WhenOfTypeArray()
    {
        var schema = new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Reference = new OpenApiReference { Id = "somedto" } }};
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("ref array<ref somedto>", enfusionType.Name);
    }
    
    [Fact]
    public void DataTypeName_ReturnsObjectTypeName_WhenOfKindReference()
    {
        var schema = new OpenApiSchema { Type = null, Reference = new OpenApiReference { Id = "refdto" }};
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("refdto", enfusionType.Name);
    }
    
    [Fact]
    public void DataTypeName_ReturnsObjectTypeName_WhenReferenceIsAlsoGiven()
    {
        var schema = new OpenApiSchema { Type = "typedto", Reference = new OpenApiReference { Id = "refdto" }};
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("typedto", enfusionType.Name);
    }

    [Fact]
    public void BaseDataType_ReturnsJsonApiStruct_WhenOfTypeObject()
    {
        var schema = new OpenApiSchema { Type = "object" };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("JsonApiStruct", enfusionType.BaseName);
    }
    
    [Fact]
    public void BaseDataType_ReturnsNull_WhenOfTypeEnum()
    {
        var schema = new OpenApiSchema { Type = "integer" };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Null(enfusionType.BaseName);
    }
    
    [Theory]
    [InlineData("string")]
    [InlineData("integer")]
    public void DefaultValue_ReturnsNull_WhenOpenApiTypeIsSimpleType(string openApiType)
    {
        var schema = new OpenApiSchema { Type = openApiType };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Null(enfusionType.DefaultValue);
    }
    
    [Fact]
    public void DefaultValue_ReturnsValue_WhenOfTypeArray()
    {
        var schema = new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Reference = new OpenApiReference { Id = "somedto" } } };
        
        var enfusionType = new EnfusionType(schema);
        
        Assert.Equal("{}", enfusionType.DefaultValue);
    }
}
