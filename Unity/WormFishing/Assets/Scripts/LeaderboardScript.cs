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
    void Start () 
    {
        _defaultScore = 0;
        _defaultUsername = "Username";

        _leaderboardSpots = names.Length;
	}
    #endregion

    #region Loop
    void Update () 
    {
        FetchLeaderboard();
    }
    #endregion

    #region Methods
    private void FetchLeaderboard()
    {
        for(int index = 0; index < _leaderboardSpots; index++)
        {
            names[index].alignment = TextAlignment.Left;
            names[index].text = _defaultUsername;

            scores[index].alignment = TextAlignment.Right;
            scores[index].text = _defaultScore.ToString();
        }
    }
    #endregion
}
