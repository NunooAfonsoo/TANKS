//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Tank"",
            ""id"": ""07b48743-435b-4dad-a06e-e07e69a0da3c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""857d50c2-bd7d-4324-9f09-53bcbf5864fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""92cf0da9-ad46-4267-b278-d1b3620a46b1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""632e491b-1b6d-487b-9c2e-d4079436710b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9d4ed155-2da8-4a45-86be-63d73a3842b0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""17015a39-0ddc-40ad-9cf0-492251e7a38d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3414be03-a868-4204-97ff-05c4bc12e0df"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""20c25ab8-ab04-47c6-bbb4-3479afab275a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4242c9c5-4708-4a95-99c3-0a262c91e4bc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9c8c1c35-93ad-44b4-853b-7d068d91e966"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""04b3394b-78b9-44cd-9626-e8c478b8a995"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Tank
        m_Tank = asset.FindActionMap("Tank", throwIfNotFound: true);
        m_Tank_Move = m_Tank.FindAction("Move", throwIfNotFound: true);
        m_Tank_Rotate = m_Tank.FindAction("Rotate", throwIfNotFound: true);
        m_Tank_Shoot = m_Tank.FindAction("Shoot", throwIfNotFound: true);
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

    // Tank
    private readonly InputActionMap m_Tank;
    private ITankActions m_TankActionsCallbackInterface;
    private readonly InputAction m_Tank_Move;
    private readonly InputAction m_Tank_Rotate;
    private readonly InputAction m_Tank_Shoot;
    public struct TankActions
    {
        private @Controls m_Wrapper;
        public TankActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Tank_Move;
        public InputAction @Rotate => m_Wrapper.m_Tank_Rotate;
        public InputAction @Shoot => m_Wrapper.m_Tank_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Tank; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TankActions set) { return set.Get(); }
        public void SetCallbacks(ITankActions instance)
        {
            if (m_Wrapper.m_TankActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnRotate;
                @Shoot.started -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_TankActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_TankActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public TankActions @Tank => new TankActions(this);
    public interface ITankActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}