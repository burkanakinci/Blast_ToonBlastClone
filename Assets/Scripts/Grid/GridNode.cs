using UnityEngine;

[System.Serializable]
public class GridNode
{
    [SerializeField] private Vector3 m_GlobalPosition;
    [SerializeField] private int m_XIndex;
    public int XIndex=>m_XIndex;
    [SerializeField] private int m_YIndex;
    public int YIndex=>m_YIndex;
    public Vector3 GlobalPosition => m_GlobalPosition;

    private NeighborGridNodeIndex[] m_NeighboringIndexes;
    public GridNode(Vector3 _globalPos, int _xIndis, int _yIndis)
    {
        m_GlobalPosition = _globalPos;
        m_XIndex = _xIndis;
        m_YIndex = _yIndis;

        m_NeighboringIndexes = new NeighborGridNodeIndex[4];
        m_NeighboringIndexes[(int)NeighboringState.OnDown] = new NeighborGridNodeIndex((m_XIndex - 1), (m_YIndex));
        m_NeighboringIndexes[(int)NeighboringState.OnLeft] = new NeighborGridNodeIndex((m_XIndex), (m_YIndex - 1));
        m_NeighboringIndexes[(int)NeighboringState.OnRight] = new NeighborGridNodeIndex((m_XIndex), (m_YIndex + 1));
        m_NeighboringIndexes[(int)NeighboringState.OnUp] = new NeighborGridNodeIndex((m_XIndex + 1), (m_YIndex));
    }

    public GridNode GetNeighborGridNode(NeighboringState _neighboringState)
    {
        if (!m_NeighboringIndexes[(int)_neighboringState].IsNeighbor)
        {
            return null;
        }
        return GameManager.Instance.GridManager.GetGridNodeByIndex(
            m_NeighboringIndexes[(int)_neighboringState].NeighborIndexX,
            m_NeighboringIndexes[(int)_neighboringState].NeighborIndexY
        );
    }
}
public class NeighborGridNodeIndex
{
    public int NeighborIndexX;
    public int NeighborIndexY;
    public bool IsNeighbor;

    public NeighborGridNodeIndex(int _indexX, int _indexY)
    {
        NeighborIndexX = _indexX;
        NeighborIndexY = _indexY;

        if ((NeighborIndexX >= 0) &&
        (NeighborIndexY >= 0) &&
        (NeighborIndexX < GameManager.Instance.LevelManager.ActiveGridRowCount) &&
        (NeighborIndexY < GameManager.Instance.LevelManager.ActiveGridColumnCount))
        {
            IsNeighbor = true;
        }
        else
        {
            IsNeighbor = false;
        }
    }
}