using UnityEngine;

[System.Serializable]
public class GridNode
{
    [SerializeField] private Vector3 m_GlobalPosition;
    [SerializeField] private int m_XIndex;
    [SerializeField] private int m_YIndex;
    public Vector3 GlobalPosition => m_GlobalPosition;
    public int XIndex => m_XIndex;
    public int YIndex => m_YIndex;
    public GridNode(Vector3 _globalPos, int _xIndis, int _yIndis)
    {
        m_GlobalPosition = _globalPos;
        m_XIndex = _xIndis;
        m_YIndex = _yIndis;
    }


    public GridNode GetNeighborGridNode(NeighboringState _neighboringState)
    {
        switch (_neighboringState)
        {
            case (NeighboringState.OnDown):
                return GameManager.Instance.GridManager.GridNodes[(m_XIndex), (m_YIndex - 1)];

            case (NeighboringState.OnUp):
                return GameManager.Instance.GridManager.GridNodes[(m_XIndex), (m_YIndex + 1)];

            case (NeighboringState.OnLeft):
                return GameManager.Instance.GridManager.GridNodes[(m_XIndex - 1), (m_YIndex)];

            case (NeighboringState.OnRight):
                return GameManager.Instance.GridManager.GridNodes[(m_XIndex + 1), (m_YIndex)];

            default:
                return null;
        }



    }
}
