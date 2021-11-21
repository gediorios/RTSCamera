﻿using MissionLibrary.Provider;
using TaleWorlds.Library;

namespace MissionLibrary.View
{
    public abstract class AOptionSelectionViewModelProvider : ATag<AOptionSelectionViewModelProvider>, IViewModelProvider<ViewModel>
    {
        public virtual string Id => "";
        public abstract ViewModel GetViewModel();

        public abstract void UpdateSelection(bool isSelected);
    }
}
