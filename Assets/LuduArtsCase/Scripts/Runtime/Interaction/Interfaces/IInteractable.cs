using UnityEngine;
using LuduArtsCase.Interaction.Core;

namespace LuduArtsCase.Interaction.Interfaces
{
    public interface IInteractable
    {
        string Prompt { get; }
        Transform Transform { get; }

        InteractionType Type { get; }
        float HoldDuration { get; }

        bool CanInteract(IInteractor interactor);
        void Interact(IInteractor interactor);
    }
}
