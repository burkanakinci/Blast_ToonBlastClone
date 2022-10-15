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
            if (m_BlastableOnScene.ContainsKey(_gridNode))
            {
                m_BlastableOnScene[_gridNode] = _blastable;
            }
            else
            {
                m_BlastableOnScene.Add(_gridNode, _blastable);
            }
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

    private Coroutine m_FilledEmptyGridCoroutine;
    public void StartFillEmptyGridNodes()
    {
        if (m_FilledEmptyGridCoroutine != null)
        {
            StopCoroutine(m_FilledEmptyGridCoroutine);
        }
        m_FilledEmptyGridCoroutine = StartCoroutine(FillEmptyGridNodes());
    }
    private IEnumerator FillEmptyGridNodes()
    {
        yield return new WaitForEndOfFrame();

        if (IsEmptyGrid())
        {
            for (int _gridNodeCount = m_BlastableOnScene.Count - 1; _gridNodeCount >= 0; _gridNodeCount--)
            {
                if (m_BlastableOnScene.Values.ElementAt(_gridNodeCount) == null)
                {
                    m_BlastableOnScene.Keys.ElementAt(_gridNodeCount).FillGridNode();
                }
                else
                {
                    m_BlastableOnScene.Values.ElementAt(_gridNodeCount).SetCurrentGridNode(m_BlastableOnScene.Keys.ElementAt(_gridNodeCount));
                }
            }
        }
    }

    public bool IsEmptyGrid()
    {
       return m_BlastableOnScene.ContainsValue(null);
    }
}

