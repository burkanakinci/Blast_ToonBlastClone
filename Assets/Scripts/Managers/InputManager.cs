using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : CustomBehaviour
{
    #region Attributes
    private int m_MoveCount;
    private bool m_CanClickable;
    #endregion
    #region ExternalAccess
    public int MoveCount => m_MoveCount;
    public bool CanClickable => m_CanClickable;
    #endregion
    #region Actions
    public event Action OnClicked;
    #endregion    
    public override void Initialize()
    {
        m_CanClickable = false;

        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += CanClickableChangeTrue;
        GameManager.Instance.OnGameStart += CanClickableChangeTrue;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        OnClicked += OnClickedInput;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelCompleted;
    }
    public void Clicked()
    {
        OnClicked?.Invoke();
    }
    private void CanClickableChangeTrue()
    {
        if (GameManager.Instance.PlayerManager.PlayerStateMachine.EqualsCurrentState(PlayerStates.GameState))
        {
            m_CanClickable = true;
        }
    }
    private void CanClickedChangeFalse()
    {
        m_CanClickable = false;
    }
    private void OnResetToMainMenu()
    {
        m_MoveCount = 0;
        StartEqualMoveCount();
    }
    private void OnClickedInput()
    {
        IncreaseMoveCounter();
        CanClickedChangeFalse();
    }
    private void IncreaseMoveCounter()
    {
        m_MoveCount++;
    }
    private void OnLevelCompleted()
    {
        CanClickedChangeFalse();
    }

    private Coroutine m_StartEqualMoveCount;
    private void StartEqualMoveCount()
    {
        if (m_StartEqualMoveCount != null)
        {
            StopCoroutine(m_StartEqualMoveCount);
        }
        m_StartEqualMoveCount = StartCoroutine(EqualMoveCount());
    }
    private IEnumerator EqualMoveCount()
    {
        yield return new WaitUntil(() => ((m_MoveCount >= GameManager.Instance.LevelManager.CurrentLevelData.MoveCount) && (m_CanClickable)));
        GameManager.Instance.LevelFailed();
    }
}
