using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPooledObject : CustomBehaviour, IPooledObject
{
    [SerializeField] private string m_PooledTag;

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
