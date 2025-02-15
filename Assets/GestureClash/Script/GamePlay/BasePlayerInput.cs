using UnityEngine;
namespace GestureClash
{
    public abstract class BasePlayerInput
    {
        protected GestureType? _gestureType { private set;  get; }
        public abstract GestureType? GetSelectedGesture();

        public virtual void SetGestureType(GestureType? gestureType)
        {
            _gestureType = gestureType;
        }
    }
}