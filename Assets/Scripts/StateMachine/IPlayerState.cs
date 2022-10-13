using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Enter();
    void Execute();
    void Exit();
    void OnEvent();
}
