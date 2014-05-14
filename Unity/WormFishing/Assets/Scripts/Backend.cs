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
        WebClient   webClient = new WebClient();

        webClient.Headers.Add("Content-Type", "application/json");

        webClient.UploadString(_updateUri, "{ name : '" + name + "', score : " + score + " }");

        webClient.Dispose();
    }

    public static List<string> GetHighScores()
    {
        WebClient webClient = new WebClient();

        webClient.Headers.Add("Content-Type", "application/json");

        string jsonScores = webClient.DownloadString(_leaderboardUrl);

        Debug.Log(jsonScores);

        var scores = JSON.Parse(jsonScores);

        List<string> scoreList = new List<string>();

        for (int index = 0; index < scores.Count; index++)
        {
            string scoreText = scores[index]["name"].Value + ": " + scores[index]["score"].AsInt;

            scoreList.Add(scoreText);
        }

        webClient.Dispose();

        return scoreList;
    }
    #endregion
}
