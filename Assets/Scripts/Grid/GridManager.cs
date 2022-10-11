using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : CustomBehaviour
{
    #region Attributes
    public GridNode[,] GridNodes;
    [SerializeField] private GameObject m_LockedGrid;
    [SerializeField] private Transform m_GridStartTransform;
    [SerializeField] private float m_PerGridWidht = 0.5f;
    public float PerGridWidht => m_PerGridWidht;
    [SerializeField] private int m_GridXCount = 8;
    [SerializeField] private int m_GridYCount = 8;

    #endregion
    #region Actions
    #endregion
    public override void Initialize()
    {
        GridNodes = new GridNode[m_GridXCount, m_GridYCount];

        SpawnGrid();
        SetNeighborAllBlastable();
    }

    private Vector3 m_TempGridNodePos;
    private Blastable m_TempBlastable;
    private GridNode m_TempGridNode;
    public void SpawnGrid()
    {
        for (int _yCount = 0; _yCount < m_GridYCount; _yCount++)
        {
            for (int _xCount = 0; _xCount < m_GridXCount; _xCount++)
            {
                m_TempGridNodePos = m_GridStartTransform.position;
                m_TempGridNodePos.x += (m_PerGridWidht * _xCount);
                m_TempGridNodePos.y += (m_PerGridWidht * _yCount);

                m_TempGridNode = new GridNode(m_TempGridNodePos, _xCount, _yCount);
                GridNodes[_xCount, _yCount] = m_TempGridNode;

                m_TempBlastable =
                    GameManager.Instance.ObjectPool.SpawnFromPool(PooledObjectTags.Blastable, m_TempGridNodePos, Quaternion.identity, null)
                    .GetGameObject().
                    GetComponent<Blastable>();

                m_TempBlastable.CurrentGridNode = m_TempGridNode;

                GameManager.Instance.Entities.ManageBlastableList(ListOperation.Adding, m_TempBlastable.CurrentGridNode, m_TempBlastable);
            }
        }
    }

    public void SetNeighborAllBlastable()
    {
        for (int _blastableCount = GameManager.Instance.Entities.BlastableCount - 1; _blastableCount >= 0; _blastableCount--)
        {

            m_TempBlastable =
                GameManager.Instance.Entities.GetBlastableByIndex(_blastableCount);

            if (m_TempBlastable.CurrentGridNode.XIndex > 0)
            {
                m_TempBlastable.SetNeighborBlastable(NeighboringState.OnLeft,
                (GameManager.Instance.Entities.GetBlastableByGridNode(m_TempBlastable.CurrentGridNode.GetNeighborGridNode(NeighboringState.OnLeft))));
            }
            if (m_TempBlastable.CurrentGridNode.XIndex < (m_GridXCount - 1))
            {
                m_TempBlastable.SetNeighborBlastable(NeighboringState.OnRight,
                (GameManager.Instance.Entities.GetBlastableByGridNode(m_TempBlastable.CurrentGridNode.GetNeighborGridNode(NeighboringState.OnRight))));
            }
            if (m_TempBlastable.CurrentGridNode.YIndex > 0)
            {
                m_TempBlastable.SetNeighborBlastable(NeighboringState.OnDown,
                (GameManager.Instance.Entities.GetBlastableByGridNode(m_TempBlastable.CurrentGridNode.GetNeighborGridNode(NeighboringState.OnDown))));
            }
            if (m_TempBlastable.CurrentGridNode.YIndex < (m_GridYCount - 1))
            {
                m_TempBlastable.SetNeighborBlastable(NeighboringState.OnUp,
                (GameManager.Instance.Entities.GetBlastableByGridNode(m_TempBlastable.CurrentGridNode.GetNeighborGridNode(NeighboringState.OnUp))));
            }

        }
    }
}
