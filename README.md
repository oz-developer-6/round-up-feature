# round-up-feature

Ozgur Tezel

# Running Localy

## Prerequisites

Please download and install dotnet 6 SDK to build and run this application locally. Links are provided below:

[for MacOs](https://download.visualstudio.microsoft.com/download/pr/a048efaf-c4ee-4c22-a987-2bda2a58253c/3ddde00d1b2cf420a0d870df26bc56f8/dotnet-sdk-6.0.300-osx-x64.pkg)

[for Windows](https://download.visualstudio.microsoft.com/download/pr/deb4711b-7bbc-4afa-8884-9f2b964797f2/fb603c451b2a6e0a2cb5372d33ed68b9/dotnet-sdk-6.0.300-win-x64.exe)

After the installation of dotnet 6 CLI, please verify the installation by running the following command in terminal. It should return the version of dotnet that is installed.

`dotnet --version`

## Refresh Access Token

Please refresh the Access Token in `program.cs` as auto refresh is not implemented in this MVP version.

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

`curl -X 'GET' \
  'http://localhost:5068/RoundUp?weekCommencingInput=2022%2F05%2F21d' \
  -H 'accept: */*''`

  ## Get/RoundUp
  
  This endpoint will accept a valid dateTime string in one of the following formats
  `YYYY/MM/DD` or `yyyy-MM-ddTHH:mm:ss.000Z`

  Once a valid date format is entered, this will act as a starting day of the week and application will apply round up calculation to all out transactions on the primary account.

  If user has a savingsGoal account total of the round-up amount from all transactions in the given period will be added to their savingsGoal account.

  If user does not have a savingsGoal account a new one with the name `Holiday Savings` with target value of `£185.00`, and total round-up value will be added to this new savingsGoal account.

  After a successful amount transfer, endpoint will return a success message with accountUid. We can use this accountUid to check our savingsGoal accounts via the below endpoint

  ## Get/RoundUp/SavingsGoals

  This endpoint will display `name`, `target amount`, `totalSaved amount` and `savedPercentage` information for the users all savingsAccount.

  We will need the `accountUid` to invoke this enpoint, which can be retrieved from the result of the above endpoint request call.

  ## Running Unit Tests
  
  At the root of the project please run the following command
  `dotnet test`
