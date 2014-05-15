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
    private static string      _leaderboardUrl;
    private static Uri         _updateUri;        
    #endregion

    #region Constructor
    static Backend()
    {
        _updateUri = new Uri("https://api.mongolab.com/api/1/databases/gameone/collections/highscores?apiKey=yJm1fjif5X4rPqnq3bz9sX1vYV4cypLK");

        _leaderboardUrl = "https://api.mongolab.com/api/1/databases/gameone/collections/highscores?l=5&s={score:-1}}&apiKey=yJm1fjif5X4rPqnq3bz9sX1vYV4cypLK";
    }
    #endregion

    #region Methods
    public static void PostHighScore(string name, long score)
    {
		try
		{
	        WebClient   webClient = new WebClient();

	        webClient.Headers.Add("Content-Type", "application/json");

	        webClient.UploadString(_updateUri, "{ name : '" + name + "', score : " + score + " }");

	        webClient.Dispose();
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
    }

    public static List<string> GetHighScores()
    {

        List<string> scoreList = new List<string>();

		try
		{
#if UNITY_WEBPLAYER
	        WWW site = new WWW(_leaderboardUrl);

	        while (!site.isDone)
	        { }

	        string jsonScores = site.text;
#else
	        WebClient webClient = new WebClient();

	        webClient.Headers.Add("Content-Type", "application/json");

	        string jsonScores = webClient.DownloadString(_leaderboardUrl);

	        webClient.Dispose();
#endif

	        var scores = JSON.Parse(jsonScores);

	        for (int index = 0; index < scores.Count; index++)
	        {
	            string scoreText = scores[index]["name"].Value + ":" + scores[index]["score"].AsInt;

	            scoreList.Add(scoreText);
	        }    
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}

		for(int index = 0; index < 5; index++)
		{
			scoreList.Add ("Food:0");
		}

        return scoreList;
    }
    #endregion
}
