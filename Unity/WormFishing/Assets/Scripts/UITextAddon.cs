using UnityEngine;
using System.Collections;

public class UITextAddon : MonoBehaviour
{
    #region Private Variables
    private TextMesh _textMesh;
    #endregion

    #region Public Variables
    public bool fetchScore;
    #endregion

    #region Constructor
    void Start () 
    {
        gameObject.renderer.sortingLayerName = "UI";
        gameObject.renderer.sortingOrder = 9;

        _textMesh = GetComponent<TextMesh>();
	}
    #endregion

    #region Loop
    void Update () 
    {
        if(fetchScore)
        {
            _textMesh.text = "Your Score: " + GameDirector.instance.gameScore;
        }
    }
    #endregion
}
