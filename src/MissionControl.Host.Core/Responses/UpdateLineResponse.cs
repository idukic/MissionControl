﻿namespace MissionControl.Host.Core.Responses
{
    public sealed class UpdateLineResponse : CliResponse
    {
        public int CharPosition { get; set; }

        public string Content { get; set; }
    }
}