using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : CustomBehaviour
{
    #region Datas
    [SerializeField] private BlastableData[] m_BlastableDatas;
    #endregion
    #region Attributes
    private Dictionary<GridNode, Blastable> m_BlastableOnScene;
    public int BlastableCount => m_BlastableOnScene.Values.Count;
    #endregion

    public override void Initialize()
    {
        m_BlastableOnScene = new Dictionary<GridNode, Blastable>();
    }

    #region ListManagement
    public void ManageBlastableList(ListOperation _operation, GridNode _gridNode, Blastable _blastable)
    {
        if ((_operation == ListOperation.Adding))
        {
            m_BlastableOnScene.Add(_gridNode, _blastable);
        }
        else if (m_BlastableOnScene.ContainsKey(_gridNode))
        {
            m_BlastableOnScene.Remove(_gridNode);
        }
    }
    #endregion
    public Blastable GetBlastableByGridNode(GridNode _gridNode)
    {
        return m_BlastableOnScene[_gridNode];
    }
    public Blastable GetBlastableByIndex(int _index)
    {
        return m_BlastableOnScene.Values.ElementAt(_index);
    }

    public void SetNeighborAllBlastable()
    {
        for (int _blastableCount = GameManager.Instance.Entities.BlastableCount - 1; _blastableCount >= 0; _blastableCount--)
        {
            GetBlastableByIndex(_blastableCount).SetNeighborsByBlastable();
        }
    }
}
