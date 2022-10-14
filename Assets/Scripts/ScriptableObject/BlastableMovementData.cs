using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BlastableMovementData", menuName = "Blastable Movement Data")]
public class BlastableMovementData : ScriptableObject
{
    #region Attributes
    [SerializeField] private float m_GridCellMovementDuration = 0.65f;
    [SerializeField] private AnimationCurve m_GridCellMovementCurve;

    [SerializeField] private float m_ShakeDuration = 0.65f;
    [SerializeField] private AnimationCurve m_ShakeCurve;

    [SerializeField] private float m_ClickedMovementDuration = 0.5f;
    [SerializeField] private AnimationCurve m_ClickedMovementCurve;
    #endregion

    #region ExternalAccess
    [HideInInspector] public float GridCellMovementDuration => m_GridCellMovementDuration;
    [HideInInspector] public AnimationCurve GridCellMovementCurve => m_GridCellMovementCurve;
    [HideInInspector] public float ShakeDuration => m_ShakeDuration;
    [HideInInspector] public AnimationCurve ShakeCurve => m_ShakeCurve;
    [HideInInspector] public float ClickedMovementDuration => m_ClickedMovementDuration;
    [HideInInspector] public AnimationCurve ClickedMovementCurve => m_ClickedMovementCurve;
    #endregion

}

