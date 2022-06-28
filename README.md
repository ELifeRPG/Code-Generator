# CodeGenerators
Tool which takes an OpenAPI json and creates Enfusion structs/classes out of it.

## Install

### Adding the package registry 

The package is available from GitHub Packages. There is one major downside of using GH Packages: despite being a public package, you need to authenticate against the registry (you can read more of this quirks [here](https://github.community/t/download-from-github-package-registry-without-authentication/14407)).

To consume the packages from our GitHub registry, simple add a new source specifying your credentials `dotnet nuget add source --username "YOURUSER" --password "YOURPASSWORD" --name ELifeRPG "https://nuget.pkg.github.com/ELifeRPG/index.json"`.
Alternatively, you can [use a personal-access-token](https://github.com/settings/tokens/new) (scope: `read:packages`) using `PublicToken` as `username` parameter and the token as `password` parameter. Kind notice: do not commit the PAT as it will be [revoked immediately](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/token-expiration-and-revocation#token-revoked-when-pushed-to-a-public-repository-or-public-gist).


### Install the dotnet tool

To make the tool available on your machine, execute the following command: `dotnet tool install -g ELifeRPG.CodeGenerator.DotNetTool`
If you want to specify a prerelease or want to use a specific version, please head to the [dotnet-cli documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install).

## Usage

To get an overview of the tool, visit the inbuilt documentation by executing `enfusion-codegen --help`.

### Examples

#### Generate classes out of a file
`enfusion-codegen generate ./openapi.json --output ./out`

#### Generate classes from a remote file
`enfusion-codegen generate http://localhost/openapi.json --output ./out`
