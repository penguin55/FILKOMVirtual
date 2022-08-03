using System.Collections;
using System.Collections.Generic;
using TomGustin.GameDesignPattern;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputType inputType;
    [SerializeField] private VariableJoystick playerJoystick;
    [SerializeField] private VariableJoystick cameraJoystick;

    private static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static float GetAxis(string command)
    {
        if (instance.inputType.Equals(InputType.Standart))
        {
            return Input.GetAxisRaw(command);
        } else
        {
            if (command.ToLower().Contains("player"))
            {
                if (command.ToLower().Contains("horizontal")) return instance.playerJoystick.Horizontal;
                if (command.ToLower().Contains("vertical")) return instance.playerJoystick.Vertical;
            }

            if (command.ToLower().Contains("camera"))
            {
                if (command.ToLower().Contains("horizontal")) return instance.cameraJoystick.Horizontal;
                if (command.ToLower().Contains("vertical")) return instance.cameraJoystick.Vertical;
            }
        }

        return 0f;
    }

    public static bool GetButtonDown(string command)
    {
        if (instance.inputType.Equals(InputType.Standart)) return Input.GetButtonDown(command);

        return false;
    }

    public enum InputType { Standart, Virtual }
}
