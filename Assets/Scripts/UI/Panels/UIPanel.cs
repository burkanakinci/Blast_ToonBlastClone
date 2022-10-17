using System;
using System.Collections;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    private CanvasGroup m_CanvasGroup;
    public CanvasGroup CanvasGroup => m_CanvasGroup;
    private UIManager m_UIManager;

    public virtual void Initialize(UIManager uiManager)
    {
        m_UIManager = uiManager;
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void ShowPanel()
    {
        m_UIManager.HideAllPanels();

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        CanvasGroup.Open();
        SetCurrentPanel();
    }

    public virtual void HidePanel()
    {
        CanvasGroup.Close();
        this.gameObject.SetActive(false);
    }

    public virtual void SetCurrentPanel()
    {
        m_UIManager.SetCurrentUIPanel(this);
    }
}
