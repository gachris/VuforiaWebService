using System.CommandLine;
using VuforiaWebService.ConsoleApp;
using VuforiaWebService.ConsoleApp.Helpers;

// Create reusable global options
var accessKeyOption = new Option<string>("--access-key", "Your access key") { IsRequired = true };
var secretKeyOption = new Option<string>("--secret-key", "Your secret key") { IsRequired = true };

// Define the root command
var rootCommand = new RootCommand("Vuforia Web Service CLI Tool");

rootCommand.AddGlobalOption(accessKeyOption);
rootCommand.AddGlobalOption(secretKeyOption);

var targetIdOption = new Option<string>("--target-id", "ID of the target") { IsRequired = true };
var targetJsonOption = new Option<string>("--target", "The target in JSON format") { IsRequired = true };

// Command: list
rootCommand.Add(CommandHelper.CreateActionCommand(
    "list",
    TargetConsoleService.ListTargets,
    accessKeyOption, secretKeyOption));

// Command: get
rootCommand.Add(CommandHelper.CreateActionCommand(
    "get",
    TargetConsoleService.GetTarget,
    targetIdOption, accessKeyOption, secretKeyOption));

// Command: insert
rootCommand.Add(CommandHelper.CreateActionCommand(
    "insert",
    TargetConsoleService.InsertTarget,
    targetJsonOption, accessKeyOption, secretKeyOption));

// Command: update
rootCommand.Add(CommandHelper.CreateActionCommand(
    "update",
    TargetConsoleService.UpdateTarget,
    targetJsonOption, targetIdOption, accessKeyOption, secretKeyOption));

// Command: delete
rootCommand.Add(CommandHelper.CreateActionCommand(
    "delete",
    TargetConsoleService.DeleteTarget,
    targetIdOption, accessKeyOption, secretKeyOption));

// Command: check-similar
rootCommand.Add(CommandHelper.CreateActionCommand(
    "check-similar",
    TargetConsoleService.CheckSimilarTargets,
    targetIdOption, accessKeyOption, secretKeyOption));

// Command: summary-report
rootCommand.Add(CommandHelper.CreateActionCommand(
    "summary-report",
    TargetConsoleService.RetrieveTargetSummaryReport,
    targetIdOption, accessKeyOption, secretKeyOption));

// Command: database-summary
rootCommand.Add(CommandHelper.CreateActionCommand(
    "database-summary",
    TargetConsoleService.GetDatabaseSummaryReport,
    accessKeyOption, secretKeyOption));

// Execute the command line app
return await rootCommand.InvokeAsync(args);