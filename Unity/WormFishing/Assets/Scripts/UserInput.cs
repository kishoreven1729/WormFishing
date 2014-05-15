using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour 
{
	#region Private Variables
	private bool   	hasUserEnteredName;
	private string	_userName;
	private bool	_enabled;
	#if UNITY_ANDROID || UNITY_IPHONE
	private TouchScreenKeyboard	keyboard;
	#endif
	#endregion

	#region Public Variables
	public GUISkin	guiSkin;
	#endregion

	void Start () 
	{
		hasUserEnteredName = false;	

		_enabled = false;

		_userName = "";
	}
	
	void Update () 
	{

	}

	void OnGUI()
	{
		GUI.skin = guiSkin;

		if(_enabled)
		{
			#if UNITY_WEBPLAYER || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
			Event e = Event.current;
			
			if (e.keyCode == KeyCode.Return) 
			{
				hasUserEnteredName = true;
			}

			if(!hasUserEnteredName)
			{
				GUI.Label (new Rect (Screen.width / 2 - 115.0f, Screen.height - 70.0f, 100.0f, 30.0f), "Enter Name:");
				
				_userName = GUI.TextField (new Rect (Screen.width / 2 - 35.0f, Screen.height - 70.0f, 100.0f, 25.0f), _userName, 40);
			}
			else
			{
				if(_userName.Length > 8)
				{
					_userName = _userName.Substring(0, 8);
				}


				Backend.PostHighScore(_userName, GameDirector.instance.gameScore);

				GameDirector.instance.UpdateLeaderboard();

				DisableInput();
			}
#else
			if(hasUserEnteredName == false)
			{
				keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "Enter Username:");

				hasUserEnteredName = true;
			}

			if(keyboard.done)
			{
				_userName = keyboard.text;

				if(_userName.Length > 8)
				{
					_userName = _userName.Substring(0, 8);
				}

				Backend.PostHighScore(_userName, GameDirector.instance.gameScore);

				GameDirector.instance.UpdateLeaderboard();
				
				DisableInput();
			}
#endif
		}
	}

	#region Public Methods
	public void EnableInput()
	{
		_enabled = true;
	}

	public void DisableInput()
	{
		_enabled = false;
	}
	#endregion
}
