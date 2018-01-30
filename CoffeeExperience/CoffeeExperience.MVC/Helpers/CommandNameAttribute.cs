using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CoffeeExperience.MVC.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CommandNameAttribute : ActionNameSelectorAttribute
    {
        public CommandNameAttribute(string action, string command)
        {
            this.action = action;
            this.command = command;
            this.commandRegex = CreateCommandRegex(command);
        }


        private readonly string action;

        public string Action
        {
            get { return this.action; }
        }

        private readonly string command;

        public string Command
        {
            get { return this.command; }
        }

        private Regex commandRegex;

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            if (!string.IsNullOrWhiteSpace(this.Action))
            {
                var actions = this.Action.Split(new[] { ',', '|', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

                if (actions.Length == 0)
                {
                    return false;
                }

                if (!actions.Contains((string)controllerContext.Controller.ControllerContext.RouteData.Values["action"], StringComparer.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            Match commandMatch = null;
            foreach (var key in controllerContext.HttpContext.Request.Form.AllKeys)
            {
                var match = this.commandRegex.Match(key);
                if (match.Success)
                {
                    commandMatch = match;
                    break;
                }
            }

            if (commandMatch == null)
            {
                return false;
            }

            var value = controllerContext.Controller.ValueProvider.GetValue(commandMatch.Value);

            if (value == null)
            {
                return false;
            }

            foreach (var groupName in this.commandRegex.GetGroupNames().Where(n => n.Any(c => !char.IsDigit(c))))
            {
                var group = commandMatch.Groups[groupName];
                if (group != null && group.Success)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[groupName] = group.Value;
                }
            }

            return true;
        }

        private static Regex CreateCommandRegex(string command)
        {
            var parts = CommandArgumentPattern.Matches(command);

            var builder = new StringBuilder("^");
            int index = 0;
            foreach (Match part in parts)
            {
                if (part.Index > index)
                {
                    builder.Append(Regex.Escape(command.Substring(index, part.Index)));
                }

                builder.AppendFormat($"(?<{part.Groups["name"].Value}>[^;]+)");

                index = part.Index + part.Length + 1;
            }

            if (index < command.Length)
            {
                builder.Append(Regex.Escape(command.Substring(index)));
            }

            builder.Append("$");

            return new Regex(builder.ToString());
        }

        private static readonly Regex CommandArgumentPattern = new Regex(@"\{(?<name>\w+)\}", RegexOptions.Compiled);
    }
}