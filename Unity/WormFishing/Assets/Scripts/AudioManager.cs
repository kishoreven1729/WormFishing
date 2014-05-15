using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	#region Private Variables
	private AudioSource	_audioSource;
	private Dictionary<string, AudioClip> _audioDictionary;
	#endregion

	#region Public Variables
	public AudioClip[]	audioClips;
	public static AudioManager instace;
	#endregion

	#region Constructor
	void Awake()
	{
		if(instace == null)
		{
			instace = this;
		}
	}

	void Start () 
	{
		_audioSource = GetComponent<AudioSource>();

		_audioDictionary = new Dictionary<string, AudioClip>();

		for(int index = 0; index < audioClips.Length; index ++)
		{
			_audioDictionary.Add(audioClips[index].name, audioClips[index]);
		}
	}
	#endregion
	
	#region Loop
	void Update () 
	{
	
	}
	#endregion


	#region Methods
	public void PlaySound(string name)
	{
		_audioSource.clip = _audioDictionary[name];

		_audioSource.Play();
	}
	#endregion
}
