echo "Restore dependencies and tools for the project "
dotnet restore
echo "Building the solution...."
dotnet build CompanyName.GatewayManagement.sln
echo "Running unit tests...."
dotnet test --no-restore .\tests\CompanyName.GatewayManagement.Domain.Tests\CompanyName.GatewayManagement.Domain.Tests
dotnet publish CompanyName.GatewayManagement.sln
PAUSE 
