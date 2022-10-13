using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    private PlayerStateMachine m_PlayerStateMachine;
    public event Action OnCoinCountChanged;

    public override void Initialize()
    {
        m_PlayerStateMachine = new PlayerStateMachine(this);

        m_PlayerStateMachine.ChangeStateTo(PlayerStates.Idle, true);
    }

    private void UpdateCoinCountData(int collectedCoin)
    {

    }
    public int GetTotalCoinCount()
    {
        return 0;
    }
    public void UpdateLevelData()
    {

    }

    public int GetLevelNumber()
    {
        return 0;
    }

    #region Events

    public void CoinCountUpdated(int coinCount)
    {
        UpdateCoinCountData(coinCount);

        OnCoinCountChanged?.Invoke();
    }

    private void OnStartGame()
    {

    }

    private void OnResetToMainMenu()
    {
    }

    private void OnGameFinished()
    {

    }

    private void OnLevelCompleted()
    {
    }

    private void OnLevelFailed()
    {
    }

    private void OnDestroy()
    {
    }

    #endregion
}
