using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Soap
{
    public interface ISoapClient
    {
        object[] Invoke(string methodName, object[] parameters);
    }
}
