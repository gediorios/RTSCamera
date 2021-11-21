using System;

namespace MissionLibrary.Provider
{
    public interface IVersion
    {
        Version ProviderVersion { get; }
        void ForceCreate();
    }

    public interface IObjectVersion<out T>: IVersion where T : ATag<T>
    {
        T Value { get; }
    }
}
