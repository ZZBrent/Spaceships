using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text myText = GetComponent<Text>();
		myText.text = ScoreKeeper.score.ToString();
        GameObject manager = GameObject.Find("PlayGamesController");
        if (manager != null && manager.GetComponent<PlayGamesPlatform>().localUser != null)
        {
            manager.GetComponent<GooglePlayManager>().AddScoreToLeaderBoard(GPGSIds.leaderboard_leaderboard, (long)ScoreKeeper.score);
        }

        ScoreKeeper.Reset();
	}
}
