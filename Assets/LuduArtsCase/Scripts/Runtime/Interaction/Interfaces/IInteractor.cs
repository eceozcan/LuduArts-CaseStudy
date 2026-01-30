using UnityEngine;

namespace LuduArtsCase.Interaction.Interfaces
{
    public interface IInteractor
    {
        Transform Origin { get; }
        GameObject Owner { get; }
    }
}
