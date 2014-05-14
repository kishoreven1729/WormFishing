using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour
{
    #region Animation Events
    public void OnGameOver()
    {
        StartCoroutine(TouchpadOn());
    }

    public void OnCatchComplete()
    {
        GameDirector.instance.worm.SendMessage("CatchAnimationComplete", SendMessageOptions.DontRequireReceiver);
    }
    #endregion

    #region Coroutines
    public IEnumerator TouchpadOn()
    {
        yield return new WaitForSeconds(2.5f);

        TouchScreenKeyboard nameEntry = TouchScreenKeyboard.Open("Enter your name: ", TouchScreenKeyboardType.Default, false, false, false);

        string userName = nameEntry.text;

        Debug.Log("username");
    }
    #endregion
}
