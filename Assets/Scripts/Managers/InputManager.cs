using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : CustomBehaviour
{
    #region Attributes
    public int MoveCounter;
    public bool CanClickable;
    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        CanClickable = false;

        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += CanClickableChangeTrue;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }
    private void CanClickableChangeTrue()
    {
        
        CanClickable = true;
    }
    private void CanClickableChangeFalse()
    {
        CanClickable = false;
    }
    private void OnResetToMainMenu()
    {
        MoveCounter = 0;
        StartEqualMoveCount();
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
        yield return new WaitUntil(() => MoveCounter >= GameManager.Instance.LevelManager.CurrentLevelData.MoveCount);
        GameManager.Instance.LevelFailed();
    }
}
