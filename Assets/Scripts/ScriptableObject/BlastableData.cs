using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BlastableData", menuName = "Blastable Data")]
public class BlastableData : ScriptableObject
{
    #region Attributes
    [SerializeField] private BlastableType m_BlastableType;
    [SerializeField] private Sprite[] m_BlastableSprites;
    #endregion

    #region ExternalAccess
    [HideInInspector] public BlastableType BlastableType => m_BlastableType;
    [HideInInspector] public Sprite[] BlastableSprites => m_BlastableSprites;
    #endregion

}

