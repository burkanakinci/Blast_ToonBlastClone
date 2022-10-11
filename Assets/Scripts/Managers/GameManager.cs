using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Components
    public GridManager GridManager;
    public Entities Entities;
    public ObjectPool ObjectPool;
    #endregion

    #region Actions

    #endregion
    private void Awake()
    {
        Instance = this;

        base.Initialize();
        ObjectPool.Initialize();
        Entities.Initialize();
        GridManager.Initialize();

    }
}
