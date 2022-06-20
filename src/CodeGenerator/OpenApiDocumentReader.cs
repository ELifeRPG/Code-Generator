using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace ELifeRPG.CodeGenerator;

public class OpenApiDocumentReader
{
    private static readonly OpenApiReaderSettings Settings = new() { ReferenceResolution = ReferenceResolutionSetting.DoNotResolveReferences };
    private readonly HttpClient _httpClient;

    public OpenApiDocumentReader()
    {
        _httpClient = new HttpClient();
    }
    
    public (OpenApiDocument Document, OpenApiDiagnostic Diagnostic) Read(string filePath)
    {
        var stream = new FileStream(filePath, FileMode.Open);
        return ReadInternal(stream);
    }
    
    public async Task<(OpenApiDocument Document, OpenApiDiagnostic Diagnostic)> ReadFromUri(Uri fileUri)
    {
        var stream = await _httpClient.GetStreamAsync(fileUri);
        return ReadInternal(stream);
    }

    private static (OpenApiDocument Document, OpenApiDiagnostic Diagnostic) ReadInternal(Stream input)
    {
        var document = new OpenApiStreamReader(Settings).Read(input, out OpenApiDiagnostic diagnostic);
        return (document, diagnostic);
    }
}
