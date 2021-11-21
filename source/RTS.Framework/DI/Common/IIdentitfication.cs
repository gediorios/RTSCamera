namespace MissionLibrary.Provider
{
    public interface IIdentitfication
    {
        string Id { get; }
        void ForceCreate();
        void Clear();
    }

    public interface IObjectIdentitfication<out T> : IIdentitfication where T : ATag<T>
    {
        T Value { get; }
    }
}
