using UnityEngine;

namespace LuduArtsCase.Core
{
    [CreateAssetMenu(fileName = "SO_InteractionSettings", menuName = "LuduArtsCase/Settings/InteractionSettings")]
    public sealed class InteractionSettings : ScriptableObject
    {
        #region Fields

        [Header("Detection")]
        [SerializeField] private float m_InteractionRange = 2.5f;
        [SerializeField] private LayerMask m_InteractableLayers = ~0;

        [Header("Input")]
        [SerializeField] private KeyCode m_InteractKey = KeyCode.E;

        #endregion

        #region Properties

        public float InteractionRange => m_InteractionRange;
        public LayerMask InteractableLayers => m_InteractableLayers;
        public KeyCode InteractKey => m_InteractKey;

        #endregion
    }
}
