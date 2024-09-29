# Vuforia Web Service API Client

## Overview
This repository contains a C# client for interacting with the Vuforia Web Service API. The client provides methods for managing targets within a Vuforia database, allowing for actions such as listing targets, retrieving reports, and managing target data.

## Features
- **Authentication**: Utilize `ServerAccessKeys` for secure access to the Vuforia API.
- **Manage Targets**: List, retrieve, update, and delete targets from the Vuforia database.
- **Database Reports**: Obtain summary reports for the database and target summaries.
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

### Usage
Run the `Main` method to execute various operations against the Vuforia Web Service API. The sample code demonstrates how to:
- List All Targets: Retrieve a list of all targets in the database.
- Retrieve Specific Target: Get detailed information about a specific target based on its ID.
- Insert New Target: Add a new target to the database, including its properties such as name, width, and associated image.
- Update Existing Target: Modify the properties of an existing target identified by its ID.
- Delete Target: Remove a target from the database using its ID.
- Check Similar Targets: Identify targets that are similar to a specified target based on certain criteria.
- Get Target Summary Report: Retrieve a summary report for a specific target to gain insights into its details and performance.
- Get Database Summary Report: Get an overview report for the entire database, including statistics and status.

### Example
Here is a simplified version of the `Main` method demonstrating how to use the API client:

```csharp
private static void Main()
{
    try
    {
        var userCredential = new UserCredential();
        var initializer = new BaseClientService.Initializer()
        {
            ApplicationName = "VuforiaWebService",
            HttpClientInitializer = userCredential,
        };

        var keys = new ServerAccessKeys("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY");
        var targetService = new TargetService(initializer);

        // Example: List all targets
        var vuforiaGetAllResponse = targetService.TargetList.List(keys).Execute();
        Console.WriteLine("All Targets:");
        foreach (var target in vuforiaGetAllResponse.Results)
        {
            Console.WriteLine($"- ID: {target}");
        }

        // Example: Check for a specific target
        var vuforiaRetrieveResponse = targetService.TargetList.Get(keys, "<your_target_id>").Execute();
        Console.WriteLine("Target:");
        Console.WriteLine($"- Name: {vuforiaRetrieveResponse.TargetRecord.Name}");

        // Example: Insert a new target
        var newTarget = targetService.TargetList.Insert(keys, new PostTrackableRequest
        {
            Name = "NewSampleTarget",
            Width = 1,
            Image = "<path_to_your_image>",
            ActiveFlag = true
        }).Execute();
        Console.WriteLine($"Inserted New Target ID: {newTarget.TargetId}");

        // Example: Update an existing target
        var updatedTarget = targetService.TargetList.Update(keys, new UpdateTrackableRequest
        {
            Name = "UpdatedSampleTarget",
            Width = 2
        }, "<your_target_id>").Execute();
        Console.WriteLine($"Target updated successfully.");

        // Example: Delete a target
        targetService.TargetList.Delete(keys, "<your_target_id>").Execute();
        Console.WriteLine("Target deleted successfully.");

        // Example: Check for similar targets
        var similarTargetsResponse = targetService.TargetList.CheckSimilar(keys, "<your_target_id>").Execute();
        Console.WriteLine("Similar Targets:");
        foreach (var similarTarget in similarTargetsResponse.SimilarTargets)
        {
            Console.WriteLine($"- ID: {similarTarget}");
        }

        // Example: Get target summary report
        var vuforiaRetrieveTargetSummaryReportResponse = targetService.TargetList.RetrieveTargetSummaryReport(keys, "<your_target_id>").Execute();
        Console.WriteLine("Target Summary Report:");
        Console.WriteLine($"- Database Name: {vuforiaRetrieveTargetSummaryReportResponse.DatabaseName}");

        // Example: Get database summary report
        var vuforiaGetDatabaseSummaryReportResponse = targetService.TargetList.GetDatabaseSummaryReport(keys).Execute();
        Console.WriteLine("Database Summary Report:");
        Console.WriteLine($"- Database Name: {vuforiaGetDatabaseSummaryReportResponse.Name}");
    }
    catch (VuforiaPortalApiException ex)
    {
        Console.WriteLine($"Vuforia API Error: {ex.Message} (Code: {ex.Error.ResultCode})");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"General Error: {ex.Message}");
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
