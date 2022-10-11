using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastable : CustomBehaviour, IPooledObject
{
    public GridNode CurrentGridNode;
    [SerializeField] private Blastable[] m_NeighborBlastable;
    

    public override void Initialize()
    {
        m_NeighborBlastable = new Blastable[4];
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

    public void SetNeighborBlastable(NeighboringState _neighboringSide, Blastable _blastable)
    {
        m_NeighborBlastable[(int)_neighboringSide] = _blastable;
    }
}
