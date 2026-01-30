using UnityEngine;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactor
{
    public sealed class InteractionDetector
    {
        private readonly Collider[] m_Results = new Collider[32];

        public bool TryFindNearest(
            Vector3 origin,
            float range,
            LayerMask layerMask,
            out IInteractable nearest,
            out float nearestDistance)
        {
            nearest = null;
            nearestDistance = float.MaxValue;

            int count = Physics.OverlapSphereNonAlloc(origin, range, m_Results, layerMask, QueryTriggerInteraction.Collide);
            if (count <= 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                Collider col = m_Results[i];
                if (col == null)
                {
                    continue;
                }

                IInteractable interactable = col.GetComponentInParent<IInteractable>();
                if (interactable == null)
                {
                    continue;
                }

                float dist = Vector3.Distance(origin, interactable.Transform.position);
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearest = interactable;
                }
            }

            return nearest != null;
        }
    }
}
