using UnityEngine;

namespace LuduArtsCase.Interaction.Interfaces
{
    public interface IInteractable
    {
        string Prompt { get; }
        Transform Transform { get; }

        bool CanInteract(IInteractor interactor);
        void Interact(IInteractor interactor);
    }
}
