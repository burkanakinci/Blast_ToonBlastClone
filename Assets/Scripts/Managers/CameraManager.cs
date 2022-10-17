using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : CustomBehaviour
{
    private Camera m_MainCamera;
    public override void Initialize()
    {
        m_MainCamera = Camera.main;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    private void OnResetToMainMenu()
    {
        m_MainCamera.orthographicSize = GameManager.Instance.LevelManager.CurrentLevelData.CameraSize;
    }
}
