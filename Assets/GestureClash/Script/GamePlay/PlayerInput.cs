using UnityEngine;
namespace GestureClash
{
    public class PlayerInput : BasePlayerInput
    {
        public override GestureType? GetSelectedGesture()
        {
            return _gestureType;
        }
    }
}