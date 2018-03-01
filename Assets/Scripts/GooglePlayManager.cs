using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayManager : MonoBehaviour {

    static GooglePlayManager instance = null;

    // Use this for initialization
    void Awake ()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();
        }

        SignIn();
    }
	
	void SignIn()
    {
        if (Application.platform == RuntimePlatform.Android)
            Social.localUser.Authenticate(success => { });
    }

    public void AddScoreToLeaderBoard(string leaderboardName, long score)
    {
        Social.ReportScore(score, leaderboardName, success => { });
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }
}
