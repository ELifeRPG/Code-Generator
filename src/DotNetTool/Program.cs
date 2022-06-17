using RazorLight;
using ELifeRPG.CodeGenerator;

var (document, _) = new OpenApiDocumentReader("swagger.json").Read();

var engine = new RazorLightEngineBuilder()
    .UseEmbeddedResourcesProject(typeof(OpenApiDocumentReader).Assembly, "ELifeRPG.CodeGenerator.Templates")
    .UseMemoryCachingProvider()
    .Build();

foreach (var path in document.Components.Schemas)
{
    var result = await engine.CompileRenderAsync("DataTypeContainer", TemplateModelFactory.GetContainer(path));
    Console.WriteLine(result);
}
