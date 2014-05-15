﻿using UnityEngine;
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
#if UNITY_WEBPLAYER
        Backend.PostHighScore("TempPlayer", GameDirector.instance.gameScore);
        yield return new WaitForSeconds(0.0f);
#else
        TouchScreenKeyboard nameEntry = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Your Score: " + GameDirector.instance.gameScore + "\n Enter your name: ");

        while(nameEntry.done)
        {
            if (nameEntry.wasCanceled)
            {
                break;
            }
            else
            { 
                yield return null;
            }
        }
        
        if(!nameEntry.wasCanceled)
        { 
            string userName = nameEntry.text;

            if(userName.Length == 0)
            {
                userName = "Temp";
            }

            Backend.PostHighScore(userName, GameDirector.instance.gameScore);

            GameDirector.instance.UpdateLeaderboard();
        }        

        yield return new WaitForSeconds(1.0f);
#endif
    }
    #endregion
}