using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private PlayerManager m_PlayerManager;
    private IPlayerState m_CurrentState;
    private IPlayerState m_GeneralState;
    private IPlayerState m_PrevState;

    private List<IPlayerState> mStates = new List<IPlayerState>();

    public PlayerStateMachine(PlayerManager _playerManager)
    {
        m_PlayerManager = _playerManager;
        Initialize();
    }

    public void Initialize()
    {
        mStates.Add(new IdleState(this));
        mStates.Add(new WinState(this));
        mStates.Add(new FailState(this));
        mStates.Add(new GameState(this));

        mStates.Add(new GeneralState(this));

        m_GeneralState = mStates[mStates.Count - 1];
        m_CurrentState = m_GeneralState;
    }

    public void ChangeStateTo(PlayerStates _state, bool _force = false)
    {
        if (m_CurrentState != mStates[(int)_state] || _force)
        {
            m_PrevState = m_CurrentState;

            m_CurrentState.Exit();
            m_CurrentState = mStates[(int)_state];
            m_CurrentState.Enter();
        }
    }

    public void Enter()
    {
        m_CurrentState.Enter();
        m_GeneralState.Enter();
    }

    public void Execute()
    {
        m_CurrentState.Execute();
        m_GeneralState.Execute();
    }

    public void Exit()
    {
        m_CurrentState.Exit();
        m_GeneralState.Exit();
    }

    public bool EqualsCurrentState(PlayerStates _state)
    {
        return (m_CurrentState == mStates[(int)_state]);
    }

    public IPlayerState GetState(PlayerStates _state)
    {
        return mStates[(int)_state];
    }
}
