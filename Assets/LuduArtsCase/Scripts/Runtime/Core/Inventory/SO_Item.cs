using UnityEngine;

namespace LuduArtsCase.Core.Inventory
{
    public abstract class SO_Item : ScriptableObject
    {
        #region Properties

        public string Id => name;

        #endregion
    }
}
