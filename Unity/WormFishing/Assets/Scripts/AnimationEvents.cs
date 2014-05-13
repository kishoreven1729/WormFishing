using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour
{
    #region Animation Events
    public void OnGameOver()
    {
        Application.LoadLevel(0);
    }
    #endregion
}
