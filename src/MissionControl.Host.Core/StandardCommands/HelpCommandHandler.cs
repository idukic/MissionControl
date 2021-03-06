﻿using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MissionControl.Host.Core.Contracts;
using MissionControl.Host.Core.Pipeline;
using MissionControl.Host.Core.Responses;
using MissionControl.Host.Core.Utilities;

namespace MissionControl.Host.Core.StandardCommands
{
    internal class HelpInterceptorPipeline<T> : IPipelineBehavior<T> where T: CliCommand
    {
        private readonly ICommandTypesCatalog _commandTypes;

        public HelpInterceptorPipeline(ICommandTypesCatalog commandTypes)
        {
            _commandTypes = commandTypes;
        }

        public async Task<CliResponse> Process(T command, CliHandlerDelegate next)
        {
            return command.Help ? GenerateHelp(command) : await next();
        }

        private TextResponse GenerateHelp(T command)
        {
            var type = command.GetType();

            var registration = _commandTypes.RegisteredCommands.FirstOrDefault(x => x.Type == type);

            var builder = new StringBuilder();
            builder.Append($"Description of command {registration?.Name}:\n");

            if (registration != null)
            {
                if (registration.Attribute.Help.HasContent())
                    builder.Append(registration.Attribute.Help).Append("\n");

                builder.Append("Available arguments:\n");

                var properties = registration.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in properties)
                {
                    var argAttribute = propertyInfo.GetCustomAttribute<CliArgAttribute>();

                    if (argAttribute != null)
                    {
                        if (argAttribute.Skip)
                            continue;

                        builder.Append("-").Append(propertyInfo.Name.ToLower()).Append(" : ");

                        builder.Append(argAttribute.Help);

                        if (argAttribute.Required)
                            builder.Append(" (required)");
                    }
                    else
                    {
                        builder.Append("-").Append(propertyInfo.Name.ToLower());
                    }

                    builder.Append("\n");
                }
            }

            return new TextResponse(builder.ToString());
        }
    }
}