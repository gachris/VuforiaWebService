using VuforiaWebService.Api.Auth;
using VuforiaWebService.Api.Core;
using VuforiaWebService.Api.Target.Services;
using VuforiaWebService.Api.Target.Types;

namespace VuforiaWebService.ConsoleApp;

internal class TargetConsoleService
{
    private static BaseClientService.Initializer GetInitializer()
    {
        var userCredential = new UserCredential();
        var initializer = new BaseClientService.Initializer()
        {
            ApplicationName = "VuforiaWebService.ConsoleApp",
            HttpClientInitializer = userCredential,
        };

        return initializer;
    }

    public static void ListTargets(string accessKey, string secretKey)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.List(serverAccessKeys).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Targets: {string.Join(", ", result.Results)}");
    }

    public static void GetTarget(string accessKey, string secretKey, string targetId)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Get(serverAccessKeys, targetId).Execute();
        var targetRecord = result.TargetRecord;

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Status: {result.Status}");
        Console.WriteLine($"Target ID: {result.TargetRecord.TargetId}");
        Console.WriteLine($"Active Flag: {targetRecord.ActiveFlag}");
        Console.WriteLine($"Tracking Rating: {targetRecord.TrackingRating}");
        Console.WriteLine($"Width: {targetRecord.Width}");
        Console.WriteLine($"Name: {targetRecord.Name}");
    }

    public static void InsertTarget(string accessKey, string secretKey, string name, float width, string image, bool? activeFlag, string metadata)
    {
        var imageBytes = File.ReadAllBytes(image);
        var imageBase64 = Convert.ToBase64String(imageBytes);

        var body = new PostTrackableRequest()
        {
            ActiveFlag = activeFlag,
            ApplicationMetadata = metadata,
            Image = imageBase64,
            Name = name,
            Width = width
        };
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Insert(serverAccessKeys, body).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Inserted Target ID: {result.TargetId}");
    }

    public static void UpdateTarget(string accessKey, string secretKey, string targetId, string name, float? width, string image, bool? activeFlag, string metadata)
    {
        var imageBytes = image != null ? File.ReadAllBytes(image) : null;
        var imageBase64 = imageBytes != null ? Convert.ToBase64String(imageBytes) : null;

        var body = new UpdateTrackableRequest()
        {
            ActiveFlag = activeFlag,
            ApplicationMetadata = metadata,
            Image = imageBase64,
            Name = name,
            Width = width
        };
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Update(serverAccessKeys, body, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
    }

    public static void DeleteTarget(string accessKey, string secretKey, string targetId)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Delete(serverAccessKeys, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
    }

    public static void CheckSimilarTargets(string accessKey, string secretKey, string targetId)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.CheckSimilar(serverAccessKeys, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Similar Targets: {string.Join(", ", result.SimilarTargets)}");
    }

    public static void RetrieveTargetSummaryReport(string accessKey, string secretKey, string targetId)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.RetrieveTargetSummaryReport(serverAccessKeys, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Status: {result.Status}");
        Console.WriteLine($"Total Reco: {result.TotalRecos}");
        Console.WriteLine($"Active: {result.ActiveFlag}");
        Console.WriteLine($"Database Name: {result.DatabaseName}");
        Console.WriteLine($"Current Month Recos: {result.CurrentMonthRecos}");
        Console.WriteLine($"Previous Month Recos: {result.PreviousMonthRecos}");
        Console.WriteLine($"Target Name: {result.TargetName}");
        Console.WriteLine($"Tracking Rating: {result.TrackingRating}");
        Console.WriteLine($"Reco Rating: {result.RecoRating}");
        Console.WriteLine($"Tracking Rating: {result.UploadDate}");
    }

    public static void GetDatabaseSummaryReport(string accessKey, string secretKey)
    {
        var serverAccessKeys = new ServerAccessKeys(accessKey, secretKey);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.GetDatabaseSummaryReport(serverAccessKeys).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Name: {result.Name}");
        Console.WriteLine($"Active Images: {result.ActiveImages}");
        Console.WriteLine($"Failed Images: {result.FailedImages}");
        Console.WriteLine($"Inactive Images: {result.InactiveImages}");
    }
}
