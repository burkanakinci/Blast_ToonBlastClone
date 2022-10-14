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

    }
    public override CustomBehaviour GetGameObject()
    {
        return this;
    }
}
