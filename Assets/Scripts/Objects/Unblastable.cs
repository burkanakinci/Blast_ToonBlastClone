using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unblastable : Blastable
{
    #region Attributes
    #endregion
    public override void MovementOnGridCell()
    {
        transform.position = CurrentGridNode.GlobalPosition;
    }
    public override void SetBlastableNeighbors()
    {
    }
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnObjectSpawn()
    {

    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        RemoveOnActions();

        GameManager.Instance.ObjectPool.AddObjectPool(PooledObjectTags.Blastable, this);

        this.gameObject.SetActive(false);
    }
    public override CustomBehaviour GetGameObject()
    {
        return this;
    }

    public override void BlastBlastable(Blastable _clickedBlastable)
    {
        GameManager.Instance.Entities.ManageBlastableOnSceneList(ListOperation.Subtraction, CurrentGridNode, this);

        OnObjectDeactive();
    }
}
