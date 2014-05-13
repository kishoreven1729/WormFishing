using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour
{
    #region Animation Events
    public void OnGameOver()
    {
        Application.LoadLevel(0);
    }

    public void OnCatchComplete()
    {
        GameDirector.instance.worm.SendMessage("CatchAnimationComplete", SendMessageOptions.DontRequireReceiver);
    }
    #endregion
}
