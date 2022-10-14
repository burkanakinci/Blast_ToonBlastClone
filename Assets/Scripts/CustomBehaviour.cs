using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    #region Components
    private Blastable m_Blastable;
    public Blastable Blastable
    {
        get
        {
            if (m_Blastable == null)
            {
                m_Blastable = base.GetComponent<Blastable>();
            }
            return m_Blastable;
        }
    }
    #endregion

    #region Methods
    public virtual void Initialize()
    {
        m_Blastable = base.GetComponent<Blastable>();
    }

    #endregion
}
