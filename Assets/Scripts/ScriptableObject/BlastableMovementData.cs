using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BlastableMovementData", menuName = "Blastable Movement Data")]
public class BlastableMovementData : ScriptableObject
{
    #region Attributes
    [SerializeField] private float m_GridCellMovementDuration=0.65f;
    [SerializeField] private AnimationCurve m_GridCellMovementCurve;
    #endregion

    #region ExternalAccess
    [HideInInspector] public float GridCellMovementDuration => m_GridCellMovementDuration;
    [HideInInspector] public AnimationCurve GridCellMovementCurve => m_GridCellMovementCurve;
    #endregion

}

