using System.Collections.Generic;
using UnityEngine;

namespace LuduArtsCase.Core.Inventory
{
    public sealed class PlayerInventory : MonoBehaviour
    {
        #region Fields

        private readonly HashSet<string> m_KeyIds = new HashSet<string>();

        #endregion

        #region Public Methods

        public bool AddKey(SO_KeyItem key)
        {
            if (key == null)
            {
                return false;
            }

            return m_KeyIds.Add(key.Id);
        }

        public bool HasKey(SO_KeyItem key)
        {
            if (key == null)
            {
                return false;
            }

            return m_KeyIds.Contains(key.Id);
        }

        public IReadOnlyCollection<string> GetAllKeyIds()
        {
            return m_KeyIds;
        }

        #endregion
    }
}
