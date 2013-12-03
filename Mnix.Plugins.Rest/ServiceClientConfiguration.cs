using Cirrious.CrossCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Rest
{
    public class ServiceClientConfiguration : IMvxPluginConfiguration
    {
        public string BaseUri { get; set; }
    }
}
