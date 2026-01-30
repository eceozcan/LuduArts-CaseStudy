using UnityEngine;
using LuduArtsCase.Core.Inventory;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactable
{
    public sealed class KeyPickupInteractable : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Key")]
        [SerializeField] private SO_KeyItem m_Key;

        [Header("Prompt")]
        [SerializeField] private string m_Prompt = "Pick up key";

        #endregion

        #region IInteractable

        public string Prompt => m_Prompt;
        public Transform Transform => transform;
        public InteractionType Type => InteractionType.Instant;
        public float HoldDuration => 0f;

        public bool CanInteract(IInteractor interactor)
        {
            return interactor != null && m_Key != null;
        }

        public void Interact(IInteractor interactor)
        {
            if (interactor == null)
            {
                return;
            }

            PlayerInventory inventory = interactor.Owner.GetComponent<PlayerInventory>();
            if (inventory == null)
            {
                Debug.LogWarning("PlayerInventory not found on interactor owner.");
                return;
            }

            bool added = inventory.AddKey(m_Key);
            Debug.Log(added ? $"Picked up: {m_Key.DisplayName}" : $"Already have: {m_Key.DisplayName}");

            // Destroy pickup after successful add
            if (added)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
