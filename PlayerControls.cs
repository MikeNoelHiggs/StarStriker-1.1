// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace playerControls
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Global"",
            ""id"": ""8f48677a-2137-49b7-8211-b78838fb7817"",
            ""actions"": [
                {
                    ""name"": ""XMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0bb2a535-d39f-47d4-ac2a-7ee8ccf59d9f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""YMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4036889a-5bac-41b9-986e-eb1009f4f701"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZMovement"",
                    ""type"": ""Button"",
                    ""id"": ""9eac6cd5-d349-4d8d-8f54-8d1db6115b7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""09b7b04d-82c0-4973-9ede-742b90bcf5ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Missiles"",
                    ""type"": ""Button"",
                    ""id"": ""bd865c21-1146-4b97-a47c-310bff1fa05e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shield"",
                    ""type"": ""Button"",
                    ""id"": ""06f73072-f74e-4bd9-a647-9b39af580c7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d7f273df-7262-473d-b289-d83618fc6313"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IceToggle"",
                    ""type"": ""Button"",
                    ""id"": ""506d4327-ab53-4815-ab5b-26df05fea3a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""X"",
                    ""id"": ""344f274f-1552-460d-85bd-64e52b48c61e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6ba6224d-ee0e-463d-9c32-895da50685d6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""54c2f404-0dc5-4ef4-a348-5e66d43a07d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2f2c5fcb-039d-4a01-8563-db12a3aa2be7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Y"",
                    ""id"": ""73965dac-b1d4-4cbc-970f-f82aad3feecf"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""996dede9-b2d1-483f-9fce-f35fbb528709"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0ace8d9a-7d93-425b-940c-21752570b2bf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dffb8c22-f8fb-4421-a16c-85b8ef449683"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""290c45ab-98ea-4c7a-bd54-ba1307c69d21"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Z"",
                    ""id"": ""b34a16f4-a3cc-4c1a-b930-d831411df140"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f75280c3-65a3-44f0-96d8-22bec006e7b2"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""15fe4b68-3aaa-45bf-8df8-9ac7c53ff8db"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4a550ea9-c88a-4099-9a2b-cce2aea87911"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Missiles"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""121a84d3-645a-4e52-a656-31750dba3797"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IceToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");

            // Global
            m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
            m_Global_XMovement = m_Global.FindAction("XMovement", throwIfNotFound: true);
            m_Global_YMovement = m_Global.FindAction("YMovement", throwIfNotFound: true);
            m_Global_ZMovement = m_Global.FindAction("ZMovement", throwIfNotFound: true);
            m_Global_Fire = m_Global.FindAction("Fire", throwIfNotFound: true);
            m_Global_Missiles = m_Global.FindAction("Missiles", throwIfNotFound: true);
            m_Global_Shield = m_Global.FindAction("Shield", throwIfNotFound: true);
            m_Global_Pause = m_Global.FindAction("Pause", throwIfNotFound: true);
            m_Global_IceToggle = m_Global.FindAction("IceToggle", throwIfNotFound: true);
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

        // Global
        private readonly InputActionMap m_Global;
        private IGlobalActions m_GlobalActionsCallbackInterface;
        private readonly InputAction m_Global_XMovement;
        private readonly InputAction m_Global_YMovement;
        private readonly InputAction m_Global_ZMovement;
        private readonly InputAction m_Global_Fire;
        private readonly InputAction m_Global_Missiles;
        private readonly InputAction m_Global_Shield;
        private readonly InputAction m_Global_Pause;
        private readonly InputAction m_Global_IceToggle;
        public struct GlobalActions
        {
            private @PlayerControls m_Wrapper;
            public GlobalActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @XMovement => m_Wrapper.m_Global_XMovement;
            public InputAction @YMovement => m_Wrapper.m_Global_YMovement;
            public InputAction @ZMovement => m_Wrapper.m_Global_ZMovement;
            public InputAction @Fire => m_Wrapper.m_Global_Fire;
            public InputAction @Missiles => m_Wrapper.m_Global_Missiles;
            public InputAction @Shield => m_Wrapper.m_Global_Shield;
            public InputAction @Pause => m_Wrapper.m_Global_Pause;
            public InputAction @IceToggle => m_Wrapper.m_Global_IceToggle;
            public InputActionMap Get() { return m_Wrapper.m_Global; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
            public void SetCallbacks(IGlobalActions instance)
            {
                if (m_Wrapper.m_GlobalActionsCallbackInterface != null)
                {
                    @XMovement.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnXMovement;
                    @XMovement.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnXMovement;
                    @XMovement.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnXMovement;
                    @YMovement.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnYMovement;
                    @YMovement.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnYMovement;
                    @YMovement.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnYMovement;
                    @ZMovement.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnZMovement;
                    @ZMovement.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnZMovement;
                    @ZMovement.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnZMovement;
                    @Fire.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnFire;
                    @Missiles.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMissiles;
                    @Missiles.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMissiles;
                    @Missiles.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMissiles;
                    @Shield.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnShield;
                    @Shield.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnShield;
                    @Shield.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnShield;
                    @Pause.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnPause;
                    @IceToggle.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnIceToggle;
                    @IceToggle.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnIceToggle;
                    @IceToggle.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnIceToggle;
                }
                m_Wrapper.m_GlobalActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @XMovement.started += instance.OnXMovement;
                    @XMovement.performed += instance.OnXMovement;
                    @XMovement.canceled += instance.OnXMovement;
                    @YMovement.started += instance.OnYMovement;
                    @YMovement.performed += instance.OnYMovement;
                    @YMovement.canceled += instance.OnYMovement;
                    @ZMovement.started += instance.OnZMovement;
                    @ZMovement.performed += instance.OnZMovement;
                    @ZMovement.canceled += instance.OnZMovement;
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                    @Missiles.started += instance.OnMissiles;
                    @Missiles.performed += instance.OnMissiles;
                    @Missiles.canceled += instance.OnMissiles;
                    @Shield.started += instance.OnShield;
                    @Shield.performed += instance.OnShield;
                    @Shield.canceled += instance.OnShield;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                    @IceToggle.started += instance.OnIceToggle;
                    @IceToggle.performed += instance.OnIceToggle;
                    @IceToggle.canceled += instance.OnIceToggle;
                }
            }
        }
        public GlobalActions @Global => new GlobalActions(this);
        public interface IGlobalActions
        {
            void OnXMovement(InputAction.CallbackContext context);
            void OnYMovement(InputAction.CallbackContext context);
            void OnZMovement(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
            void OnMissiles(InputAction.CallbackContext context);
            void OnShield(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnIceToggle(InputAction.CallbackContext context);
        }
    }
}
