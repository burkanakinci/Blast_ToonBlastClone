using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    #region Attributes
    [SerializeField] private LevelData m_LevelData;
    #endregion
    #region ExternalAccess
    public float ActivePerGridWidht => m_LevelData.PerGridWidht;
    public int ActiveGridRowCount => m_LevelData.GridRowCount;
    public int ActiveGridColumnCount => m_LevelData.GridColumnCount;
    #endregion
    #region Actions

    #endregion
    public override void Initialize()
    {
    }
}
