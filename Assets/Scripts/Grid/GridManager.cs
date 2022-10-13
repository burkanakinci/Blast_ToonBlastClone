using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : CustomBehaviour
{
    #region Attributes
    private GridNode[,] m_GridNodes;
    [SerializeField] private GameObject m_LockedGrid;
    [SerializeField] private Transform m_GridStartTransform;

    #endregion
    #region Actions
    #endregion
    public override void Initialize()
    {

    }

    private Vector3 m_TempGridNodePos;
    private Blastable m_TempBlastable;
    private GridNode m_TempGridNode;
    public void SpawnGrid()
    {
        for (int _yCount = 0; _yCount < GameManager.Instance.LevelManager.ActiveGridColumnCount; _yCount++)
        {
            for (int _xCount = 0; _xCount < GameManager.Instance.LevelManager.ActiveGridRowCount; _xCount++)
            {
                m_TempGridNodePos = m_GridStartTransform.position;
                m_TempGridNodePos.y += (GameManager.Instance.LevelManager.ActivePerGridWidht * _xCount);
                m_TempGridNodePos.x += (GameManager.Instance.LevelManager.ActivePerGridWidht * _yCount);

                m_TempGridNode = new GridNode(m_TempGridNodePos, _xCount, _yCount);
                m_GridNodes[_xCount, _yCount] = m_TempGridNode;

                m_TempBlastable =
                    GameManager.Instance.ObjectPool.SpawnFromPool(PooledObjectTags.Blastable, m_TempGridNodePos, Quaternion.identity, null)
                    .GetGameObject()
                    .Blastable;

                m_TempBlastable.SetCurrentGridNode(m_TempGridNode);
            }
        }
    }

    #region Getter&Setter
    public GridNode GetGridNode(int _xIndex, int _yIndex)
    {
        return m_GridNodes[_xIndex, _yIndex];
    }
    public void SetGridArray()
    {
        m_GridNodes = new GridNode[GameManager.Instance.LevelManager.ActiveGridRowCount, GameManager.Instance.LevelManager.ActiveGridColumnCount];
    }

    #endregion
}
