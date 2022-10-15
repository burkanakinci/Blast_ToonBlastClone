using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : CustomBehaviour, IPooledObject
{
    #region Attributes
    [SerializeField] private BlastableMovementData m_BlastableMovementData;
    private BlastableData m_BlastableData;
    [SerializeField] private SpriteRenderer m_BlastableVisual;
    public GridNode CurrentGridNode;
    [SerializeField] private Blastable[] m_NeighborsBlastable;
    public List<Blastable> SameNeighborBlastable;
    #endregion
    #region ExternalAccess
    public BlastableType BlastableType => m_BlastableData.BlastableType;
    public int SameNeighborBlastableCount => SameNeighborBlastable.Count;
    #endregion
    #region Actions
    public event Action<float> OnClickedMovementAction;
    public event Action<Blastable> OnCompleteMovementAction;
    #endregion


    public override void Initialize()
    {
        base.Initialize();

        m_ShakeBlastableTweenID = GetInstanceID() + "m_ShakeBlastableTweenID";
        m_ClickedBlastableMoveTweenId = GetInstanceID() + "m_ClickedBlastableMoveTweenId";

        m_NeighborsBlastable = new Blastable[4];
        SameNeighborBlastable = new List<Blastable>();
    }

    public virtual void OnObjectSpawn()
    {
        SameNeighborBlastable.Clear();

    }
    public virtual void OnObjectDeactive()
    {
        RemoveOnActions();
        KillAllTween();

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.Blastable, this);

        this.gameObject.SetActive(false);
    }
    public virtual CustomBehaviour GetGameObject()
    {
        return this;
    }
    public void SetBlastableData(BlastableData _blastableData)
    {
        m_BlastableData = _blastableData;

        m_BlastableVisual.sprite = m_BlastableData.BlastableSprites[(int)BlastableSpriteType.Level1_Blastable];
    }
    public void SetCurrentGridNode(GridNode _node)
    {
        CurrentGridNode = _node;
        GameManager.Instance.Entities.ManageBlastableOnSceneList(ListOperation.Adding, CurrentGridNode, this);

        m_TempStartPosition = transform.position;

        SameNeighborBlastable.Clear();
        AddSameNeighborBlastableList(this);

        GameManager.Instance.GridManager.OnSpawnedBlastableMove += MovementOnGridCell;
        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += SetBlastableNeighbors;
        GameManager.Instance.GridManager.OnCompleteBlastableSettingSprite += SetBlastableSprite;
        GameManager.Instance.GridManager.OnCompleteBlastableSettingSprite += RemoveOnActions;

        if (!GameManager.Instance.Entities.IsEmptyGrid())
        {
            GameManager.Instance.GridManager.StartBlastableMoveTween();
        }
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

        if (m_NeighborsBlastable[(int)NeighboringState.OnRight] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnRight].BlastableType == this.BlastableType)
            {

                m_NeighborsBlastable[(int)NeighboringState.OnRight].AddSameNeighborBlastableList(SameNeighborBlastable);
                for (int _sameCount = SameNeighborBlastableCount - 1; _sameCount >= 0; _sameCount--)
                {
                    SameNeighborBlastable[_sameCount].AddSameNeighborBlastableList(m_NeighborsBlastable[(int)NeighboringState.OnRight].SameNeighborBlastable);
                }
            }
        }
        if (m_NeighborsBlastable[(int)NeighboringState.OnUp] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnUp].BlastableType == this.BlastableType)
            {

                m_NeighborsBlastable[(int)NeighboringState.OnUp].AddSameNeighborBlastableList(SameNeighborBlastable);
                for (int _sameCount = SameNeighborBlastableCount - 1; _sameCount >= 0; _sameCount--)
                {
                    SameNeighborBlastable[_sameCount].AddSameNeighborBlastableList(m_NeighborsBlastable[(int)NeighboringState.OnUp].SameNeighborBlastable);
                }
            }
        }
        if (!GameManager.Instance.Entities.IsEmptyGrid())
        {
            GameManager.Instance.GridManager.StartSetBlastablesSprite();
        }
    }
    public virtual void SetBlastableSprite()
    {
        if (SameNeighborBlastableCount >= GameManager.Instance.LevelManager.CurrentLevelData.IconChangedValues[(int)BlastableSpriteType.Level4_Blastable])
        {
            m_BlastableVisual.sprite = m_BlastableData.BlastableSprites[(int)BlastableSpriteType.Level4_Blastable];

        }
        else if (SameNeighborBlastableCount >= GameManager.Instance.LevelManager.CurrentLevelData.IconChangedValues[(int)BlastableSpriteType.Level3_Blastable])
        {
            m_BlastableVisual.sprite = m_BlastableData.BlastableSprites[(int)BlastableSpriteType.Level3_Blastable];
        }
        else if (SameNeighborBlastableCount >= GameManager.Instance.LevelManager.CurrentLevelData.IconChangedValues[(int)BlastableSpriteType.Level2_Blastable])
        {
            m_BlastableVisual.sprite = m_BlastableData.BlastableSprites[(int)BlastableSpriteType.Level2_Blastable];
        }
    }
    public void KillAllTween()
    {
        DOTween.Kill(m_ShakeBlastableTweenID);
        DOTween.Kill(m_ClickedBlastableMoveTweenId);
    }
    public void RemoveOnActions()
    {
        GameManager.Instance.GridManager.OnSpawnedBlastableMove -= MovementOnGridCell;
        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove -= SetBlastableNeighbors;
        GameManager.Instance.GridManager.OnCompleteBlastableSettingSprite -= SetBlastableSprite;
        GameManager.Instance.GridManager.OnCompleteBlastableSettingSprite -= RemoveOnActions;
    }

    public void AddSameNeighborBlastableList(Blastable _blastable)
    {
        SameNeighborBlastable.Add(_blastable);
    }
    public void AddSameNeighborBlastableList(List<Blastable> _blastables)
    {
        for (int _sameCount = _blastables.Count - 1; _sameCount >= 0; _sameCount--)
        {
            if (!SameNeighborBlastable.Contains(_blastables[_sameCount]))
            {
                SameNeighborBlastable.Add(_blastables[_sameCount]);
            }
        }
    }
    #region Clicked&Blast
    private void OnMouseDown()
    {
        if (!GameManager.Instance.InputManager.CanClickable)
        {
            return;
        }

        if ((m_BlastableData.BlastableType != BlastableType.Unblastable) &&
            (SameNeighborBlastableCount >= 2))
        {
            for (int _sameCount = SameNeighborBlastableCount - 1; _sameCount >= 0; _sameCount--)
            {

                if (SameNeighborBlastable[_sameCount] != this)
                {
                    SameNeighborBlastable[_sameCount].SetClickBlastableMoveValue(this);
                }
            }
            StartClickedTween();
        }
        else
        {
            ShakeBlastable();
        }

        GameManager.Instance.InputManager.MoveCounter++;
    }

    private string m_ClickedBlastableMoveTweenId;
    private void StartClickedTween()
    {
        DOTween.Kill(m_ClickedBlastableMoveTweenId);
        m_ClickedMovementLerpValue = 0.0f;

        GameManager.Instance.InputManager.CanClickable = false;

        DOTween.To(() => m_ClickedMovementLerpValue, x => m_ClickedMovementLerpValue = x, 1.0f, m_BlastableMovementData.ClickedMovementDuration).
        OnUpdate(() =>
        {
            OnClickedMovementAction?.Invoke(m_ClickedMovementLerpValue);
        }).
        OnComplete(() =>
        {
            OnCompleteMovementAction?.Invoke(this);
            BlastBlastable(this);
            BlastNeighborUnblastable();
            GameManager.Instance.Entities.StartFillEmptyGridNodes();
        }).
        SetEase(m_BlastableMovementData.ClickedMovementCurve).
        SetId(m_ClickedBlastableMoveTweenId);
    }
    private string m_ShakeBlastableTweenID;
    private void ShakeBlastable()
    {
        DOTween.Kill(m_ShakeBlastableTweenID);
        transform.position = CurrentGridNode.GlobalPosition;

        transform.DOShakePosition(m_BlastableMovementData.ShakeDuration, 0.060f, 10, 0.0f).
        SetEase(m_BlastableMovementData.ShakeCurve).
        SetId(m_ShakeBlastableTweenID);
    }
    private Vector3 m_ClickedBlastablePos;
    private float m_ClickedMovementLerpValue;
    public void SetClickBlastableMoveValue(Blastable _clickedBlastable)
    {
        m_ClickedBlastablePos = _clickedBlastable.CurrentGridNode.GlobalPosition;
        _clickedBlastable.OnClickedMovementAction += MoveClickedBlastable;
        _clickedBlastable.OnCompleteMovementAction += BlastBlastable;
    }
    private void MoveClickedBlastable(float _lerpValue)
    {
        transform.position = Vector3.Lerp(CurrentGridNode.GlobalPosition, m_ClickedBlastablePos, _lerpValue);
    }

    public virtual void BlastBlastable(Blastable _clickedBlastable)
    {
        _clickedBlastable.OnClickedMovementAction -= MoveClickedBlastable;
        _clickedBlastable.OnCompleteMovementAction -= BlastBlastable;

        GameManager.Instance.Entities.ManageBlastableOnSceneList(ListOperation.Subtraction, CurrentGridNode, this);

        OnObjectDeactive();
    }

    private void BlastNeighborUnblastable()
    {
        if (m_NeighborsBlastable[(int)NeighboringState.OnDown] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnDown].BlastableType == BlastableType.Unblastable)
            {
                m_NeighborsBlastable[(int)NeighboringState.OnDown].BlastBlastable(this);
            }
        }
        if (m_NeighborsBlastable[(int)NeighboringState.OnUp] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnUp].BlastableType == BlastableType.Unblastable)
            {
                m_NeighborsBlastable[(int)NeighboringState.OnUp].BlastBlastable(this);
            }
        }
        if (m_NeighborsBlastable[(int)NeighboringState.OnLeft] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnLeft].BlastableType == BlastableType.Unblastable)
            {
                m_NeighborsBlastable[(int)NeighboringState.OnLeft].BlastBlastable(this);
            }
        }
        if (m_NeighborsBlastable[(int)NeighboringState.OnRight] != null)
        {
            if (m_NeighborsBlastable[(int)NeighboringState.OnRight].BlastableType == BlastableType.Unblastable)
            {
                m_NeighborsBlastable[(int)NeighboringState.OnRight].BlastBlastable(this);
            }
        }
    }
    #endregion
}
