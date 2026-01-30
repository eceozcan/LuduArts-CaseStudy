using UnityEngine;
using UnityEngine.Events;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class SwitchInteractable : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Prompt")]
        [SerializeField] private string m_OnPrompt = "Turn on";
        [SerializeField] private string m_OffPrompt = "Turn off";

        [Header("Events")]
        [SerializeField] private UnityEvent m_OnSwitchedOn;
        [SerializeField] private UnityEvent m_OnSwitchedOff;

        private bool m_IsOn;

        #endregion

        #region IInteractable

        public string Prompt => m_IsOn ? m_OffPrompt : m_OnPrompt;
        public Transform Transform => transform;
        public InteractionType Type => InteractionType.Toggle;
        public float HoldDuration => 0f;

        public bool CanInteract(IInteractor interactor)
        {
            return interactor != null;
        }

        public void Interact(IInteractor interactor)
        {
            m_IsOn = !m_IsOn;

            if (m_IsOn)
            {
                m_OnSwitchedOn?.Invoke();
            }
            else
            {
                m_OnSwitchedOff?.Invoke();
            }
        }

        #endregion
    }
}
