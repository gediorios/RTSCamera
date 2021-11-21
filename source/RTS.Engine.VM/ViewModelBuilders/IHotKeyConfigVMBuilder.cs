using MissionLibrary.HotKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS.Engine.Extensions
{
    internal interface IHotKeyConfigVMBuilder
    {
        AHotKeyConfigVM CreateViewModel(Action<IHotKeySetter> onKeyBindRequest);
    }
}
