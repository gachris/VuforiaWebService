# Vuforia Web Service API Client

## Overview
This repository contains a C# client for interacting with the Vuforia Web Service API. The client provides methods for managing targets within a Vuforia database, allowing for actions such as listing targets, retrieving reports, and managing target data.

## Features
- **Authentication**: Utilize `ServerAccessKeys` for secure access to the Vuforia API.
- **Manage Targets**: List, retrieve, update, and delete targets from the Vuforia database.
- **Database Reports**: Get summary reports for the database and target summaries.
- **Similarity Check**: Check for similar targets in the database.

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- A Vuforia account with API access to retrieve your access and secret keys.

### Installation

To use `VuforiaWebService` in your project, install it via NuGet:

```bash
dotnet add package VuforiaWebService.Api.Core
dotnet add package VuforiaWebService.Api.Auth
dotnet add package VuforiaWebService.Api.Target
```

Alternatively, you can clone this repository and build the project locally:

```bash
git clone https://github.com/gachris/VuforiaWebService.git
cd VuforiaWebService
dotnet build
```

### Configuration
Replace `ACCESS_KEY` and `SECRET_KEY` in the `GetKeys` method with your actual Vuforia API keys.

```csharp
private static ServerAccessKeys GetKeys()
{
    if (_keys == null)
    {
        _keys = new ServerAccessKeys("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY");
    }
    return _keys;
}
```

### Usage
Run the `Main` method to execute various operations against the Vuforia Web Service API. The sample code demonstrates how to:
- List all targets.
- Get a summary report of the database.
- Check for similar targets.
- Insert, update, and delete targets.

### Example
Here is a simplified version of the `Main` method demonstrating how to use the API client:

```csharp
private static void Main()
{
    try
    {
        ServerAccessKeys keys = GetKeys();
        TargetListResource resource = new TargetListResource(GetService());
        
        // Example: List all targets
        var vuforiaGetAllResponse = resource.List(keys).Execute();
        
        // Example: Get database summary report
        var vuforiaGetDatabaseSummaryReportResponse = resource.GetDatabaseSummaryReport(keys).Execute();
        
        // Further operations...
    }
    catch (VuforiaPortalApiException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.Error.ResultCode);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
```

## Exception Handling
The client includes error handling for API exceptions. You can catch `VuforiaPortalApiException` to handle specific Vuforia API errors.

## Contributing

Contributions are welcome! Please follow these steps if you wish to contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Write clean, readable code and add comments where necessary.
4. Submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.