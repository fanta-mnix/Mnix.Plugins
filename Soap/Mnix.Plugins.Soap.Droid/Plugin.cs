using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Mnix.Plugins.Soap.Droid.Svc;

namespace Mnix.Plugins.Soap.Droid
{
    public class Plugin : IMvxConfigurablePlugin
    {
        private SoapClientConfiguration mConfiguration;

		public void Load()
		{
            Mvx.RegisterSingleton<ISoapClient>(new SoapClient(mConfiguration));
		}

        public void Configure(IMvxPluginConfiguration configuration)
        {
            if (configuration == null)
            {
                // Configuration not found
                throw new ArgumentNullException("configuration", "Override method GetPluginConfiguration in Setup");
            }
            mConfiguration = (SoapClientConfiguration)configuration;
        }
	}
}