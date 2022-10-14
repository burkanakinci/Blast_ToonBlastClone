using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;


public class LevelDataCreator : MonoBehaviour
{
    public LevelData LevelData;
    public ColumnBlastables[] ColumnBlastables;
    [HideInInspector] public int CreatedGridRowCount, CreatedGridColumnCount;
    [HideInInspector] public float CreatedGridWidht = 0.5f;
    [HideInInspector] public int CreatedLevelNumber;
    [HideInInspector] public int[] CreatedIconChangeValue = new int[3];

    [HideInInspector] public GameObject GridVisualPrefab;
    private Vector3 m_TempCreatedGridPos;
    [HideInInspector] public Transform CreatedStartTransform;
    [HideInInspector] public List<GameObject> GridVisuals;
    public void SpawnGridCell()
    {
        GridVisuals.ForEach(x => DestroyImmediate(x));
        GridVisuals.Clear();

        for (int _yCount = 0; _yCount < CreatedGridRowCount; _yCount++)
        {
            for (int _xCount = 0; _xCount < CreatedGridColumnCount; _xCount++)
            {
                m_TempCreatedGridPos = CreatedStartTransform.position;
                m_TempCreatedGridPos.x += (CreatedGridWidht * _xCount);
                m_TempCreatedGridPos.y += (CreatedGridWidht * _yCount);

                GridVisuals.Add(Instantiate(GridVisualPrefab, m_TempCreatedGridPos, Quaternion.identity, null));
            }
        }

        ColumnBlastables = new ColumnBlastables[CreatedGridColumnCount];
    }
    private string m_SavePath;
    public void CreateLevelData()
    {
        LevelData = ScriptableObject.CreateInstance<LevelData>();

        m_SavePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelDatas/" + CreatedLevelNumber + "_LevelData.asset");

        LevelData.CameraSize = Camera.main.orthographicSize;
        LevelData.PerGridWidht = CreatedGridWidht;
        LevelData.GridRowCount = CreatedGridRowCount;
        LevelData.GridColumnCount = CreatedGridColumnCount;
        LevelData.GridStartPosition = CreatedStartTransform.position;
        LevelData.ColumnBlastables = new ColumnBlastables[CreatedGridColumnCount];
        LevelData.ColumnBlastables = ColumnBlastables;
        LevelData.IconChangedValues = CreatedIconChangeValue;

        AssetDatabase.CreateAsset(LevelData, m_SavePath);
        AssetDatabase.SaveAssets();

    }
}

[Serializable]
public class ColumnBlastables
{
    public int ColumnIndex;
    public BlastableType[] SpawnedBlastable;
}

