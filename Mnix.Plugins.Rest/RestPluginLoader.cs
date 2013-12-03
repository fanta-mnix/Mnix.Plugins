using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Rest
{
    public class RestPluginLoader : IMvxPluginLoader
    {
        public static readonly RestPluginLoader Instance = new RestPluginLoader();

        public void EnsureLoaded()
        {
            IMvxPluginManager manager = Mvx.Resolve<IMvxPluginManager>();
            manager.EnsurePlatformAdaptionLoaded<RestPluginLoader>();
        }
    }
}
