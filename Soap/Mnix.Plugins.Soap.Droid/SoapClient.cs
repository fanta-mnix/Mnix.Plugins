using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Services.Protocols;
using Cirrious.CrossCore;

namespace Mnix.Plugins.Soap.Common
{
    public class SoapClient : ISoapClient
    {
        private SoapHttpClientProtocol mClient;

        // protected object[] Invoke(string method_name, object[] parameters);
        private MethodInfo mInvokeInfo;

        public SoapClient(SoapClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }
            mClient = configuration.Client;
            // Cache reflected method
            mInvokeInfo = mClient.GetType().GetMethod("Invoke", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public object[] Invoke(string methodName, object[] parameters)
        {
            try
            {
                // Use reflection to call protected "Invoke" method of SoapHttpClientProtocol
                return (object[])mInvokeInfo.Invoke(mClient, new object[] { methodName, parameters });
            }
            catch (TargetInvocationException exc)
            {
                throw exc.InnerException;
                // If reflection exception, throw inner exception
            }
        }
    }
}