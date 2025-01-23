using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyBindings
{
    public static KeyCode[] TurnRight = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };
    public static KeyCode[] TurnLeft = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
    public static KeyCode[] TurnUp = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
    public static KeyCode[] TurnDown = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };
    public static KeyCode[] Attack = new KeyCode[] { KeyCode.Z };
    public static KeyCode[] Parry = new KeyCode[] { KeyCode.K };
    public static KeyCode[] Dash = new KeyCode[] { KeyCode.L };


    // dev mode
    public static KeyCode[] Debug = new KeyCode[] { KeyCode.Tab };
}
