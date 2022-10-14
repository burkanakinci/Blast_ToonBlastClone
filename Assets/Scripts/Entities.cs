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
    public int BlastableCount => m_BlastableOnScene.Count;
    #endregion

    public override void Initialize()
    {
        m_BlastableOnScene = new Dictionary<GridNode, Blastable>();
    }

    #region ListManagement
    public void ManageBlastableOnSceneList(ListOperation _operation, GridNode _gridNode, Blastable _blastable)
    {
        if ((_operation == ListOperation.Adding))
        {
            m_BlastableOnScene.Add(_gridNode, _blastable);
        }
        else if (m_BlastableOnScene.ContainsKey(_gridNode))
        {
            m_BlastableOnScene[_gridNode] = null;
        }
    }
    #endregion

    #region OnAction
    private void OnResetToMainMenu()
    {

    }

    private void OnLevelCompleted()
    {
    }

    private void OnLevelFailed()
    {
    }
    #endregion

    #region Getter_Setter
    public BlastableData GetBlastableData(BlastableType _blastableType)
    {
        return m_BlastableDatas[(int)_blastableType];
    }
    public Blastable GetBlastableByGridNode(GridNode _gridNode)
    {
        if (_gridNode == null)
        {
            return null;
        }
        return m_BlastableOnScene[_gridNode];
    }
    #endregion
}
