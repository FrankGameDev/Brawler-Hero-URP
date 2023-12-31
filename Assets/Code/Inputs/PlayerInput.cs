//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Code/Inputs/PlayerInput.inputactions
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

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""496d7336-cd3d-4dda-a46b-41a675a81def"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""413210dc-319b-48a0-b9bf-1cff1fdc96cf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Charged Attack"",
                    ""type"": ""Button"",
                    ""id"": ""a4dff2a3-fd00-40ec-99a6-8c4576e1b0a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""a38e253e-c6ed-4d5f-8743-5887fcc77368"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Light attack"",
                    ""type"": ""Button"",
                    ""id"": ""b1a89e33-c8e7-4af0-96c4-0116c145ac38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""36891593-b3cd-4c7e-b48d-1e18121d8487"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On"",
                    ""type"": ""Button"",
                    ""id"": ""dacb9ce1-49e4-43a4-8150-91e0470ecfb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LookAround"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e5b688d4-b23e-4dc9-a236-0a3b487638ec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""16abf3a2-5119-4ed7-bd59-e1c9fdb58a1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5872a303-6ab0-4b90-ac5e-f40a41e954a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StartFlying"",
                    ""type"": ""Button"",
                    ""id"": ""b849d9a4-2a6b-4cff-bca8-c10c2f3bff8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StopFlying"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1471f6c6-bed3-44de-a609-a4a8eafc862f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b1c3a46b-2c14-4075-b80b-904ef6095bd1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""d8f0349e-65bc-45f0-8ee3-2adbf0c92918"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""1f7cf67e-5277-4636-830f-b0090843347d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""eba8a24e-663f-4aa8-aeaf-bc46fcf9a6b7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""a3a465b2-e9c4-4407-a7e0-b9f0011e3ee4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""34ab018b-9b82-4d98-8cc7-daa8b962eea1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ad1a21c-e5de-4cba-836a-057bbb0aa21d"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a6f0881-5765-4ff6-b011-ef04ed8ae91d"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f95306d9-5a17-46ff-9c5e-3b38b2631eb3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94edf944-3d6a-4bed-a809-65f6869c0c7d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""daf07da0-b836-44ae-9962-d76d8fa7c0a5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cd2afa2-3e02-4ce6-b26f-4818de7a5db1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9319ed8-084a-4bd6-b150-4deb1a726181"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b79a5424-e051-4e3a-86c3-91ad909cf607"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50817a31-322f-4d9f-a011-2106c51f6b7c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dba3b32a-613f-4741-8155-c55f88b468f2"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be531b95-8d86-4272-80fb-1bb28d2225e0"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57218036-41ff-4e64-946c-850a3c3bb693"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70ce1505-335c-465c-bc5c-52f063d2f6f6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9a029b3-3101-4bb9-848d-f8d97bc6bd1e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed819c54-7ded-43af-b0f4-1e111001af6e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79b764ea-f6d3-4ede-a269-d0a952aa5c78"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""253a3133-5655-495d-a7b3-9276aa4bfdea"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceb2e253-d3c6-425e-8ab8-f9d2efe0d066"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Charged Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e17fb6d3-72c8-4dca-bb00-dfb50ca3e32d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Charged Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Flying"",
            ""id"": ""1c4c5089-e120-40de-a767-85760ddf47ae"",
            ""actions"": [
                {
                    ""name"": ""FlyUp"",
                    ""type"": ""Button"",
                    ""id"": ""89f6fd37-875b-4b99-a6af-f108107bdec2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FlyDown"",
                    ""type"": ""Button"",
                    ""id"": ""d9139fc1-af1f-4101-97b4-b603cc5aa433"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""30dc602c-4ff0-4f7d-be2c-7dc2b8b707a9"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b40443b6-e0fa-4142-8a6f-d14a10f926a9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlyDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_ChargedAttack = m_Gameplay.FindAction("Charged Attack", throwIfNotFound: true);
        m_Gameplay_Grab = m_Gameplay.FindAction("Grab", throwIfNotFound: true);
        m_Gameplay_Lightattack = m_Gameplay.FindAction("Light attack", throwIfNotFound: true);
        m_Gameplay_Dodge = m_Gameplay.FindAction("Dodge", throwIfNotFound: true);
        m_Gameplay_LockOn = m_Gameplay.FindAction("Lock On", throwIfNotFound: true);
        m_Gameplay_LookAround = m_Gameplay.FindAction("LookAround", throwIfNotFound: true);
        m_Gameplay_SpeedUp = m_Gameplay.FindAction("SpeedUp", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_StartFlying = m_Gameplay.FindAction("StartFlying", throwIfNotFound: true);
        m_Gameplay_StopFlying = m_Gameplay.FindAction("StopFlying", throwIfNotFound: true);
        // Flying
        m_Flying = asset.FindActionMap("Flying", throwIfNotFound: true);
        m_Flying_FlyUp = m_Flying.FindAction("FlyUp", throwIfNotFound: true);
        m_Flying_FlyDown = m_Flying.FindAction("FlyDown", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_ChargedAttack;
    private readonly InputAction m_Gameplay_Grab;
    private readonly InputAction m_Gameplay_Lightattack;
    private readonly InputAction m_Gameplay_Dodge;
    private readonly InputAction m_Gameplay_LockOn;
    private readonly InputAction m_Gameplay_LookAround;
    private readonly InputAction m_Gameplay_SpeedUp;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_StartFlying;
    private readonly InputAction m_Gameplay_StopFlying;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @ChargedAttack => m_Wrapper.m_Gameplay_ChargedAttack;
        public InputAction @Grab => m_Wrapper.m_Gameplay_Grab;
        public InputAction @Lightattack => m_Wrapper.m_Gameplay_Lightattack;
        public InputAction @Dodge => m_Wrapper.m_Gameplay_Dodge;
        public InputAction @LockOn => m_Wrapper.m_Gameplay_LockOn;
        public InputAction @LookAround => m_Wrapper.m_Gameplay_LookAround;
        public InputAction @SpeedUp => m_Wrapper.m_Gameplay_SpeedUp;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @StartFlying => m_Wrapper.m_Gameplay_StartFlying;
        public InputAction @StopFlying => m_Wrapper.m_Gameplay_StopFlying;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @ChargedAttack.started += instance.OnChargedAttack;
            @ChargedAttack.performed += instance.OnChargedAttack;
            @ChargedAttack.canceled += instance.OnChargedAttack;
            @Grab.started += instance.OnGrab;
            @Grab.performed += instance.OnGrab;
            @Grab.canceled += instance.OnGrab;
            @Lightattack.started += instance.OnLightattack;
            @Lightattack.performed += instance.OnLightattack;
            @Lightattack.canceled += instance.OnLightattack;
            @Dodge.started += instance.OnDodge;
            @Dodge.performed += instance.OnDodge;
            @Dodge.canceled += instance.OnDodge;
            @LockOn.started += instance.OnLockOn;
            @LockOn.performed += instance.OnLockOn;
            @LockOn.canceled += instance.OnLockOn;
            @LookAround.started += instance.OnLookAround;
            @LookAround.performed += instance.OnLookAround;
            @LookAround.canceled += instance.OnLookAround;
            @SpeedUp.started += instance.OnSpeedUp;
            @SpeedUp.performed += instance.OnSpeedUp;
            @SpeedUp.canceled += instance.OnSpeedUp;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @StartFlying.started += instance.OnStartFlying;
            @StartFlying.performed += instance.OnStartFlying;
            @StartFlying.canceled += instance.OnStartFlying;
            @StopFlying.started += instance.OnStopFlying;
            @StopFlying.performed += instance.OnStopFlying;
            @StopFlying.canceled += instance.OnStopFlying;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @ChargedAttack.started -= instance.OnChargedAttack;
            @ChargedAttack.performed -= instance.OnChargedAttack;
            @ChargedAttack.canceled -= instance.OnChargedAttack;
            @Grab.started -= instance.OnGrab;
            @Grab.performed -= instance.OnGrab;
            @Grab.canceled -= instance.OnGrab;
            @Lightattack.started -= instance.OnLightattack;
            @Lightattack.performed -= instance.OnLightattack;
            @Lightattack.canceled -= instance.OnLightattack;
            @Dodge.started -= instance.OnDodge;
            @Dodge.performed -= instance.OnDodge;
            @Dodge.canceled -= instance.OnDodge;
            @LockOn.started -= instance.OnLockOn;
            @LockOn.performed -= instance.OnLockOn;
            @LockOn.canceled -= instance.OnLockOn;
            @LookAround.started -= instance.OnLookAround;
            @LookAround.performed -= instance.OnLookAround;
            @LookAround.canceled -= instance.OnLookAround;
            @SpeedUp.started -= instance.OnSpeedUp;
            @SpeedUp.performed -= instance.OnSpeedUp;
            @SpeedUp.canceled -= instance.OnSpeedUp;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @StartFlying.started -= instance.OnStartFlying;
            @StartFlying.performed -= instance.OnStartFlying;
            @StartFlying.canceled -= instance.OnStartFlying;
            @StopFlying.started -= instance.OnStopFlying;
            @StopFlying.performed -= instance.OnStopFlying;
            @StopFlying.canceled -= instance.OnStopFlying;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Flying
    private readonly InputActionMap m_Flying;
    private List<IFlyingActions> m_FlyingActionsCallbackInterfaces = new List<IFlyingActions>();
    private readonly InputAction m_Flying_FlyUp;
    private readonly InputAction m_Flying_FlyDown;
    public struct FlyingActions
    {
        private @PlayerInput m_Wrapper;
        public FlyingActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @FlyUp => m_Wrapper.m_Flying_FlyUp;
        public InputAction @FlyDown => m_Wrapper.m_Flying_FlyDown;
        public InputActionMap Get() { return m_Wrapper.m_Flying; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FlyingActions set) { return set.Get(); }
        public void AddCallbacks(IFlyingActions instance)
        {
            if (instance == null || m_Wrapper.m_FlyingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FlyingActionsCallbackInterfaces.Add(instance);
            @FlyUp.started += instance.OnFlyUp;
            @FlyUp.performed += instance.OnFlyUp;
            @FlyUp.canceled += instance.OnFlyUp;
            @FlyDown.started += instance.OnFlyDown;
            @FlyDown.performed += instance.OnFlyDown;
            @FlyDown.canceled += instance.OnFlyDown;
        }

        private void UnregisterCallbacks(IFlyingActions instance)
        {
            @FlyUp.started -= instance.OnFlyUp;
            @FlyUp.performed -= instance.OnFlyUp;
            @FlyUp.canceled -= instance.OnFlyUp;
            @FlyDown.started -= instance.OnFlyDown;
            @FlyDown.performed -= instance.OnFlyDown;
            @FlyDown.canceled -= instance.OnFlyDown;
        }

        public void RemoveCallbacks(IFlyingActions instance)
        {
            if (m_Wrapper.m_FlyingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFlyingActions instance)
        {
            foreach (var item in m_Wrapper.m_FlyingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FlyingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FlyingActions @Flying => new FlyingActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnChargedAttack(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnLightattack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnLookAround(InputAction.CallbackContext context);
        void OnSpeedUp(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnStartFlying(InputAction.CallbackContext context);
        void OnStopFlying(InputAction.CallbackContext context);
    }
    public interface IFlyingActions
    {
        void OnFlyUp(InputAction.CallbackContext context);
        void OnFlyDown(InputAction.CallbackContext context);
    }
}
