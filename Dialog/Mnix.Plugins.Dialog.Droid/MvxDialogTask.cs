using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Droid.Views;

namespace Mnix.Plugins.Dialog.Droid
{
    public class MvxDialogTask : IMvxDialogTask
    {
        private readonly Action mEmpty = () => { };

        public void Show(string title, string message, string positiveText, string negativeText, Action yesCallback, Action noCallback)
        {
            new AlertDialog.Builder(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity)
                .SetTitle(title)
                .SetMessage(message)
                .SetPositiveButton(positiveText, (s, e) => { (yesCallback ?? mEmpty)(); })
                .SetNegativeButton(negativeText, (s, e) => { (noCallback ?? mEmpty)(); })
                .Create().Show();
        }

        public void Show(string message)
        {
            Toast.MakeText(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity, message, ToastLength.Long).Show();
        }
    }
}