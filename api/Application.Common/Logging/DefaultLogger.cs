namespace App.Common.Logging
{
    using log4net;

    public class DefaultLogger : ILogger
    {
        private readonly ILog logger;

        public DefaultLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.logger = LogManager.GetLogger(typeof(DefaultLogger));
        }

        public void Info(string message, params object[] args)
        {
            string messageToWrite = this.GetMessage(message, args);
            this.logger.Info(messageToWrite);
        }

        public void Warn(string message, params object[] args)
        {
            string messageToWrite = this.GetMessage(message, args);
            this.logger.Warn(messageToWrite);
        }

        public void Error(string message, params object[] args)
        {
            string messageToWrite = this.GetMessage(message, args);
            this.logger.Error(messageToWrite);
        }

        public void Info(object arg)
        {
            this.logger.Info(arg);
        }

        public void Warn(object arg)
        {
            this.logger.Warn(arg);
        }

        public void Error(object arg)
        {
            this.logger.Error(arg);
        }

        private string GetMessage(string message, object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return message;
            }

            return string.Format(message, args);
        }
    }
}