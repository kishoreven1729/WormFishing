#region References
using UnityEngine;
using System.Collections;
#endregion

public class DeathScript : MonoBehaviour
{
    #region Private Variables
    #endregion

    #region Public Variables
    public WormAI wormAI;
    #endregion

    #region Constructor
    void Start () 
    {
	
	}
    #endregion

    #region Loop
    void Update ()
    {

    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.CompareTag("Player"))
        {
            if(wormAI != null)
            { 
                if(wormAI.shootEvent == WormAI.ShootEvent.Animating || wormAI.shootEvent == WormAI.ShootEvent.Shot)
                {
                    GameDirector.instance.CharacterDead();
                }
            }
        }
    }
    #endregion
}
