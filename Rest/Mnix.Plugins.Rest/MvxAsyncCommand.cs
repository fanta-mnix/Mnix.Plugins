using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnix.Plugins.Rest
{
    public class MvxAsyncCommand : MvxCommand
    {
        public MvxAsyncCommand(Func<Task> command) : base(async () => await command()) { }
    }

    public class MvxAsyncCommand<T> : MvxCommand<T>
    {
        public MvxAsyncCommand(Func<T, Task> command) : base(async (arg) => await command(arg)) { }
    }
}
