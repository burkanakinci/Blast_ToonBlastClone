using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    #region Components
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
    }

    #endregion
}
