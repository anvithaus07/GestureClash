using System;

namespace GestureClash
{
    public class ASignal<T>
    {
        public static event Action<T> OnSignal;

        public static void AddListener(Action<T> listener)
        {
            OnSignal += listener;
        }

        public static void RemoveListener(Action<T> listener)
        {
            OnSignal -= listener;
        }

        public static void Dispatch(T signal)
        {
            OnSignal?.Invoke(signal);
        }
    }
}