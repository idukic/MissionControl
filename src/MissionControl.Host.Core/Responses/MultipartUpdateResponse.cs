﻿namespace MissionControl.Host.Core.Responses
{
    internal class MultipartUpdateResponse : CliResponse
    {
        public MultipartUpdateResponse( bool isDone, string content, string clientId)
        {
            IsDone = isDone;
            Content = content;
        }
        
        public override string Type => "multipart-update";

    }
}