using UnityEngine;

namespace LuduArtsCase.Core.Inventory
{
    [CreateAssetMenu(menuName = "LuduArtsCase/Items/Key Item", fileName = "SO_Key_")]
    public sealed class SO_KeyItem : SO_Item
    {
        #region Fields

        [SerializeField] private string m_DisplayName = "Key";

        #endregion

        #region Properties

        public string DisplayName => m_DisplayName;

        #endregion
    }
}
