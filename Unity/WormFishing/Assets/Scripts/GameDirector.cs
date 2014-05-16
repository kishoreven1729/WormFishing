#region References
using UnityEngine;
using System.Collections;
#endregion

public class GameDirector : MonoBehaviour 
{
    #region Private Variables
    #endregion

    #region Public Variables
    public Transform            character;
    public Transform            shipAnchor;
    public Transform            worm;
    public static GameDirector  instance;
    public Animator             shipAnimator;

	public Transform			tutorialShot01;
	public Transform			tutorialShot02;
    public Transform            leaderboard;
	public Transform 			deathCollider;

	public Transform			scoreboard;

	public UserInput			userInput;

    public long                 gameScore;

	public bool					isCharacterDead;

	public AudioSource			loopingSound;
    #endregion

    #region Constructor
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        gameScore = 0;

		isCharacterDead = false;

		if(PlayerPrefs.GetInt("Virgin", 0) == 0)
		{
			tutorialShot02.gameObject.SetActive(true);

			StartCoroutine(DisableTutorial2());
		}
    }
    #endregion

    #region Loop
    void Update()
    {

    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void EnablePlayerControl()
    {
#if UNITY_IPHONE || UNITY_ANDROID
		Handheld.Vibrate();
#endif

        character.SendMessage("EnablePlayerMovement", SendMessageOptions.DontRequireReceiver);
    }

    public void DisablePlayerControl()
    {
        character.SendMessage("DisablePlayerMovement", SendMessageOptions.DontRequireReceiver);
    }

    public void HaltShipAnchor()
    {
        shipAnchor.SendMessage("WormAproaching", SendMessageOptions.DontRequireReceiver);

		if(PlayerPrefs.GetInt("Virgin", 0) == 0)
		{
			if(gameScore < 2)
			{
				tutorialShot01.gameObject.SetActive(true);

				tutorialShot01.position = character.position - new Vector3(0.0f, 1.0f, 0.0f);

				StartCoroutine(DisableTutorial());
			}
			else
			{
				PlayerPrefs.SetInt("Virgin", 1);
			}
		}
    }

    public void ResumeShipAnchor()
    {
        shipAnchor.SendMessage("WormBackInDune", SendMessageOptions.DontRequireReceiver);
    }

    public void StopFiringWorm()
    {
        worm.SendMessage("StopFiringWorm", SendMessageOptions.DontRequireReceiver);
    }

    public void UpdateLeaderboard()
    {
        leaderboard.SendMessage("FetchLeaderboard", SendMessageOptions.DontRequireReceiver);
    }

    public void CharacterDead()
    {
		isCharacterDead = true;

		scoreboard.gameObject.SetActive(false);

        character.SendMessage("KillCharacter", SendMessageOptions.DontRequireReceiver);

        shipAnchor.SendMessage("KillShipAI", SendMessageOptions.DontRequireReceiver);

        UpdateLeaderboard();

        StopFiringWorm();        
    }

    public void AddMissScore()
    {
        gameScore += 1;

		AudioManager.instace.PlaySound("UI");

		if(PlayerPrefs.GetInt("Virgin", 0) == 0)
		{
			if(gameScore < 2)
			{
				tutorialShot02.gameObject.SetActive(true);

				StartCoroutine(DisableTutorial2());
			}
		}

        if(gameScore == 6 || gameScore == 11 || gameScore == 16 || gameScore == 21 || gameScore == 41)
        {
            worm.SendMessage("IncreaseDifficulty", SendMessageOptions.DontRequireReceiver);
            character.SendMessage("IncreaseDifficulty", SendMessageOptions.DontRequireReceiver);
        }        
    }
    #endregion

	#region Coroutines
	public IEnumerator DisableTutorial()
	{
		yield return new WaitForSeconds(1.0f);

		tutorialShot01.gameObject.SetActive(false);
	}

	public IEnumerator DisableTutorial2()
	{
		yield return new WaitForSeconds(1.5f);

		tutorialShot02.gameObject.SetActive(false);
	}
	#endregion
}
