using System.CommandLine;
using System.CommandLine.Invocation;
using ELifeRPG.CodeGenerator.Templates;
using Microsoft.OpenApi.Models;
using RazorLight;

namespace ELifeRPG.CodeGenerator.DotNetTool.Commands;

public class GenerateCommand : Command
{
    private readonly Argument<string> _inputArgument;
    private readonly Option<string> _outputOption;

    public GenerateCommand()
        : base("generate", "Generates Enfusion classes")
    {
        _inputArgument = new Argument<string>(name: "source", description: "File-Path or URL to get the OpenAPI json.");
        AddArgument(_inputArgument);
        
        _outputOption = new Option<string>(name: "--output", description: "Specifies the destination folder.");
        AddOption(_outputOption);

        this.SetHandler(Handle);
    }

    private async Task Handle(InvocationContext context)
    {
        var inputArgumentValue = context.ParseResult.GetValueForArgument(_inputArgument);
        var outputOptionValue = context.ParseResult.GetValueForOption(_outputOption) ?? Path.Join(Directory.GetCurrentDirectory(), "out");
        Console.WriteLine($"inputArgumentValue: {inputArgumentValue} -- outputOptionValue: {outputOptionValue}");

        var reader = new OpenApiDocumentReader();
        var document = Uri.TryCreate(inputArgumentValue, UriKind.Absolute, out var inputUri)
            ? (await reader.ReadFromUri(inputUri)).Document
            : reader.Read(inputArgumentValue).Document;

        await foreach (var item in HandleDocument(document))
        {
            await WriteFile(outputOptionValue, $"{item.Name}.c", item.Content);
        }
    }

    private static async IAsyncEnumerable<(string Name, string Content)> HandleDocument(OpenApiDocument document)
    {
        var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(OpenApiDocumentReader).Assembly, "ELifeRPG.CodeGenerator.Templates")
            .UseMemoryCachingProvider()
            .Build();

        foreach (var path in document.Components.Schemas)
        {
            yield return (path.Key, await engine.CompileRenderAsync(nameof(DataTypeContainer), TemplateModelFactory.GetContainer(path)));
        }
    }

    private static async Task WriteFile(string folderPath, string fileName, string content)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        await using var stream = new FileStream(Path.Join(folderPath, fileName), FileMode.Create);
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(content);
    }
}
