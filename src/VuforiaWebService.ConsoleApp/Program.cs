using System.CommandLine;
using VuforiaWebService.ConsoleApp;

// Create reusable global options
var accessKeyOption = new Option<string>("--access-key", "Your access key") { IsRequired = true };
var secretKeyOption = new Option<string>("--secret-key", "Your secret key") { IsRequired = true };

// Define the root command
var rootCommand = new RootCommand("Vuforia Web Service CLI Tool");

rootCommand.AddGlobalOption(accessKeyOption);
rootCommand.AddGlobalOption(secretKeyOption);

// Define target options with unique names and correct descriptions
var targetIdOption = new Option<string>("--target-id", "ID of the target");
var targetNameOption = new Option<string>("--target-name", "The name of the target");
var targetNullableWidthOption = new Option<float?>("--target-width", "The width of the target in units");
var targetWidthOption = new Option<float>("--target-width", "The width of the target in units");
var targetImageOption = new Option<string>("--target-image", "Path to the image file of the target");
var targetActiveFlagOption = new Option<bool?>("--target-active-flag", "Indicates if the target is active (true or false)");
var targetMetadataOption = new Option<string>("--target-metadata", "Additional metadata for the target");

// Command: list
var listCommand = new Command("list");
listCommand.SetHandler(TargetConsoleService.ListTargets, accessKeyOption, secretKeyOption);
rootCommand.Add(listCommand);

// Command: get
var getCommand = new Command("get");
getCommand.AddOption(targetIdOption);
getCommand.SetHandler(TargetConsoleService.GetTarget, accessKeyOption, secretKeyOption, targetIdOption);
rootCommand.Add(getCommand);

// Command: insert
var insertCommand = new Command("insert");
insertCommand.AddOption(targetNameOption);
insertCommand.AddOption(targetWidthOption);
insertCommand.AddOption(targetImageOption);
insertCommand.AddOption(targetActiveFlagOption);
insertCommand.AddOption(targetMetadataOption);
insertCommand.SetHandler(TargetConsoleService.InsertTarget, accessKeyOption, secretKeyOption, targetNameOption, targetWidthOption, targetImageOption, targetActiveFlagOption, targetMetadataOption);
rootCommand.Add(insertCommand);

// Command: update
var updateCommand = new Command("update");
updateCommand.AddOption(targetIdOption);
updateCommand.AddOption(targetNameOption);
updateCommand.AddOption(targetNullableWidthOption);
updateCommand.AddOption(targetImageOption);
updateCommand.AddOption(targetActiveFlagOption);
updateCommand.AddOption(targetMetadataOption);
updateCommand.SetHandler(TargetConsoleService.UpdateTarget, accessKeyOption, secretKeyOption, targetIdOption, targetNameOption, targetNullableWidthOption, targetImageOption, targetActiveFlagOption, targetMetadataOption);
rootCommand.Add(updateCommand);

// Command: delete
var deleteCommand = new Command("delete");
deleteCommand.AddOption(targetIdOption);
deleteCommand.SetHandler(TargetConsoleService.DeleteTarget, accessKeyOption, secretKeyOption, targetIdOption);
rootCommand.Add(deleteCommand);

// Command: check-similar
var checkSimilarCommand = new Command("check-similar");
checkSimilarCommand.AddOption(targetIdOption);
checkSimilarCommand.SetHandler(TargetConsoleService.CheckSimilarTargets, accessKeyOption, secretKeyOption, targetIdOption);
rootCommand.Add(checkSimilarCommand);

// Command: summary-report
var summaryReportCommand = new Command("summary-report");
summaryReportCommand.AddOption(targetIdOption);
summaryReportCommand.SetHandler(TargetConsoleService.RetrieveTargetSummaryReport, accessKeyOption, secretKeyOption, targetIdOption);
rootCommand.Add(summaryReportCommand);

// Command: database-summary
var databaseSummary = new Command("database-summary");
databaseSummary.SetHandler(TargetConsoleService.GetDatabaseSummaryReport, accessKeyOption, secretKeyOption);
rootCommand.Add(databaseSummary);

// Execute the command line app
return await rootCommand.InvokeAsync(args);