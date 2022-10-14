
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    #region Attributes
    [SerializeField] private LevelData m_LevelData;
    private int m_CurrentLevelNumber;
    private int m_ActiveLevelDataNumber;
    private int m_MaxLevelDataCount;
    #endregion
    #region ExternalAccess
    public int ActiveGridRowCount => m_LevelData.GridRowCount;
    public int ActiveGridColumnCount => m_LevelData.GridColumnCount;
    #endregion
    #region Actions
    public event Action OnLevelLoaded;
    #endregion
    public override void Initialize()
    {

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        m_MaxLevelDataCount = Resources.LoadAll("LevelDatas", typeof(LevelData)).Length;
    }

    private void GetLevelData()
    {
        m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ? (m_CurrentLevelNumber) : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        m_LevelData = Resources.Load<LevelData>("LevelDatas/" + m_ActiveLevelDataNumber + "_LevelData");
    }

    private void OnResetToMainMenu()
    {
        m_CurrentLevelNumber = GameManager.Instance.PlayerManager.GetLevelNumber();

        GetLevelData();
        GameManager.Instance.GridManager.SpawnGrid(ref m_LevelData);
    }

    private void OnLevelCompleted()
    {
    }

    private void OnLevelFailed()
    {
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }

}
