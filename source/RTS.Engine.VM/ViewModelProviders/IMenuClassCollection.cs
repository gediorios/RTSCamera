using MissionLibrary.Provider;
using TaleWorlds.Library;

namespace MissionLibrary.View
{
    public interface IMenuClassCollection : IViewModelProvider<ViewModel>
    {
        void AddOptionClass(IObjectIdentitfication<AOptionClass> optionClass);

        void OnOptionClassSelected(AOptionClass optionClass);

        void Clear();
    }
}