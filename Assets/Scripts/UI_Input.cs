// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/UI_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UI_Input : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UI_Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UI_Input"",
    ""maps"": [
        {
            ""name"": ""Joystick"",
            ""id"": ""3d892f1c-ed88-41a6-be5b-0c2ab0a8f81b"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""f23fb031-d6e2-429c-a643-8037b049e083"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a7ee810c-830d-41c7-9720-73e3782a4ed3"",
                    ""path"": ""<AndroidJoystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Joystick
        m_Joystick = asset.FindActionMap("Joystick", throwIfNotFound: true);
        m_Joystick_Aim = m_Joystick.FindAction("Aim", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Joystick
    private readonly InputActionMap m_Joystick;
    private IJoystickActions m_JoystickActionsCallbackInterface;
    private readonly InputAction m_Joystick_Aim;
    public struct JoystickActions
    {
        private @UI_Input m_Wrapper;
        public JoystickActions(@UI_Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Joystick_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Joystick; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JoystickActions set) { return set.Get(); }
        public void SetCallbacks(IJoystickActions instance)
        {
            if (m_Wrapper.m_JoystickActionsCallbackInterface != null)
            {
                @Aim.started -= m_Wrapper.m_JoystickActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_JoystickActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_JoystickActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_JoystickActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public JoystickActions @Joystick => new JoystickActions(this);
    public interface IJoystickActions
    {
        void OnAim(InputAction.CallbackContext context);
    }
}
