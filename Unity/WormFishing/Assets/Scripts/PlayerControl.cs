#region References
using UnityEngine;
using System.Collections;
#endregion

public class PlayerControl : MonoBehaviour 
{
	#region Private Variables
	private ConstantForce		_constantForce;
	#endregion

	#region Public Variables
	public float 				magnitude;
	#endregion

	#region Constructor
	void Start() 
	{
		//rigidbody.AddForce (new Vector3 (1000.0f, 0.0f, 0.0f));
		_constantForce = GetComponent<ConstantForce> ();
	}
	#endregion
	
	#region Loop
	void Update() 
	{
		Vector3 direction = Vector3.zero;

		if(Input.GetKey(KeyCode.A))
		{
			direction.x = 1.0f;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			direction.x = -1.0f;
		}

		_constantForce.force = direction * magnitude;
	}
	#endregion
}
