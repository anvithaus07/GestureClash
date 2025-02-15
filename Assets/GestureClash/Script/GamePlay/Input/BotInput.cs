using GestureClash.UI;
using System;

namespace GestureClash.GamePlay
{
    public class BotInput : BasePlayerInput
    {
        public BotInput()
        {
            Array _gestures = Enum.GetValues(typeof(GestureType));
            var gesture = (GestureType)_gestures.GetValue(UnityEngine.Random.Range(0, _gestures.Length));
            SetGestureType(gesture);
        }
        public override GestureType? GetSelectedGesture()
        {
            return _gestureType;
        }
    }
}