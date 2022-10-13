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
    #endregion

    #region Actions

    #endregion
    private void Awake()
    {
        Instance = this;

        ObjectPool.Initialize();

        JsonConverter.Initialize();
        PlayerManager.Initialize();

        Entities.Initialize();

        GridManager.Initialize();

        LevelManager.Initialize();

    }
}
