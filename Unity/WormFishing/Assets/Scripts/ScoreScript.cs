using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour
{
    #region Private Variables
    private TextMesh        _textMesh;
    #endregion

    #region Public Variables
    public string       scoreMessage;
    #endregion

    #region Constructor
    void Start () 
    {
        gameObject.renderer.sortingLayerName = "UI";

        _textMesh = GetComponent<TextMesh>();
	}
    #endregion

    #region Loop
    void Update () 
    {
        _textMesh.text = scoreMessage + GameDirector.instance.gameScore;
    }
    #endregion
}
