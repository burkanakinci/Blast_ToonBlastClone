using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : CustomBehaviour, IPooledObject
{
    #region Attributes
    private BlastableData m_BlastableData;
    public BlastableType BlastableType => m_BlastableData.BlastableType;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [HideInInspector] public GridNode CurrentGridNode;
    #endregion

    [SerializeField] private Blastable[] m_NeighborsBlastable;
    public int SameNeighborCounter;

    public override void Initialize()
    {
        base.Initialize();

        m_NeighborsBlastable = new Blastable[4];
    }

    public virtual void OnObjectSpawn()
    {
        SameNeighborCounter = 0;
        KillAllTween();
    }
    public virtual void OnObjectDeactive()
    {

    }
    public virtual CustomBehaviour GetGameObject()
    {
        return this;
    }

    public void SetBlastableData(BlastableData _blastableData)
    {
        m_BlastableData = _blastableData;

        m_SpriteRenderer.sprite = m_BlastableData.BlastableSprites[0];
    }
    public void SetCurrentGridNode(GridNode _node)
    {
        CurrentGridNode = _node;
        GameManager.Instance.Entities.ManageBlastableOnSceneList(ListOperation.Adding, CurrentGridNode, this);

        m_TempStartPosition = transform.position;

        GameManager.Instance.GridManager.OnSpawnedBlastableMove += MovementOnGridCell;
        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += SetBlastableNeighbors;
        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += SetBlastableSprite;
    }

    private Vector3 m_TempStartPosition;
    public virtual void MovementOnGridCell()
    {
        transform.position = Vector3.Lerp(m_TempStartPosition, CurrentGridNode.GlobalPosition, GameManager.Instance.GridManager.SpawnedBlastableMovementLerpValue);
    }
    public virtual void SetBlastableNeighbors()
    {
        m_NeighborsBlastable[(int)NeighboringState.OnDown] =
            GameManager.Instance.Entities.GetBlastableByGridNode(CurrentGridNode.GetNeighborGridNode(NeighboringState.OnDown));
        m_NeighborsBlastable[(int)NeighboringState.OnLeft] =
            GameManager.Instance.Entities.GetBlastableByGridNode(CurrentGridNode.GetNeighborGridNode(NeighboringState.OnLeft));
        m_NeighborsBlastable[(int)NeighboringState.OnRight] =
            GameManager.Instance.Entities.GetBlastableByGridNode(CurrentGridNode.GetNeighborGridNode(NeighboringState.OnRight));
        m_NeighborsBlastable[(int)NeighboringState.OnUp] =
            GameManager.Instance.Entities.GetBlastableByGridNode(CurrentGridNode.GetNeighborGridNode(NeighboringState.OnUp));

        if (m_NeighborsBlastable[(int)NeighboringState.OnRight].BlastableType == this.BlastableType)
        {
            m_NeighborsBlastable[(int)NeighboringState.OnRight].SameNeighborCounter++;
            SameNeighborCounter++;
        }
        if (m_NeighborsBlastable[(int)NeighboringState.OnUp].BlastableType == this.BlastableType)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnRight].BlastableType == this.BlastableType)
            {
                m_NeighborsBlastable[(int)NeighboringState.OnRight].SameNeighborCounter++;
            }
            m_NeighborsBlastable[(int)NeighboringState.OnUp].SameNeighborCounter++;
            SameNeighborCounter++;
        }
    }
    public virtual void SetBlastableSprite()
    {

    }
    private void KillAllTween()
    {
    }

    private void OnMouseDown()
    {
        //Debug.Log(name);
    }
}
