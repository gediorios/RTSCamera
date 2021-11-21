using System;
using MissionLibrary.Provider;

namespace MissionSharedLibrary.Provider
{
    public class ObjectIdentitfication<T> : IObjectIdentitfication<T> where T : ATag<T>
    {
        public static IObjectIdentitfication<T> Create<T>(Func<ATag<T>> creator, string id) where T : ATag<T>
        {
            return new ObjectIdentitfication<T>(creator, id);
        }

        private readonly Func<ATag<T>> _creator;

        private T _value;

        public string Id { get; }

        public T Value 
        { 
            get 
            {
                if(_value == null)
                    _value = Create();

                return _value;
            }
        } 

        public ObjectIdentitfication(Func<ATag<T>> creator, string id)
        {
            Id = id;
            _creator = creator;
        }

        public void ForceCreate()
        {
            if (_value == null)
                _value = Create();
        }

        public void Clear()
        {
            _value = null;
        }

        private T Create()
        {
            return _creator?.Invoke().Self;
        }
    }
}
