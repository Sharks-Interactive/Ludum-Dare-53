using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Threading.Tasks;

public static class Input
{
    public static int ConnectedPlayers {  get =>
        Gamepad.all.Count > 0 ? Gamepad.all.Count : 1;
    }

    public static float ReadAxis(int PlayerID, int Axis)
    {
        return Axis == 0 ?
            Gamepad.all.Count != 0 ? Gamepad.all[PlayerID].leftStick.ReadValue().x
            : Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue()
        :
            Gamepad.all.Count != 0 ? Gamepad.all[PlayerID].leftStick.ReadValue().y
            : Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue();
    }

    public static UnityEngine.InputSystem.Controls.ButtonControl GetActionInput(int PlayerId) =>
        Gamepad.all.Count != 0 ? Gamepad.all[PlayerId].aButton : Keyboard.current.eKey;
    
    public static async void Feedback(int PlayerId, Vector2 Strength, int Time)
    {
        if (Gamepad.all.Count == 0) return;

        Gamepad.all[PlayerId].SetMotorSpeeds(Strength.x, Strength.y);
        await Task.Delay(Time);
        Gamepad.all[PlayerId].SetMotorSpeeds(0, 0);
    }
}
