# round-up-feature

Ozgur Tezel

# Running Localy

## Prerequisites

Please download and install dotnet 6 SDK to build and run this application locally. Links are provided below:

[for MacOs](https://download.visualstudio.microsoft.com/download/pr/a048efaf-c4ee-4c22-a987-2bda2a58253c/3ddde00d1b2cf420a0d870df26bc56f8/dotnet-sdk-6.0.300-osx-x64.pkg)

[for Windows](https://download.visualstudio.microsoft.com/download/pr/deb4711b-7bbc-4afa-8884-9f2b964797f2/fb603c451b2a6e0a2cb5372d33ed68b9/dotnet-sdk-6.0.300-win-x64.exe)

After the installation of dotnet 6 CLI, please verify the installation by running the following command in terminal. It should return the version of dotnet that is installed.

`dotnet --version`

## Build and Run the application

At the root of the project, run the following command:

For Windows:
`dotnet run --project .\src\RoundUpFeature.Api\RoundUpFeature.Api.csproj`
For MacOS:
`dotnet run --project ./src/RoundUpFeature.Api/RoundUpFeature.Api.csproj`

Application will first build and then run. It will expose the configured ports in `launchSettings.json`, which can be re-configured if there is a conflict.

## Accessing to the exposed endpoint

Application has swagger installed and will present with swagger UI, which documents the exposed endpoint and provides schema information.

Please navigate to the following address via your browser to access Swagger:

http://localhost:5068/swagger/index.html

Alternatively we can send HttpRequests via following curl command:

`curl -X 'GET' 'http://localhost:5068/RoundUp'`
