using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{

    #region Public Variables
    public string       scoreMessage;
    #endregion

    #region Constructor
    void Start () 
    {
        gameObject.renderer.sortingLayerName = "UI";
	}
    #endregion

    #region Loop
    void Update () 
    {

    }
    #endregion

    #region Public Variables

    #endregion
}
