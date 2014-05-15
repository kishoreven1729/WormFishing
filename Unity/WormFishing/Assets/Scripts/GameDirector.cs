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
    public Transform            leaderboard;

    public long                 gameScore;
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
}
