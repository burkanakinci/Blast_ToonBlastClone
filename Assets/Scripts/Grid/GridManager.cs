using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridManager : CustomBehaviour
{
    #region Datas
    [SerializeField] private BlastableMovementData m_BlastableMovementData;
    #endregion
    #region Attributes
    private GridNode[,] m_GridNodes;
    [SerializeField] private GameObject m_LockedGrid;
    #endregion
    #region Actions
    public event Action OnSpawnedBlastableMove;
    public event Action OnCompleteSpawnedBlastableMove;
    public event Action OnCompleteBlastableSettingSprite;
    #endregion
    public override void Initialize()
    {
        m_SpawnedBlastableMoveTweenID = GetInstanceID() + "m_SpawnedBlastableMoveTweenID";

        GameManager.Instance.OnResetToMainMenu+=SpawnGrid;
    }

    private Vector3 m_TempGridNodePos;
    private Blastable m_TempBlastable;
    private GridNode m_TempGridNode;
    private Vector3 m_TempSpawnPos;
    public void SpawnGrid()
    {
        m_GridNodes = new GridNode[GameManager.Instance.LevelManager.CurrentLevelData.GridRowCount, GameManager.Instance.LevelManager.CurrentLevelData.GridColumnCount];
        for (int _xCount = 0; _xCount < m_GridNodes.GetLength(0); _xCount++)
        {
            for (int _yCount = 0; _yCount < m_GridNodes.GetLength(1); _yCount++)
            {

                m_TempGridNodePos = GameManager.Instance.LevelManager.CurrentLevelData.GridStartPosition;
                m_TempGridNodePos.y += (GameManager.Instance.LevelManager.CurrentLevelData.PerGridWidht * _xCount);
                m_TempGridNodePos.x += (GameManager.Instance.LevelManager.CurrentLevelData.PerGridWidht * _yCount);

                m_TempGridNode = new GridNode(m_TempGridNodePos, _xCount, _yCount);
                m_GridNodes[_xCount, _yCount] = m_TempGridNode;

                SpawnBlastable((GameManager.Instance.LevelManager.CurrentLevelData.ColumnBlastables[_yCount].SpawnedBlastable[_xCount]),m_GridNodes[_xCount, _yCount]);
            }
        }
        StartBlastableMoveTween();
    }

    public void SpawnBlastable( BlastableType _blastableType,GridNode _node)
    {
        m_TempSpawnPos = _node.GlobalPosition;
        m_TempSpawnPos.y = (GameManager.Instance.LevelManager.CurrentLevelData.CameraSize + GameManager.Instance.LevelManager.CurrentLevelData.PerGridWidht);

        m_TempBlastable =
            GameManager.Instance.ObjectPool.SpawnFromPool(((_blastableType == BlastableType.Unblastable) ? (PooledObjectTags.Unblastable) : (PooledObjectTags.Blastable)),
            m_TempSpawnPos, Quaternion.identity, null).
                GetGameObject().
                Blastable;

        m_TempBlastable.SetBlastableData(GameManager.Instance.Entities.GetBlastableData(_blastableType));
        m_TempBlastable.SetCurrentGridNode(_node);
    }

    public GridNode GetGridNodeByIndex(int _row, int _column)
    {
        return m_GridNodes[_row, _column];
    }

    private string m_SpawnedBlastableMoveTweenID;
    private float m_SpawnedBlastableMoveLerpValue;
    [HideInInspector] public float SpawnedBlastableMovementLerpValue => m_SpawnedBlastableMoveLerpValue;
    public void StartBlastableMoveTween()
    {
        DOTween.Kill(m_SpawnedBlastableMoveTweenID);

        m_SpawnedBlastableMoveLerpValue = 0.0f;

        DOTween.To(() => m_SpawnedBlastableMoveLerpValue, x => m_SpawnedBlastableMoveLerpValue = x, 1.0f, m_BlastableMovementData.GridCellMovementDuration).
        OnUpdate(() =>
        {
            OnSpawnedBlastableMove?.Invoke();
        }).
        OnComplete(() =>
        {
            OnCompleteSpawnedBlastableMove?.Invoke();
        }).
        SetEase(m_BlastableMovementData.GridCellMovementCurve).
        SetId(m_SpawnedBlastableMoveTweenID);
    }

    public void SetBlastablesSprite()
    {
        OnCompleteBlastableSettingSprite?.Invoke();
    }
}
