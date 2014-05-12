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
    public static GameDirector  instance;
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
    #endregion
}
