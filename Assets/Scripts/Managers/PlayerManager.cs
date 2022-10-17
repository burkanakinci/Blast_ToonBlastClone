using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    private PlayerStateMachine m_PlayerStateMachine;
    public PlayerStateMachine PlayerStateMachine => m_PlayerStateMachine;

    public override void Initialize()
    {
        m_PlayerStateMachine = new PlayerStateMachine(this);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed+=OnLevelFailed;
    }

    public void UpdateCoinCountData(int _coinCount)
    {
        GameManager.Instance.JsonConverter.PlayerData.CoinCount = _coinCount;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }
    public int GetTotalCoinCount()
    {
        return GameManager.Instance.JsonConverter.PlayerData.CoinCount;
    }
    public void UpdateLevelData(int _levelNumber)
    {
        GameManager.Instance.JsonConverter.PlayerData.LevelNumber = _levelNumber;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }
    private void UpdateNextLevel()
    {
        GameManager.Instance.JsonConverter.PlayerData.LevelNumber = (GetLevelNumber() + 1);
        GameManager.Instance.JsonConverter.SavePlayerData();
    }

    public int GetLevelNumber()
    {
        return GameManager.Instance.JsonConverter.PlayerData.LevelNumber;
    }


    #region Events

    private void OnResetToMainMenu()
    {
        m_PlayerStateMachine.ChangeStateTo(PlayerStates.IdleState, true);
    }

    private void OnLevelCompleted()
    {
        m_PlayerStateMachine.ChangeStateTo(PlayerStates.WinState);
    }

    private void OnLevelFailed()
    {
        m_PlayerStateMachine.ChangeStateTo(PlayerStates.FailState);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }

    #endregion
}
