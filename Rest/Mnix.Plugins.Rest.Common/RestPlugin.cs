using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore.Plugins;
using Cirrious.CrossCore;

namespace Mnix.Plugins.Rest.Common
{
    public class RestPlugin : IMvxConfigurablePlugin
    {
        private ServiceClientConfiguration mConfiguration;

        public void Load()
        {
			Mvx.RegisterSingleton<ServiceStack.Service.IServiceClient>(new ServiceStack.ServiceClient.Web.JsonServiceClient(mConfiguration.BaseUri));
        }

        public void Configure(IMvxPluginConfiguration configuration)
        {
            mConfiguration = (ServiceClientConfiguration)configuration;
        }
    }
}