using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LevelDataCreator))]
public class LevelCreatorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelDataCreator levelDataCreator = (LevelDataCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("Grid Visual Prefab");
        levelDataCreator.GridVisualPrefab =
            (GameObject)EditorGUILayout.ObjectField(levelDataCreator.GridVisualPrefab, typeof(GameObject), true);
        GUILayout.Label("Grid Start Transform");
        levelDataCreator.CreatedStartTransform =
            (Transform)EditorGUILayout.ObjectField(levelDataCreator.CreatedStartTransform, typeof(Transform), true);
        levelDataCreator.CreatedStartTransform.position =
            EditorGUILayout.Vector3Field("Grid Start Position ", levelDataCreator.CreatedStartTransform.position);
        GUILayout.Label("Created Camera Size");
        Camera.main.orthographicSize =
            EditorGUILayout.FloatField("Camera Size", Camera.main.orthographicSize);

        GUILayout.Space(20f);

        GUILayout.Label("Grid Cell Width");
        levelDataCreator.CreatedGridWidht =
            EditorGUILayout.FloatField(levelDataCreator.CreatedGridWidht);
        GUILayout.Label("Grid Row Count");
        levelDataCreator.CreatedGridRowCount =
            EditorGUILayout.IntSlider(levelDataCreator.CreatedGridRowCount, 2, 8);
        GUILayout.Label("Grid Column Count");
        levelDataCreator.CreatedGridColumnCount =
            EditorGUILayout.IntSlider(levelDataCreator.CreatedGridColumnCount, 2, 8);

        if (EditorGUI.EndChangeCheck())
        {
            levelDataCreator.SpawnGridCell();
        }

        GUILayout.Space(20f);
        GUILayout.Label("1. Icon Opened Value");
        levelDataCreator.CreatedIconChangeValue[0] =
            EditorGUILayout.IntSlider(levelDataCreator.CreatedIconChangeValue[0], 3, 64);
        GUILayout.Label("2. Icon Opened Value");
        levelDataCreator.CreatedIconChangeValue[1] =
            EditorGUILayout.IntSlider(levelDataCreator.CreatedIconChangeValue[1], 3, 64);
        GUILayout.Label("3. Icon Opened Value");
        levelDataCreator.CreatedIconChangeValue[2] =
            EditorGUILayout.IntSlider(levelDataCreator.CreatedIconChangeValue[2], 3, 64);

        GUILayout.Space(25f);
        GUILayout.Label("Number Of Level To Be Created");
        levelDataCreator.CreatedLevelNumber =
            EditorGUILayout.IntField("Number Of Level", levelDataCreator.CreatedLevelNumber);

        GUILayout.Space(25f);

        if (GUILayout.Button("CreateLevelData"))
        {
            levelDataCreator.CreateLevelData();
        }


    }
}
#endif