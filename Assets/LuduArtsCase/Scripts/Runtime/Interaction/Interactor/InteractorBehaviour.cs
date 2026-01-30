using UnityEngine;
using LuduArtsCase.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactor
{
    public sealed class InteractorBehaviour : MonoBehaviour, IInteractor
    {
        [Header("References")]
        [SerializeField] private InteractionSettings m_Settings;
        [SerializeField] private Transform m_Origin;

        [Header("Debug")]
        [SerializeField] private bool m_Debug;

        private readonly InteractionDetector m_Detector = new InteractionDetector();
        private IInteractable m_Current;

        Transform IInteractor.Origin => m_Origin != null ? m_Origin : transform;
        GameObject IInteractor.Owner => gameObject;

        private void Reset()
        {
            m_Origin = transform;
        }

        private void Awake()
        {
            if (m_Origin == null)
            {
                m_Origin = transform;
            }
        }

        private void Update()
        {
            if (m_Settings == null)
            {
                Debug.LogError($"{nameof(InteractorBehaviour)}: Settings is not assigned.", this);
                return;
            }

            UpdateCurrentTarget();

            if (m_Current == null)
            {
                return;
            }

            if (Input.GetKeyDown(m_Settings.InteractKey))
            {
                if (m_Debug)
                {
                    Debug.Log($"Interact pressed. Target: {m_Current.Transform.name}", this);
                }

                TryInteractWithCurrent();
            }
        }

        private void UpdateCurrentTarget()
        {
            Vector3 originPos = (m_Origin != null) ? m_Origin.position : transform.position;

            if (m_Debug)
            {
                Debug.DrawLine(originPos, originPos + Vector3.up * 0.25f, Color.yellow);
            }

            if (!m_Detector.TryFindNearest(originPos, m_Settings.InteractionRange, m_Settings.InteractableLayers, out IInteractable nearest, out float dist))
            {
                m_Current = null;
                return;
            }

            if (m_Debug && Time.frameCount % 60 == 0)
            {
                Debug.Log($"Nearest: {nearest.Transform.name} ({dist:F2}m)", this);
            }

            m_Current = nearest;
        }

        private void TryInteractWithCurrent()
        {
            if (m_Current == null)
            {
                return;
            }

            if (!m_Current.CanInteract(this))
            {
                if (m_Debug)
                {
                    Debug.Log($"Cannot interact with {m_Current.Transform.name}", this);
                }

                return;
            }

            m_Current.Interact(this);
        }
    }
}
