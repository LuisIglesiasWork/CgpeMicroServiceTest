using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Cgpe.Du.Ministry.WcfApi.Tracking
{

    public class DirectoryTrackingMessageInspector : IDispatchMessageInspector
    {

        private static object sync = new object();

        private const string isTrakingEnabledName = "IsTrackingEnabled";
        private const string trackingFilePathName = "TrakingFilePath";
        private const string trackingFileName = "DirectoryTracking.log";
        private const string spliter = "--------------------------------------------------";
        private const string trackingHeader = "Mensaje ha sido {0}\t-\t{1}";

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            lock(sync)
            {
                if (bool.Parse(ConfigurationManager.AppSettings[isTrakingEnabledName]))
                    this.TrackMessage(request, true); 
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (bool.Parse(ConfigurationManager.AppSettings[isTrakingEnabledName]))
                this.TrackMessage(reply, false);
        }

        private void TrackMessage(Message message, bool isReceived)
        {
            string messageString = null;
            if (message.IsFault)
                messageString = "Error message was sent.";
            else
                messageString = message.ToString();
            string filePath = ConfigurationManager.AppSettings[trackingFilePathName];
            if (string.IsNullOrEmpty(filePath))
                throw new ConfigurationErrorsException("Parameter \"" + trackingFilePathName + "\" of web.config is not valid.");
            using(Stream stream = File.Open(filePath + trackingFileName, FileMode.Append))
            {
                TextWriter writer = new StreamWriter(stream);
                writer.WriteLine(spliter);
                writer.WriteLine(trackingHeader, isReceived ? "recibido" : "enviado", DateTime.Now);
                writer.WriteLine(spliter);
                writer.WriteLine(messageString);
                writer.WriteLine();
                writer.Flush();
            }
        }

    }

}