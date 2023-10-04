using Cgpe.MessageBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{

    public static class DuMessageBusManager
    {

        private static object sync = new object();
        private static CgpeMessageBus messageBus;

        public static CgpeMessageBus MessageBus
        {
            get
            {
                lock (sync)
                {
                    if (messageBus == null)
                        messageBus = new CgpeMessageBus();
                    return messageBus;
                }
            }
        }

        public static void DisposeMessageBus()
        {
            lock(sync)
            {
                if (messageBus != null)
                    messageBus.Dispose();
            }
        }

    }

}
