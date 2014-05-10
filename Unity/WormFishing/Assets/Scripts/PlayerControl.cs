#region References
using UnityEngine;
using System.Collections;
#endregion

public class PlayerControl : MonoBehaviour 
{
	#region Private Variables
	#endregion

	#region Public Variables
	#endregion

	#region Constructor
	void Start() 
	{
		rigidbody.AddForce (new Vector3 (2000.0f, 0.0f, 0.0f));
	}
	#endregion
	
	#region Loop
	void Update() 
	{
	
	}
	#endregion
}
