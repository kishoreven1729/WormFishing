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
        Sound,
        LeaderboardMenu,
        LeaderboardGameOver,
    }
    #endregion

    #region Private Variables
    private SpriteRenderer  _spriteRenderer;
    private Sprite          _mouseOutSprite;
    #endregion

    #region Public Variables
    public ButtonType   buttonType;
    public Sprite       mouseOverSprite;

    public Transform    toggleTransform;
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
                    Toggle();

                    break;
                }
            case ButtonType.Start:
                {
                    Application.LoadLevel(1);
                    break;
                }
            case ButtonType.Sound:
                {
                    if(AudioListener.volume != 0.0f)
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
            case ButtonType.LeaderboardMenu:
                {
                    Toggle();

                    break;
                }
            case ButtonType.LeaderboardGameOver:
                {
                    Application.LoadLevel(0);

                    break;
                }
        }
    }

    void Toggle()
    {
        if (toggleTransform.gameObject.activeSelf == false)
        {
            toggleTransform.gameObject.SetActive(true);
        }
        else
        {
            toggleTransform.gameObject.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if(buttonType != ButtonType.Sound)
        { 
            _spriteRenderer.sprite = mouseOverSprite;
        }
    }

    void OnMouseExit()
    {
        if(buttonType != ButtonType.Sound)
        { 
            _spriteRenderer.sprite = _mouseOutSprite;
        }
    }
    #endregion
}