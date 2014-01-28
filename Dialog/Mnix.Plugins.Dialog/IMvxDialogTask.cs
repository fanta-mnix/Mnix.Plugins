using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mnix.Plugins.Dialog
{
    public interface IMvxDialogTask
    {
        void Show(string title, string message, string positiveText, string negativeText, Action yesCallback, Action noCallback);
        void Show(string message);
    }
}
