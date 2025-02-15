using GestureClash;
using UnityEngine;

public static class InputHandler 
{
    public static BasePlayerInput GetInputType(bool isBot)
    {
        return isBot ? new BotInput() : new PlayerInput();
    }
}
