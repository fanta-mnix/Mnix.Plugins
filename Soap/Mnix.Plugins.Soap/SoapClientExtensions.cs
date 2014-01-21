using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mnix.Plugins.Soap;

namespace Mnix.Samples.SoapPlugin
{
    public static class SoapClientExtensions
    {
        public static Task<string> HelloWorldAsync(this ISoapClient client, string name)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            return TaskEx.Run(() =>
                {
                    object[] results = client.Invoke("HelloWorld", new object[] {
                        name});
                    return ((string)(results[0]));
                });
        }
    }
}
