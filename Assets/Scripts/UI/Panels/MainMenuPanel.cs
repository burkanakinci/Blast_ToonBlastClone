using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : UIPanel
{
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
    }

    public void StartGameButton()
    {
        GameManager.Instance.PlayerManager.PlayerStateMachine.ChangeStateTo(PlayerStates.GameState);
    }
}
