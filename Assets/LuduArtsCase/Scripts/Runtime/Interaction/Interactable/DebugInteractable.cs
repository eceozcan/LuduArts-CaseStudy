using UnityEngine;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class DebugInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string m_Prompt = "Interact";
        [SerializeField] private InteractionType m_Type = InteractionType.Instant;
        [SerializeField, Min(0f)] private float m_HoldDuration;

        public Transform Transform => transform;
        public string Prompt => m_Prompt;
        public InteractionType Type => m_Type;
        public float HoldDuration => m_Type == InteractionType.Hold ? m_HoldDuration : 0f;

        public bool CanInteract(IInteractor interactor)
        {
            return interactor != null;
        }

        void IInteractable.Interact(IInteractor interactor)
        {
            Debug.Log($"Interacted with {name} by {interactor.Owner.name}");
        }
    }
}
