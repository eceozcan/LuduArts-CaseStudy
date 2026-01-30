using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LuduArtsCase.UI
{
    public sealed class InteractionPromptView : MonoBehaviour
    {
        #region Inspector Fields

        [Header("References")]
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private TMP_Text m_PromptText;
        [SerializeField] private Image m_HoldFillImage;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SetVisible(false);
            SetHoldProgress(0f);
        }

        #endregion

        #region Public Methods

        // Compatibility wrappers
        public void Show(bool isVisible)
        {
            SetVisible(isVisible);
        }

        public void Show(string text)
        {
            SetVisible(true);
            SetPrompt(text);
            SetHoldProgress(0f);
        }

        public void Show(string text, float holdProgress01)
        {
            SetVisible(true);
            SetPrompt(text);
            SetHoldProgress(holdProgress01);
        }

        public void Hide()
        {
            SetVisible(false);
            SetHoldProgress(0f);
        }

        public void SetVisible(bool isVisible)
        {
            if (m_CanvasGroup == null)
            {
                return;
            }

            m_CanvasGroup.alpha = isVisible ? 1f : 0f;
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
        }

        public void SetPrompt(string text)
        {
            if (m_PromptText == null)
            {
                return;
            }

            m_PromptText.text = text;
        }

        public void SetHoldProgress(float normalized)
        {
            if (m_HoldFillImage == null)
            {
                return;
            }

            m_HoldFillImage.fillAmount = Mathf.Clamp01(normalized);
        }

        #endregion
    }
}
