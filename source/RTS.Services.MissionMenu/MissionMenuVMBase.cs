using System;
using MissionLibrary.Event;
using TaleWorlds.Library;

namespace MissionSharedLibrary.View
{
    public abstract class MissionMenuVMBase : ViewModel
    {
        private readonly Action _closeMenu;

        public virtual void CloseMenu()
        {
            //AMenuManager.Get().OnMenuClosed();
            MissionEvents.OnMissionMenuClosed();
            _closeMenu?.Invoke();
        }

        protected MissionMenuVMBase(Action closeMenu)
        {
            _closeMenu = closeMenu;
        }
    }
}
