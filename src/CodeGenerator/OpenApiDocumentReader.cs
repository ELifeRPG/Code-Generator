using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace ELifeRPG.CodeGenerator;

public class OpenApiDocumentReader
{
    private readonly string _filePath;

    public OpenApiDocumentReader(string filePath)
    {
        _filePath = filePath;
    }

    public (OpenApiDocument document, OpenApiDiagnostic diagnostic) Read()
    {
        var settings = new OpenApiReaderSettings { ReferenceResolution = ReferenceResolutionSetting.DoNotResolveReferences };
        var document = new OpenApiStreamReader(settings).Read(new FileStream(_filePath, FileMode.Open), out OpenApiDiagnostic diagnostic);
        return (document, diagnostic);
    }
}