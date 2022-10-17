using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_MoveCountText;
    [SerializeField] private TextMeshProUGUI m_TargetCountText;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.Instance.OnGameStart += ShowPanel;
        GameManager.Instance.InputManager.OnClicked += SetMoveCountText;
        GameManager.Instance.LevelManager.OnTargetCountUpdate+=SetTargetCountText;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStart -= ShowPanel;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        SetMoveCountText();
        SetTargetCountText();
    }
    private int m_TempMoveCount;
    private void SetMoveCountText()
    {
        m_TempMoveCount = GameManager.Instance.LevelManager.CurrentLevelData.MoveCount - GameManager.Instance.InputManager.MoveCount;
        m_MoveCountText.text = "MOVE : " + m_TempMoveCount;
    }

    private int m_TempTargetCount;
    private void SetTargetCountText()
    {
        m_TempTargetCount = GameManager.Instance.LevelManager.CurrentTargetCount;
        m_TargetCountText.text = "TARGET : " + m_TempTargetCount;
    }
}
