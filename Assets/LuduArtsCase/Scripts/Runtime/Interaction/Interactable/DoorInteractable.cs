using UnityEngine;
using LuduArtsCase.Core.Inventory;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class DoorInteractable : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Door")]
        [SerializeField] private Transform m_DoorPivot;
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_OpenCloseSpeed = 6f;

        [Header("Lock")]
        [SerializeField] private bool m_IsLocked = true;
        [SerializeField] private SO_KeyItem m_RequiredKey;

        [Header("Prompt")]
        [SerializeField] private string m_OpenPrompt = "Open door";
        [SerializeField] private string m_ClosePrompt = "Close door";
        [SerializeField] private string m_LockedPrompt = "Door is locked (Key required)";

        private bool m_IsOpen;
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;

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

                return m_IsOpen ? m_ClosePrompt : m_OpenPrompt;
            }
        }

        public Transform Transform => transform;
        public InteractionType Type => InteractionType.Toggle;
        public float HoldDuration => 0f;

        public bool CanInteract(IInteractor interactor)
        {
            if (interactor == null)
            {
                return false;
            }

            if (!m_IsLocked)
            {
                return true;
            }

            // Locked => require key
            if (m_RequiredKey == null)
            {
                return false;
            }

            PlayerInventory inventory = interactor.Owner.GetComponentInParent<PlayerInventory>();
            if (inventory == null)
            {
                return false;
            }

            return inventory.HasKey(m_RequiredKey);
        }

        public void Interact(IInteractor interactor)
        {
            if (m_IsLocked)
            {
                // If we are locked but CanInteract returned true => key exists, unlock
                m_IsLocked = false;
            }

            m_IsOpen = !m_IsOpen;
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_DoorPivot == null)
            {
                m_DoorPivot = transform;
            }

            m_ClosedRotation = m_DoorPivot.localRotation;
            m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0f, m_OpenAngle, 0f);
        }

        private void Update()
        {
            if (m_DoorPivot == null)
            {
                return;
            }

            Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
            m_DoorPivot.localRotation = Quaternion.Slerp(m_DoorPivot.localRotation, target, Time.deltaTime * m_OpenCloseSpeed);
        }

        #endregion
    }
}
