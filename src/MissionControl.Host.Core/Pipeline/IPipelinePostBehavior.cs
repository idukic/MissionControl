﻿using System.Threading.Tasks;
using MissionControl.Host.Core.Contracts;
using MissionControl.Host.Core.Responses;

namespace MissionControl.Host.Core.Pipeline
{
    public interface IPipelinePostBehavior<in T> where T : CliCommand
    {
        Task Process(T command, CliResponse response);
    }
}