using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    #region Loop
    void Update ()
    {

    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.CompareTag("Player"))
        {
        }
    }
    #endregion
}
