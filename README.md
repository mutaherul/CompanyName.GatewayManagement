## Architecture
Use Clean architecture.


#### CompanyName.GatewayManagement.Api
.Net Core Web Application which contails the api. This is the Outer layer. 
    Dependant projects
    - CompanyName.GatewayManagement.Data
#### CompanyName.GatewayManagement.Domain
 .Net Core Class Library project contains the domain services. It is domain layer. This layer has all business login and use repository to persisnt data using data layer.This project used AutoMapper library to mapped one object to another.
 Dependant projects    
    - CompanyName.GatewayManagement.Data
#### CompanyName.GatewayManagement.Data
 .Net Core Class Library project contains the data repository and entity.This project uses Entity Framework core as an ORM.

#### CompanyName.GatewayManagement.Domain.Test
 .Net Core Class Library project contains all unit test for domain layer.This project uses Microsoft.NET.Test.Sdk, xunit for unit test and Moq for mocking the object.

### Clean Architecture
Source code dependencies can only point inwards. Nothing in an inner circle can know anything at all about something in an outer circle.


## Installation
This project based on NET Core 3.1 SDK
There is powershell script at scripts\Setup to install the framework.


#### Building 
There is a batch file build.bat for bulding and running unit tests of the solution
```sh
dotnet build MusalaSoft.GatewayManagement.sln
```
### Running Tests
```sh
dotnet test --no-restore .\tests\MusalaSoft.GatewayManagement.Domain.Tests\MusalaSoft.GatewayManagement.Domain.Tests.csproj
```



#### Database: Microsoft SQL Server 
    Database name: GATEWAYDB
    Database user name: gatewaydb_dba

- Note: database username and password is found in DatabaseConnection  on appsettings.dev.
 
 There are two script on \scripts\Database folder. 
 - Schema.sql: To create database schema ( db user and tables, constrains)
 - sample_data.sql: To populate some sample data


##### Configuration
Project configurations are kept in appsettings.dev file on MusalaSoft.GatewayManagement.Api project.


## Api Documentation
Swagger is tooling that uses the OpenAPI specification.
Swagger URL (IIS EXPRESS) : http://locahost:port/api/swagger

#### Gateway
######  Description: Get All Gateways with it's associated devices
 URL : /gateways
 Method  : GET 
 
###### Description: Get a specific Gateway according to it's id
URL : /gateways/{id}
Method  : GET

###### Description: Get a specific Gateway according to it's id
URL : /gateways/{id}
Method  : GET

###### Description:  Add new Gateway
URL :  /gateways
Method  : POST 

#### Device
######  Description: Get All devices of a gateway
 URL : /gateways/{id}/devices
 Method  : GET 

######  Description: Add new device under a gateway
 URL : /gateways/{id}/devices
 Method  : POST 
 
######  Description: Get a specific device according to it's UId
 URL : /gateways/{id}/devices/{deviceUid}
 Method  : GET 
 
######  Description: Delete a specific device according to it's UId
 URL : /gateways/{id}/devices/{deviceUid}
 Method  : DELETE
 



