using TMPro;
using UnityEngine;

namespace LuduArtsCase.UI
{
    public sealed class InteractionPromptView : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private GameObject m_Root;
        [SerializeField] private TMP_Text m_PromptText;
        [SerializeField] private UnityEngine.UI.Image m_HoldFill;

        #endregion

        #region Methods

        public void Hide()
        {
            if (m_Root != null)
            {
                m_Root.SetActive(false);
            }
        }

        public void Show(string text, float holdProgress01)
        {
            if (m_Root != null && !m_Root.activeSelf)
            {
                m_Root.SetActive(true);
            }

            if (m_PromptText != null)
            {
                m_PromptText.text = text;
            }

            if (m_HoldFill != null)
            {
                m_HoldFill.gameObject.SetActive(holdProgress01 > 0f);
                m_HoldFill.fillAmount = Mathf.Clamp01(holdProgress01);
            }
        }

        #endregion
    }
}
