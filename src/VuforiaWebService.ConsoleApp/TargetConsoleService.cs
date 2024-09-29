using Newtonsoft.Json;
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

    public static void ListTargets(ServerAccessKeys serverAccessKeys)
    {
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.List(serverAccessKeys).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Targets: {string.Join(", ", result.Results)}");
    }

    public static void GetTarget(ServerAccessKeys serverAccessKeys, string targetId)
    {
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

    public static void InsertTarget(ServerAccessKeys serverAccessKeys, string jsonString)
    {
        var body = JsonConvert.DeserializeObject<PostTrackableRequest>(jsonString);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Insert(serverAccessKeys, body).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Inserted Target ID: {result.TargetId}");
    }

    public static void UpdateTarget(ServerAccessKeys serverAccessKeys, string jsonString, string targetId)
    {
        var body = JsonConvert.DeserializeObject<PostTrackableRequest>(jsonString);
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Update(serverAccessKeys, body, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
    }

    public static void DeleteTarget(ServerAccessKeys serverAccessKeys, string targetId)
    {
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.Delete(serverAccessKeys, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
    }

    public static void CheckSimilarTargets(ServerAccessKeys serverAccessKeys, string targetId)
    {
        var targetService = new TargetService(GetInitializer());
        var result = targetService.TargetList.CheckSimilar(serverAccessKeys, targetId).Execute();

        // Output results
        Console.WriteLine($"Transaction ID: {result.TransactionId}");
        Console.WriteLine($"Result Code: {result.ResultCode}");
        Console.WriteLine($"Similar Targets: {string.Join(", ", result.SimilarTargets)}");
    }

    public static void RetrieveTargetSummaryReport(ServerAccessKeys serverAccessKeys, string targetId)
    {
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

    public static void GetDatabaseSummaryReport(ServerAccessKeys serverAccessKeys)
    {
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
