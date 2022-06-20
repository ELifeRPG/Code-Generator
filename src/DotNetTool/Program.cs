using System.CommandLine;
using ELifeRPG.CodeGenerator.DotNetTool.Commands;

var rootCommand = new RootCommand { new GenerateCommand() };
await rootCommand.InvokeAsync(args);
