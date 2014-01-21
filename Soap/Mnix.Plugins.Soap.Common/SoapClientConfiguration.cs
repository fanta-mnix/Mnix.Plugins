using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using Cirrious.CrossCore.Plugins;

namespace Mnix.Plugins.Soap.Droid
{
    public class SoapClientConfiguration : IMvxPluginConfiguration
    {
        public SoapHttpClientProtocol Client { get; private set; }

        public SoapClientConfiguration(SoapHttpClientProtocol client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            Client = client;
        }
    }
}