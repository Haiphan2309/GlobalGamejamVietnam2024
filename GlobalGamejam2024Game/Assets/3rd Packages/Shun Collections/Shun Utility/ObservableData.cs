

using System;
using UnityEngine;

namespace _Scripts.DataWrapper
{
    
    [Serializable]
    public class ObservableData<T>
    {
        [SerializeField]  private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                T previousValue = this._value;
                this._value = value;
                OnChangeValue?.Invoke(previousValue, value);
            }
        }

        public event Action<T, T> OnChangeValue;

        public ObservableData(T value)
        {
            this._value = value;
        }
    }
}