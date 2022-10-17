using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Attributes
    public PlayerManager PlayerManager;
    public JsonConverter JsonConverter;
    public GridManager GridManager;
    public Entities Entities;
    public ObjectPool ObjectPool;
    public LevelManager LevelManager;
    public InputManager InputManager;
    public UIManager UIManager;
    public CameraManager CameraManager;
    #endregion

    #region Actions
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    public event Action OnGameStart;
    #endregion
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ObjectPool.Initialize();
        JsonConverter.Initialize();
        PlayerManager.Initialize();

        LevelManager.Initialize();
        CameraManager.Initialize();
        InputManager.Initialize();
        UIManager.Initialize();
        Entities.Initialize();
        GridManager.Initialize();
    }

    private void Start()
    {
        ResetToMainMenu();
    }
    public void ResetToMainMenu()
    {
        OnResetToMainMenu?.Invoke();
    }
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
}
