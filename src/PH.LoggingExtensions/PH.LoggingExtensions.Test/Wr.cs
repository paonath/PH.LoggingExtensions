using System;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions.Test
{
    public class Wr
    {
        private readonly ILogger logger;
        private Action<ILogger> Flus = logger =>
            logger.Log(LogLevel.Debug, new EventId(1, "Design Debug"), "message to write at {now}", DateTime.Now);

        public Wr(ILogger logger)
        {
            this.logger = logger;
        }

        public void Flush() => Flus.Invoke(logger);


    }
}