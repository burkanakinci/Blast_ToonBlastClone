using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : CustomBehaviour, IPooledObject
{
    #region Attributes
    public BlastableData BlastableData;
    private GridNode m_CurrentGridNode;
    #endregion

    [SerializeField] private Blastable[] m_NeighborBlastable;
    public int SameNeighborCounter;

    public override void Initialize()
    {
        base.Initialize();
        m_NeighborBlastable = new Blastable[4];
    }

    public void OnObjectSpawn()
    {

    }
    public void OnObjectDeactive()
    {

    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

    public void SetCurrentGridNode(GridNode _node)
    {
        m_CurrentGridNode = _node;

        GameManager.Instance.Entities.ManageBlastableList(ListOperation.Adding, m_CurrentGridNode, this);
    }

    public void SetNeighborsByBlastable()
    {
        if (m_CurrentGridNode.XIndex > 0)
        {
            SetNeighbor(NeighboringState.OnLeft,
            (GameManager.Instance.Entities.GetBlastableByGridNode(m_CurrentGridNode.GetNeighborGridNode(NeighboringState.OnLeft))));
        }
        if (m_CurrentGridNode.XIndex < ((GameManager.Instance.LevelManager.ActiveGridColumnCount) - 1))
        {
            SetNeighbor(NeighboringState.OnRight,
            (GameManager.Instance.Entities.GetBlastableByGridNode(m_CurrentGridNode.GetNeighborGridNode(NeighboringState.OnRight))));
        }
        if (m_CurrentGridNode.YIndex > 0)
        {
            SetNeighbor(NeighboringState.OnDown,
            (GameManager.Instance.Entities.GetBlastableByGridNode(m_CurrentGridNode.GetNeighborGridNode(NeighboringState.OnDown))));
        }
        if (m_CurrentGridNode.YIndex < ((GameManager.Instance.LevelManager.ActiveGridRowCount) - 1))
        {
            SetNeighbor(NeighboringState.OnUp,
            (GameManager.Instance.Entities.GetBlastableByGridNode(m_CurrentGridNode.GetNeighborGridNode(NeighboringState.OnUp))));
        }
    }
    private void SetNeighbor(NeighboringState _neighboringSide, Blastable _blastable)
    {
        m_NeighborBlastable[(int)_neighboringSide] = _blastable;
    }
}
