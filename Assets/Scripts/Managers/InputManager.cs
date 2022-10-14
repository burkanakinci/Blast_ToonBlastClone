using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : CustomBehaviour
{
    #region Attributes
    public bool CanClickable;
    #endregion
    #region ExternalAccess

    #endregion
    public override void Initialize()
    {
        CanClickable = false;

        GameManager.Instance.GridManager.OnCompleteSpawnedBlastableMove += CanClickableChangeTrue;
    }
    private void CanClickableChangeTrue()
    {
        CanClickable = true;
    }
        private void CanClickableChangeFalse()
    {
        CanClickable = false;
    }
}
