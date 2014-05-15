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

	public Transform			tutorialShot;
    public Transform            leaderboard;
	public Transform 			deathCollider;

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
        character.SendMessage("EnablePlayerMovement", SendMessageOptions.DontRequireReceiver);
    }

    public void DisablePlayerControl()
    {
        character.SendMessage("DisablePlayerMovement", SendMessageOptions.DontRequireReceiver);
    }

    public void HaltShipAnchor()
    {
        shipAnchor.SendMessage("WormAproaching", SendMessageOptions.DontRequireReceiver);

		if(GameDirector.instance.gameScore == 0)
		{
			if(PlayerPrefs.GetInt("Virgin", 0) == 0)
			{
				tutorialShot.gameObject.SetActive(true);

				tutorialShot.position = character.position - new Vector3(0.0f, 1.0f, 0.0f);

				PlayerPrefs.SetInt("Virgin", 1);

				StartCoroutine(DisableTutorial());
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

        character.SendMessage("KillCharacter", SendMessageOptions.DontRequireReceiver);

        shipAnchor.SendMessage("KillShipAI", SendMessageOptions.DontRequireReceiver);

        UpdateLeaderboard();

        StopFiringWorm();        
    }

    public void AddMissScore()
    {
        gameScore += 1;

		AudioManager.instace.PlaySound("UI");

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

		tutorialShot.gameObject.SetActive(false);
	}
	#endregion
}
