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

	public AudioClip	audioClip;
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
					AudioManager.instace.PlaySound("UI");

                    Application.Quit();
                    break;
                }
            case ButtonType.Leaderboard:
                {
					AudioManager.instace.PlaySound("UI");

                    Toggle();

                    toggleTransform.SendMessage("FetchLeaderboard", SendMessageOptions.DontRequireReceiver);

                    break;
                }
            case ButtonType.Start:
                {
					AudioManager.instace.PlaySound("UI");

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

						AudioManager.instace.PlaySound("UI");

                        _spriteRenderer.sprite = _mouseOutSprite;
                    }
                    break;
                }
            case ButtonType.LeaderboardMenu:
                {
					AudioManager.instace.PlaySound("UI");

                    Toggle();

                    break;
                }
            case ButtonType.LeaderboardGameOver:
                {
					AudioManager.instace.PlaySound("UI");

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