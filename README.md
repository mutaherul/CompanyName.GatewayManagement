# Job Interview Assignments
## Project Description 
### .NET CORE REST API SAMPLE PROJECT
This project is managing gateways - master devices that control multiple peripheral devices.
This is a REST service (JSON/HTTP) for storing information about these gateways and
their associated devices. This information must be stored in the database.
When storing a gateway, any field marked as “to be validated” must be validated and an error returned if it
is invalid. Also, no more that 10 peripheral devices are allowed for a gateway.
The service must also offer an operation for displaying information about all stored gateways (and their
devices) and an operation for displaying details for a single gateway. Finally, it must be possible to add and
remove a device from a gateway.
Each gateway has:
- a unique serial number (string),
- human-readable name (string),
- IPv4 address (to be validated),
- multiple associated peripheral devices.
 
Each peripheral device has:
- a UID (number),
- vendor (string),
- date created,
- status - online/offline.



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
This project based on NET 5 SDK
There is PowerShell script at scripts\Setup to install the framework.


#### Building 
There is a batch file build.bat for building and running unit tests of the solution
```sh
dotnet build CompanyName.GatewayManagement.sln
```
### Running Tests
```sh
dotnet test --no-restore .\tests\CompanyName.GatewayManagement.Domain.Tests\CompanyName.GatewayManagement.Domain.Tests.csproj
```



#### Database: Microsoft SQL Server 
    Database name: GATEWAYDB
    Database user name: gatewaydb_dba

- Note: database username and password is found in DatabaseConnection  on appsettings.dev.
 
 There are two scripts on \scripts\Database folder. 
 - Schema.sql: To create database schema ( db user and tables, constraints)
 - sample_data.sql: To populate some sample data


##### Configuration
Project configurations are kept in appsettings.dev file on CompanyName.GatewayManagement.Api project.


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
 



