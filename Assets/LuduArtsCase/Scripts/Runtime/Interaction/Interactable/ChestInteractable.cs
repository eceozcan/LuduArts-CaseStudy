using UnityEngine;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class ChestInteractable : MonoBehaviour, IInteractable
    {
        #region Inspector Fields

        [Header("State")]
        [SerializeField] private bool m_IsOpened;

        [Header("Hold Settings")]
        [SerializeField] private float m_HoldDuration = 2f;

        [Header("Prompts")]
        [SerializeField] private string m_PromptClosed = "Open Chest";
        [SerializeField] private string m_PromptOpened = "Chest Opened";

        [Header("Debug")]
        [SerializeField] private bool m_DebugLog;

        #endregion

        #region IInteractable

        public string Prompt => m_IsOpened ? m_PromptOpened : m_PromptClosed;

        public Transform Transform => transform;

        public InteractionType Type => InteractionType.Hold;

        public float HoldDuration => m_HoldDuration;

        public bool CanInteract(IInteractor interactor)
        {
            if (m_IsOpened)
            {
                return false;
            }

         
            if (interactor == null)
            {
                return false;
            }

            return true;
        }

        public void Interact(IInteractor interactor)
        {
            if (!CanInteract(interactor))
            {
                if (m_DebugLog)
                {
                    Debug.Log("Chest cannot be interacted with (already opened or invalid interactor).", this);
                }

                return;
            }

            m_IsOpened = true;

            if (m_DebugLog)
            {
                Debug.Log($"Chest opened by {interactor.Owner.name}", this);
            }

        }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (m_HoldDuration < 0.1f)
            {
                m_HoldDuration = 0.1f;
            }
        }

        #endregion
    }
}
