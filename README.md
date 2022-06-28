# CodeGenerators
Tool which takes an OpenAPI json and creates Enfusion structs/classes out of it.

## Install

### Adding the package registry 

The package is available from GitHub Packages. There is one major downside of using GH Packages: depsite being a public package, you need to authenticate against the registry (you can read more of this quirks [here](https://github.community/t/download-from-github-package-registry-without-authentication/14407)).

To consume the packages from our GitHub registry, simple add a new source `dotnet nuget add source --username "PublicToken" --password "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" --store-password-in-clear-text --name ELifeRPG "https://nuget.pkg.github.com/ELifeRPG/index.json"`.
Alternatively, you can also change your `nuget.config` and merge it with the following content:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <add key="ELifeRPG" value="https://nuget.pkg.github.com/ELifeRPG/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <ELifeRPG>
            <add key="Username" value="PublicToken" />
            <add key="ClearTextPassword" value="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" />
        </ELifeRPG>
    </packageSourceCredentials>
</configuration>
```

### Install the dotnet tool

To make the tool available on your machine, execute the following command: `dotnet tool install -g ELifeRPG.CodeGenerator.DotNetTool`
If you want to specify a prerelease or want to use a specific version, please head to the [dotnet-cli documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install).

## Usage

To get an overview of the tool, visit the builtin documentation by executing `dotnet `

### Examples

#### Generate classes out of a file
`enfusion codegen generate ./openapi.json --output ./out`

#### Generate classes from a remote file
`enfusion codegen generate http://localhost/openapi.json --output ./out`
