#region References
using UnityEngine;
using System.Collections;
#endregion

public class DeathScript : MonoBehaviour
{
    #region Private Variables
    #endregion

    #region Public Variables
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
            GameDirector.instance.CharacterDead();
        }
    }
    #endregion
}
