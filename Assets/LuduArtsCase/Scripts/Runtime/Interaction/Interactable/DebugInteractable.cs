using UnityEngine;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class DebugInteractable : MonoBehaviour, IInteractable
    {
        #region Fields

        [SerializeField] private string m_Prompt = "Press E to Interact";

        #endregion

        #region Properties

        public string Prompt => m_Prompt;
        public Transform Transform => transform;

        #endregion

        #region Interface Implementations

        bool IInteractable.CanInteract(IInteractor interactor)
        {
            return true;
        }

        void IInteractable.Interact(IInteractor interactor)
        {
            Debug.Log($"Interacted with {name} by {interactor.Owner.name}");
        }

        #endregion
    }
}
