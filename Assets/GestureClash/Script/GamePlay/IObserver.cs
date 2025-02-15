using UnityEngine;

namespace GestureClash
{
    public interface IObserver<T>
    {
        void OnNotified(T data);
    }

    public interface IObservable<T>
    {
        void AddObserver(IObserver<T> observer);
        void RemoveObserver(IObserver<T> observer);

        void NotifyObserver(T data);
    }
}