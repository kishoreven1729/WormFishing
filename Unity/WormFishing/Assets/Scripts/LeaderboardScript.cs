#region References
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#endregion

public class LeaderboardScript : MonoBehaviour
{
    #region Private Variables
    private string          _defaultUsername;
    private int             _defaultScore;
    private int             _leaderboardSpots;
    #endregion

    #region Public Variables
    public TextMesh[]       names;
    public TextMesh[]       scores;
    #endregion

    #region Constructor
    void Awake()
    {
        for (int index = 0; index < _leaderboardSpots; index++)
        {
            names[index].alignment = TextAlignment.Left;
            names[index].text = _defaultUsername;

            scores[index].alignment = TextAlignment.Right;
            scores[index].text = _defaultScore + "";
        }
    }

    void Start () 
    {
        _defaultScore = 0;
        _defaultUsername = "Username";

        _leaderboardSpots = names.Length;
	}
    #endregion

    #region Loop
    void FixedUpdate () 
    {
    }
    #endregion

    #region Methods
    public void FetchLeaderboard()
    {
		Backend.GetHighScores();

		StartCoroutine(ReceiveData());
    }
    #endregion

	#region Coroutine
	public IEnumerator ReceiveData()
	{
		while(Backend.highScores.Count == 0)
		{
			yield return null;
		}

		List<string> scoreList = Backend.highScores;

		for (int index = 0; index < scoreList.Count; index++)
		{
			string[] scoreText = scoreList[index].Split(':');
			
			names[index].alignment = TextAlignment.Left;
			names[index].text = scoreText[0];
			
			scores[index].alignment = TextAlignment.Right;
			scores[index].text = scoreText[1];
		}
		
		for(int index = scoreList.Count; index < _leaderboardSpots; index++)
		{
			names[index].alignment = TextAlignment.Left;
			names[index].text = _defaultUsername;
			
			scores[index].alignment = TextAlignment.Right;
			scores[index].text = _defaultScore + "";
		}
	}
	#endregion
}
