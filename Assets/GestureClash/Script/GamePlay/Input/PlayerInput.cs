using GestureClash.UI;
namespace GestureClash.GamePlay
{
    public class PlayerInput : BasePlayerInput
    {
        public override GestureType? GetSelectedGesture()
        {
            return _gestureType;
        }
    }
}