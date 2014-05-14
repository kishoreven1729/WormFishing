using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    #region Enum
    public enum ButtonType
    {
        Exit,
        Start,
        Leaderboard,
        Sound
    }
    #endregion

    #region Private Variables
    private SpriteRenderer  _spriteRenderer;
    private Sprite          _mouseOutSprite;
    #endregion

    #region Public Variables
    public ButtonType   buttonType;
    public Sprite       mouseOverSprite;
    #endregion

    #region Constructor
    void Start () 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _mouseOutSprite = _spriteRenderer.sprite;
	}
    #endregion

    #region Loop
    void Update () 
    {

    }

    void OnMouseDown()
    {
        switch(buttonType)
        {
            case ButtonType.Exit:
                { 
                    Application.Quit();
                    break;
                }
            case ButtonType.Leaderboard:
                { 
                    break;
                }
            case ButtonType.Start:
                {
                    Application.LoadLevel(0);
                    break;
                }
            case ButtonType.Sound:
                {
                    if(AudioListener.volume == 0.0f)
                    { 
                        AudioListener.volume = 0.0f;

                        _spriteRenderer.sprite = mouseOverSprite;
                    }
                    else
                    {   
                        AudioListener.volume = 1.0f;

                        _spriteRenderer.sprite = _mouseOutSprite;
                    }
                    break;
                }
        }
    }

    void OnMouseEnter()
    {
        _spriteRenderer.sprite = mouseOverSprite;
    }

    void OnMouseExit()
    {
        _spriteRenderer.sprite = _mouseOutSprite;
    }
    #endregion
}