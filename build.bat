echo "Restore dependencies and tools for the project "
dotnet restore
echo "Building the solution...."
dotnet build MusalaSoft.GatewayManagement.sln
echo "Running unit tests...."
dotnet test --no-restore .\tests\MusalaSoft.GatewayManagement.Domain.Tests\MusalaSoft.GatewayManagement.Domain.Tests.csproj
dotnet publish MusalaSoft.GatewayManagement.sln
PAUSE 