using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralState : IPlayerState
{
    private PlayerStateMachine m_PlayerStateMachine { get; set; }

    public GeneralState(PlayerStateMachine _playerStateMachine)
    {
        m_PlayerStateMachine = _playerStateMachine;
    }

    public void Enter()
    {
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