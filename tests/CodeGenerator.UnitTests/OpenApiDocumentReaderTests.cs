using System.Linq;
using ELifeRPG.CodeGenerator;
using Xunit;

namespace UnitTests;

public class OpenApiDocumentReaderTests
{
    [Fact]
    public void Read_IncludesItemTypeName()
    {
        var reader = new OpenApiDocumentReader();
        var (document, _) = reader.Read("swagger.json");
        
        Assert.Contains(document.Components.Schemas, s => !string.IsNullOrEmpty(s.Value.Type));
    }
    
    [Fact]
    public void Read_IncludesArrayReferenceName()
    {
        var reader = new OpenApiDocumentReader();
        var (document, _) = reader.Read("swagger.json");
        
        Assert.Contains(document.Components.Schemas, s => s.Value.Properties?.Any(p => !string.IsNullOrEmpty(p.Value.Reference?.Id)) ?? false);
    }
}
