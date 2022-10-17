using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unblastable : Blastable
{
    #region Attributes
    #endregion
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject-=OnObjectDeactive;
        KillAllTween();
        RemoveOnActions();

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.Blastable, this);

        this.gameObject.SetActive(false);
    }
    public override CustomBehaviour GetGameObject()
    {
        return this;
    }
    public override void MovementOnGridCell(float _lerpvalue, bool _useLerp)
    {
        if (_useLerp)
        {
            base.MovementOnGridCell(_lerpvalue, _useLerp);
        }
        else
        {
            transform.position = CurrentGridNode.GlobalPosition;
        }

    }
    public override void SetBlastableNeighbors()
    {
    }
    public override void BlastBlastable(Blastable _clickedBlastable)
    {
        GameManager.Instance.ObjectPool.SpawnFromPool(PooledObjectTags.BlastParticle,transform.position,Quaternion.identity,null);
        GameManager.Instance.Entities.ManageBlastableOnSceneList(ListOperation.Subtraction, CurrentGridNode, this);
        GameManager.Instance.LevelManager.DecreaseTargetCount();
        OnObjectDeactive();
    }
}
