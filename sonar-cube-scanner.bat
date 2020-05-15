dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="TestResults/" /p:CoverletExcludebyAttribute=ExcludeFromCodeCoverageAttribute
dotnet sonarscanner begin /k:xunit-test-paises /d:sonar.cs.opencover.reportsPaths=*/TestResults/coverage.opencover.xml
dotnet build
dotnet sonarscanner end
