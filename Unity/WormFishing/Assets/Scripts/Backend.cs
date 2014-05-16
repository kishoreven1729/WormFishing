#region References
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;
using SimpleJSON;
#endregion

public class Backend
{
    #region Private Variables
    private static Uri      	_leaderboardUri;
    private static Uri         	_updateUri;   
    #endregion

	#region Public Variables
	public static List<string> highScores;
	#endregion

    #region Constructor
    static Backend()
    {
        _updateUri = new Uri("https://api.mongolab.com/api/1/databases/gameone/collections/highscores?apiKey=yJm1fjif5X4rPqnq3bz9sX1vYV4cypLK");

        _leaderboardUri = new Uri("https://api.mongolab.com/api/1/databases/gameone/collections/highscores?l=5&s={score:-1}}&apiKey=yJm1fjif5X4rPqnq3bz9sX1vYV4cypLK");

		highScores = new List<string>();
    }
    #endregion

    #region Methods
    public static void PostHighScore(string name, long score)
    {
		try
		{
	        WebClient   webClient = new WebClient();

	        webClient.Headers.Add("Content-Type", "application/json");

	        webClient.UploadStringAsync(_updateUri, "{ name : '" + name + "', score : " + score + " }");

	        webClient.Dispose();
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
    }

    public static void GetHighScores()
    {
		highScores.Clear();

		try
		{
#if UNITY_WEBPLAYER
	        WWW site = new WWW(_leaderboardUrl);

	        while (!site.isDone)
	        { }

	        string jsonScores = site.text;
#else
	        WebClient webClient = new WebClient();

			webClient.DownloadDataCompleted += HandleDownloadDataCompleted;

	        webClient.Headers.Add("Content-Type", "application/json");

	        webClient.DownloadDataAsync(_leaderboardUri);	        
#endif	        
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
    }

    static void HandleDownloadDataCompleted (object sender, DownloadDataCompletedEventArgs e)
    {
		List<string> scoreList = new List<string>();

		string jsonString = "";

		byte[] data = e.Result;

		int dataCount = data.Length;

		for(int byteIndex = 0; byteIndex < dataCount; byteIndex++)
		{
			jsonString += (char)data[byteIndex];
		}

		var scores = JSON.Parse(jsonString);

		for (int index = 0; index < scores.Count; index++)
		{
			string scoreText = scores[index]["name"].Value + ":" + scores[index]["score"].AsInt;

			scoreList.Add(scoreText);
		}    

		highScores = scoreList;
    }
    #endregion
}
