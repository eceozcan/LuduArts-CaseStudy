using UnityEngine;
using LuduArtsCase.UI;
using LuduArtsCase.Core;
using LuduArtsCase.Interaction.Core;
using LuduArtsCase.Interaction.Interfaces;

namespace LuduArtsCase.Interaction.Interactor
{
    public sealed class InteractorBehaviour : MonoBehaviour, IInteractor
    {
        #region Fields

        [Header("References")]
        [SerializeField] private InteractionSettings m_Settings;
        [SerializeField] private Transform m_Origin;
        [SerializeField] private InteractionPromptView m_PromptView;

        [Header("Debug")]
        [SerializeField] private bool m_Debug;

        private readonly InteractionDetector m_Detector = new InteractionDetector();

        private IInteractable m_Current;

        // Hold state
        private IInteractable m_HoldTarget;
        private float m_HoldTimer;
        private bool m_HoldTriggered;

        #endregion

        #region Interface Implementations

        Transform IInteractor.Origin => m_Origin != null ? m_Origin : transform;
        GameObject IInteractor.Owner => gameObject;

        #endregion

        #region Unity Methods

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
                ResetHoldState();

                if (m_PromptView != null)
                {
                    m_PromptView.Hide();
                }

                return;
            }

            // If target changed, reset hold progress
            if (!ReferenceEquals(m_Current, m_HoldTarget))
            {
                ResetHoldState();
                m_HoldTarget = m_Current;
            }

            if (!m_Current.CanInteract(this))
            {
                // Don't spam every frame; only show feedback when player tries
                if (Input.GetKeyDown(m_Settings.InteractKey) && m_Debug)
                {
                    Debug.Log($"Cannot interact with {m_Current.Transform.name}", this);
                }

                // Still show prompt (optional). If you want prompt hidden when cannot interact, comment this out.
                UpdatePromptUI(m_Current);
                return;
            }

            UpdatePromptUI(m_Current);
            HandleInteractionInput(m_Current);
        }

        #endregion

        #region Methods

        private void UpdateCurrentTarget()
        {
            Vector3 originPos = (m_Origin != null) ? m_Origin.position : transform.position;

            if (m_Debug)
            {
                Debug.DrawLine(originPos, originPos + Vector3.up * 0.25f, Color.yellow);
            }

            if (!m_Detector.TryFindNearest(
                    originPos,
                    m_Settings.InteractionRange,
                    m_Settings.InteractableLayers,
                    out IInteractable nearest,
                    out float nearestDistance))
            {
                m_Current = null;
                return;
            }

            if (m_Debug && Time.frameCount % 60 == 0)
            {
                Debug.Log($"Nearest: {nearest.Transform.name} ({nearestDistance:F2}m)", this);
            }

            m_Current = nearest;
        }

        private void HandleInteractionInput(IInteractable target)
        {
            switch (target.Type)
            {
                case InteractionType.Instant:
                case InteractionType.Toggle:
                    if (Input.GetKeyDown(m_Settings.InteractKey))
                    {
                        if (m_Debug)
                        {
                            Debug.Log($"Interact pressed. Target: {target.Transform.name} | Type: {target.Type}", this);
                        }

                        target.Interact(this);
                    }
                    break;

                case InteractionType.Hold:
                    HandleHoldInteraction(target);
                    break;

                default:
                    // Safe fallback
                    if (Input.GetKeyDown(m_Settings.InteractKey))
                    {
                        target.Interact(this);
                    }
                    break;
            }
        }

        private void HandleHoldInteraction(IInteractable target)
        {
            float duration = Mathf.Max(0.01f, target.HoldDuration);

            if (Input.GetKey(m_Settings.InteractKey))
            {
                if (!ReferenceEquals(m_HoldTarget, target))
                {
                    ResetHoldState();
                    m_HoldTarget = target;
                }

                m_HoldTimer += Time.deltaTime;

                if (m_Debug && Time.frameCount % 15 == 0)
                {
                    float progress = Mathf.Clamp01(m_HoldTimer / duration);
                    Debug.Log($"Hold progress: {progress:P0} on {target.Transform.name}", this);
                }

                if (!m_HoldTriggered && m_HoldTimer >= duration)
                {
                    m_HoldTriggered = true;

                    if (m_Debug)
                    {
                        Debug.Log($"Hold complete. Interact with {target.Transform.name}", this);
                    }

                    target.Interact(this);
                }
            }
            else
            {
                // Key released
                if (m_HoldTimer > 0f)
                {
                    ResetHoldState();
                }
            }
        }

        private void ResetHoldState()
        {
            m_HoldTarget = null;
            m_HoldTimer = 0f;
            m_HoldTriggered = false;
        }

        private void UpdatePromptUI(IInteractable target)
        {
            if (m_PromptView == null || m_Settings == null || target == null)
            {
                return;
            }

            string keyLabel = m_Settings.InteractKey.ToString();
            string prompt = string.IsNullOrWhiteSpace(target.Prompt) ? "Interact" : target.Prompt;

            float holdProgress01 = 0f;
            string text;

            if (target.Type == InteractionType.Hold)
            {
                float duration = Mathf.Max(0.01f, target.HoldDuration);

                if (ReferenceEquals(target, m_HoldTarget))
                {
                    holdProgress01 = Mathf.Clamp01(m_HoldTimer / duration);
                }

                text = $"Hold {keyLabel} to {prompt}";
            }
            else
            {
                text = $"Press {keyLabel} to {prompt}";
            }

            m_PromptView.Show(text, holdProgress01);
        }

        #endregion
    }
}
