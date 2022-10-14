using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    #region Attributes
    public float CameraSize = 5;
    public float PerGridWidht = 0.5f;
    public int GridRowCount = 8;
    public int GridColumnCount = 8;
    public Vector3 GridStartPosition;
    public ColumnBlastables[] ColumnBlastables;
    public int[] IconChangedValues = new int[4];
    #endregion
}

