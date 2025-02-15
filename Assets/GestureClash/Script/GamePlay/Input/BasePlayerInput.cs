using GestureClash.UI;
namespace GestureClash.GamePlay
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