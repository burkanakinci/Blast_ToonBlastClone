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
    private Unblastable m_Unblastable;
    public Unblastable Unblastable
    {
        get
        {
            if (m_Unblastable == null)
            {
                m_Unblastable = base.GetComponent<Unblastable>();
            }
            return m_Unblastable;
        }
    }
    private SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (m_SpriteRenderer == null)
            {
                m_SpriteRenderer = base.GetComponent<SpriteRenderer>();
            }
            return m_SpriteRenderer;
        }
    }
    #endregion

    #region Methods
    public virtual void Initialize()
    {
        m_SpriteRenderer = base.GetComponent<SpriteRenderer>();
        m_Blastable = base.GetComponent<Blastable>();
        m_Unblastable = base.GetComponent<Unblastable>();
    }

    #endregion
}
