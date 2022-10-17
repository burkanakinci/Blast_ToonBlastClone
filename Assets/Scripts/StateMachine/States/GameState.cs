using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : IPlayerState
{
    private PlayerStateMachine m_PlayerStateMachine { get; set; }

    public GameState(PlayerStateMachine _playerStateMachine)
    {
        m_PlayerStateMachine = _playerStateMachine;
    }

    public void Enter()
    {
        GameManager.Instance.StartGame();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

    public void OnEvent()
    {

    }
}
