using UnityEngine;
using LuduArtsCase.Core.Inventory;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class DoorInteractable : MonoBehaviour, IInteractable, IToggleTarget
    {
        #region Fields

        [Header("Door State")]
        [SerializeField] private bool m_IsLocked = true;
        [SerializeField] private bool m_IsOpen;

        [Header("Prompts")]
        [SerializeField] private string m_LockedPrompt = "Locked (Key Required)";
        [SerializeField] private string m_OpenPrompt = "Close Door";
        [SerializeField] private string m_ClosedPrompt = "Open Door";

        [Header("Key Requirement")]
        [SerializeField] private SO_KeyItem m_RequiredKey;

        [Header("Visual")]
        [SerializeField] private Transform m_DoorPivot;
        [SerializeField] private float m_OpenYaw = 90f;
        [SerializeField] private float m_CloseYaw;

        #endregion

        #region Unity Methods

        private void Start()
        {
            ApplyVisualState();
        }

        #endregion

        #region IInteractable

        public string Prompt
        {
            get
            {
                if (m_IsLocked)
                {
                    return m_LockedPrompt;
                }

                return m_IsOpen ? m_OpenPrompt : m_ClosedPrompt;
            }
        }

        public Transform Transform => transform;
        public InteractionType Type => InteractionType.Toggle;
        public float HoldDuration => 0f;

        public bool CanInteract(IInteractor interactor)
        {
            if (!m_IsLocked)
            {
                return true;
            }

            if (m_RequiredKey == null)
            {
                Debug.LogWarning($"{nameof(DoorInteractable)}: Required key is not assigned.", this);
                return false;
            }

            if (!TryGetInventory(interactor, out PlayerInventory inventory))
            {
                return false;
            }

            return inventory.HasKey(m_RequiredKey);
        }

        public void Interact(IInteractor interactor)
        {
            if (m_IsLocked)
            {
                if (m_RequiredKey == null)
                {
                    Debug.LogWarning($"{nameof(DoorInteractable)}: Required key is not assigned.", this);
                    return;
                }

                if (!TryGetInventory(interactor, out PlayerInventory inventory))
                {
                    Debug.LogWarning("PlayerInventory not found on interactor owner.", this);
                    return;
                }

                if (!inventory.HasKey(m_RequiredKey))
                {
                    Debug.Log("Door is locked. Key required.", this);
                    return;
                }

                // Unlock door
                m_IsLocked = false;
            }

            Toggle();
        }

        #endregion

        #region IToggleTarget

        /// <summary>
        /// Toggles door open/close state. External triggers cannot bypass lock.
        /// </summary>
        public void Toggle()
        {
            if (m_IsLocked)
            {
                // External trigger should not bypass lock.
                return;
            }

            m_IsOpen = !m_IsOpen;
            ApplyVisualState();
        }

        #endregion

        #region Helpers

        private void ApplyVisualState()
        {
            if (m_DoorPivot == null)
            {
                return;
            }

            float yaw = m_IsOpen ? m_OpenYaw : m_CloseYaw;

            Vector3 euler = m_DoorPivot.localEulerAngles;
            euler.y = yaw;
            m_DoorPivot.localEulerAngles = euler;
        }

        private static bool TryGetInventory(IInteractor interactor, out PlayerInventory inventory)
        {
            inventory = null;

            if (interactor == null || interactor.Owner == null)
            {
                return false;
            }

            return interactor.Owner.TryGetComponent(out inventory);
        }

        #endregion
    }
}
