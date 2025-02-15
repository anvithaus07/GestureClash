using System;
using UnityEngine;

namespace GestureClash
{
    public class BotInput : BasePlayerInput
    {
        public BotInput()
        {
            Array _gestures = Enum.GetValues(typeof(GestureType));
            var gesture = (GestureType)_gestures.GetValue(UnityEngine.Random.Range(0, _gestures.Length));
            SetGestureType(gesture);
        }
        public override GestureType GetSelectedGesture()
        {
            return _gestureType;
        }
    }
}