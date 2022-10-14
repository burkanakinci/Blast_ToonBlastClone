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

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void UpdateCoinCountData(int collectedCoin)
    {

    }
    public int GetTotalCoinCount()
    {
        return 0;
    }
    public void UpdateLevelData(int _levelNumber)
    {
        GameManager.Instance.JsonConverter.PlayerData.LevelNumber = _levelNumber;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }

    public int GetLevelNumber()
    {
        return GameManager.Instance.JsonConverter.PlayerData.LevelNumber;
    }

    #region Events

    public void CoinCountUpdated(int coinCount)
    {
        UpdateCoinCountData(coinCount);

        OnCoinCountChanged?.Invoke();
    }

    private void OnResetToMainMenu()
    {
        m_PlayerStateMachine.ChangeStateTo(PlayerStates.Idle, true);
    }

    private void OnLevelCompleted()
    {
    }

    private void OnLevelFailed()
    {
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }

    #endregion
}
