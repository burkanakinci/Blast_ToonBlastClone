using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unblastable : CustomBehaviour, IPooledObject
{
    [SerializeField] private string m_PooledTag;

    public override void Initialize()
    {
        base.Initialize();
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
}
