//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Inputs/DebuggerInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @DebuggerInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DebuggerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DebuggerInputs"",
    ""maps"": [
        {
            ""name"": ""Debug"",
            ""id"": ""a40d8607-1456-44fe-a318-a8d72146f4ae"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""0b95fc81-6321-4b8d-8280-b196c86f4b01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""11b112ce-f108-4422-a87e-dbb39025933b"",
                    ""path"": ""<Keyboard>/#(!)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c57e5110-d90f-4d3f-aab4-7be9ea97bf42"",
                    ""path"": ""<Touchscreen>/touch*/Press"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_Toggle = m_Debug.FindAction("Toggle", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_Toggle;
    public struct DebugActions
    {
        private @DebuggerInputs m_Wrapper;
        public DebugActions(@DebuggerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_Debug_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @Toggle.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggle;
                @Toggle.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggle;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Toggle.started += instance.OnToggle;
                @Toggle.performed += instance.OnToggle;
                @Toggle.canceled += instance.OnToggle;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);
    public interface IDebugActions
    {
        void OnToggle(InputAction.CallbackContext context);
    }
}
