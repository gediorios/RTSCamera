using System;
using MissionLibrary.Provider;

namespace MissionSharedLibrary.Provider
{
    public class ObjectVersion<T> : IObjectVersion<T> where T : ATag<T>
    {
        public static IObjectVersion<T> Create(Func<ATag<T>> creator, Version providerVersion)
        {
            return new ObjectVersion<T>(creator, providerVersion);
        }

        private readonly Func<ATag<T>> _creator;

        private T _value;

        public Version ProviderVersion { get; }

        public T Value
        {
            get
            {
                if (_value == null)
                    _value = Create();

                return _value;
            }
        }

        public ObjectVersion(Func<ATag<T>> creator, Version providerVersion)
        {
            ProviderVersion = providerVersion;
            _creator = creator;
        }
        public void ForceCreate()
        {
            if (_value == null)
                _value = Create();
        }

        private T Create()
        {
            return _creator?.Invoke().Self;
        }
    }
}
