using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledUIObjects : CustomBehaviour,IPooledObject
{
    [SerializeField] private string m_PooledTag;
    [SerializeField] private float m_LifeTime;

    public override void Initialize()
    {
    }
    public void OnObjectSpawn()
    {
    }
    public void OnObjectDeactive()
    {
        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }

}
