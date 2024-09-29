using System.CommandLine;
using VuforiaWebService.Api.Core;

namespace VuforiaWebService.ConsoleApp.Helpers;

internal class CommandHelper
{
    public static Command CreateActionCommand(
        string actionName,
        Action<ServerAccessKeys> action,
        Option<string> accessKeyOption,
        Option<string> secretKeyOption)
    {
        var command = new Command(actionName);

        command.SetHandler((accessKeyOptionValue, secretKeyOptionValue) =>
        {
            var serverAccessKeys = new ServerAccessKeys(accessKeyOptionValue, secretKeyOptionValue);
            action.Invoke(serverAccessKeys);
        }, accessKeyOption, secretKeyOption);

        return command;
    }

    public static Command CreateActionCommand<T>(
        string actionName,
        Action<ServerAccessKeys, T> action,
        Option<T> paramOption,
        Option<string> accessKeyOption,
        Option<string> secretKeyOption)
    {
        var command = new Command(actionName);

        command.AddOption(paramOption);
        command.SetHandler((accessKeyOptionValue, secretKeyOptionValue, param) =>
        {
            var serverAccessKeys = new ServerAccessKeys(accessKeyOptionValue, secretKeyOptionValue);
            action.Invoke(serverAccessKeys, param);
        }, accessKeyOption, secretKeyOption, paramOption);

        return command;
    }

    public static Command CreateActionCommand<T1, T2>(
        string actionName,
        Action<ServerAccessKeys, T1, T2> action,
        Option<T1> param1Option,
        Option<T2> param2Option,
        Option<string> accessKeyOption,
        Option<string> secretKeyOption)
    {
        var command = new Command(actionName);

        command.AddOption(param1Option);
        command.AddOption(param2Option);

        command.SetHandler((accessKeyOptionValue, secretKeyOptionValue, param1, param2) =>
        {
            var serverAccessKeys = new ServerAccessKeys(accessKeyOptionValue, secretKeyOptionValue);
            action.Invoke(serverAccessKeys, param1, param2);
        }, accessKeyOption, secretKeyOption, param1Option, param2Option);

        return command;
    }
}
